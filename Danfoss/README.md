## Approach
1. File parsing: `StreamReader` was used to read the file line by line, only storing the current and next line. 
this program could potentially be used for files that are many gigabytes in size, as such an intentional effort was made 
to minimize memory usage, we only store what we need, hence the decision to use StreamReader.
2. Entry parsing: we know we need to parse a line if it has a timestamp, and is therefore a new entry, to parse the entry
   Regex is used throughout the program to robustly extract timestamps, log levels, and messages from each entry across 
   different patterns and formats.
3. Multi-line entries and noise: the program handles multi-line entries by appending every line after an entry until it 
   reaches the next timestamp (or end of file), to avoid including noise, it uses a noise filter regex which tells the 
   program which lines to avoid.
4. JSON Serialization: by bundling all the log contents in one structured record with field names matching the one in the JSON.
                       we can easily serialize the entry using `JsonSerializer`.

## Assumptions and Decisions
1. All separate log entries must have a timestamp.
2. All log entries are multi-line by default.
3. Multi-line messages only end when a new timestamp begins or the file ends.
4. All noise patterns are known and no real messages will share the same patterns.
5. Missing and invalid data will be serialized as null.

### Reasoning
1. Analyzing the app.log file, even though there are some format inconsistencies, overall the following holds true, 
   each separate log entry had a timestamp, any lines that didn't have one were either part of another entry or just noise.
   - Although the timestamps and log level were somtimes in reversed orders, this is easy to manage by writing a Regex for each pattern.
2. Some entries were multi-line, some were not, how do we know which type of entry will it be? 
   -  We can't, so we must assume every entry could be multi-line.
3. How do we know when a multi-line entry ends? 
   - Our only option is to assume an entry ends once the next entry begins.
      - However, this assumption comes with some caveats, we risk including noise
                  that should have been ignored.
      - If we assume only `CRITICAL` and `ERROR` are multi-line then we will avoid any noise in this file,
      but this idea is too fragile and would fail if in another log file there's an `ERROR` that's followed by noise.
      - Another option is to only look for tracebacks and XML error messages (difficult to write a proper Regex for and will likely be buggy).
4. The safest and simplest option is to filter out the noise explicitly.
   - Easy, however requires the noise patterns be known beforehand.
5. Some entries had missing or invalid data
   - For the sake of standardization, they will all be serialized in the same way, as `null`.
        - This behavior can easily be changed later using the `??` operator to assign default values.
        - This allows a program reading the JSON to do null checking and handle missing information in a consistent manner between all parameters.
