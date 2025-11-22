# Assumptions
1. Log entries must have a timestamp.
2. All log entries are multi-line by default.
3. Multi-line messages only end when a new timestamp begins or the file ends.
4. Any missing info will be serialized as null.
5. All noise patterns are known and no real messages will share the same patterns.

# Thought Process

In chronological order I started by doing the following, taking notes during the process.
1. Analyze app.log, and take note of the different patterns.
   - All valid log entries had a timestamp (or at least a malformed timestamp), all the lines that don't have one were either part of another entry or noise.
   - Some entries were multi-line, problem is, how do we know when a multi-line entry ends?
       - If we say an entry ends once the next entry begins, and include everything in-between we risk including noise
         that should have been ignored.
       - If we assume only `CRITICAL` and `ERROR` are multi-line then we will avoid any noise in this specific file,
         but this would fail if in another file an `ERROR` is followed by noise.
       - Safest option is to only select for specific types of error messages (difficult, fragile).
       - Or filter out the noise explicitly (easy, but assumes noise known)
2. I know **Regex** is the perfect tool for filtering out text, so I researched how to use regex in C# and found a cheat sheet for all
   the regex commands which I kept as a reference.
