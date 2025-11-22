# Assumptions
1. Log entries must have a timestamp.
2. All log entries are multi-line by default.
3. Multi-line messages only end when a new timestamp begins or the file ends.
4. All noise patterns are known and no real messages will share the same patterns.

### Decisions and Reasoning
Analyzing `app.log`, we can notice that the overall structure has the following features:
   - Each seperate log entry had a timestamp, any lines that didn't have one were either part of another entry or just noise.
     - The timestamps weren't always in the beginning of the line, there were two possible patterns, we must check for both.
       - Pattern 1: entry that begins with TEXT-TEXT-TEXT
       - Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
   - Some entries were multi-line, problem is, how do we know when a multi-line entry ends?
       - If we say an entry ends once the next entry begins, and include everything in-between we risk including noise
         that should have been ignored, we would still need to find a way to avoid the noise.
       - If we do that but assume only `CRITICAL` and `ERROR` are multi-line then we will avoid any noise in this file,
         but this idea is too fragile and would fail if in another log file there's an `ERROR` that's followed by noise.
       - One option is to only look for tracebacks and XML error messages (difficult to write a proper Regex for, will likely be buggy).
       - The safest option is to filter out the noise explicitly (easy, but requires the noise be known)
   - Some entries had missing info, for the sake of standardization, they will all be serialized as null.
     - This behavior can easily be changed using the `??` operator to assign default values.
   - **Regex** is designed for text selection and filtering, which is exactly our case. 
     It makes the whole filtration process much easier and more robust compared to
     writing custom search functions and that's why I decided to use it.