# Thought Process

In chronological order I started by doing the following, taking notes during the process.
20/11/2025
1. Analyze app.log, and take note of the different patterns.
  - All valid log entries had a timestamp (or at least a malformed timestamp), entries without a timestamp could be safely ignored.
    - All timestamps had the form 'text-text-text'
  - Some entries were multi-line, how do we know when an entry ends?
    - If we say an entry ends once the next entry begins, and include everything in-between we risk including noise 
      that should have been ignored.
    - If we assume only `CRITICAL` and `ERROR` could be multi-line then we will avoid any noise in this specific file,
      but this will fail if an `ERROR` is followed by noise.
    - Safest option is to only select for specific types of error messages. (difficult)
    - Or filter out the noise explicitly. (easy)
2. I know **Regex** is the right tool for this, so I researched how to use regex in C# and found a cheat sheet for all 
      the regex commands which I kept as a reference.

# Assumptions
1. Log entries must have a timestamp.
2. Multi-line messages only end when a new timestamp begins.
3. Any missing logLevel or message will be serialized as null.
4. All noise patterns are known and no real messages will share the same patterns.
5. Partially malformed entries should be serialized, this can easily be adjusted however by changing the regex to be more selective.