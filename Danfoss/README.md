# Assumptions and Decisions
1. Log entries must have a timestamp.
2. All log entries are multi-line by default.
3. Multi-line messages only end when a new timestamp begins or the file ends.
4. Any missing info will be serialized as null.
5. All noise patterns are known and no real messages will share the same patterns.

### Reasoning
Analyzing `app.log`, we can notice that the overall structure 
   - Each seperate log entry had a timestamp (or at least a malformed timestamp), any lines that didn't have one were either part of another entry or just noise.
   - Some entries were multi-line, problem is, how do we know when a multi-line entry ends?
       - If we say an entry ends once the next entry begins, and include everything in-between we risk including noise
         that should have been ignored.
       - If we assume only `CRITICAL` and `ERROR` are multi-line then we will avoid any noise in this specific file,
         but this would fail if in another file an `ERROR` is followed by noise.
       - One option is to only select for tracebacks and XML error messages (difficult, fragile).
       - The safest option is to filter out the noise explicitly (easy, but requires the noise be known)