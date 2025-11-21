# Thought Process

In chronological order I started by doing the following, taking notes during the process.
20/11/2025
1. Analyze app.log, and take note of the different patterns.
  - All valid log entries had a timestamp (or at least a malformed timestamp), entries without a timestamp could be safely ignored.
    - All timestamps had the form 'text-text-text'
  - Some entries were multi-line, how do we know when an entry ends?
    - If we say an entry ends once the next entry begins, we risk including noise that should have been ignored.
      - If we assume only CRITICAL and ERROR could be multi-line then we can process any kind of error message.
        And we will avoid the noise in this specific file, but it runs the risk of including noise in the message.
    - One alternative is to write a regex for each kind error. Safer but doesn't scale.
2. I know **regex** is the right tool for this, so I researched how to use regex in C# and found a cheat sheet for all 
      the regex commands which I kept as a reference.

# Assumptions
1. Log entries with no timestamps can be ignored.
2. Multi-line messages only end when a new timestamp begins.
3. Messages with multiple lines only occur in CRITICAL or ERROR entries.
4. Any missing logLevel or message will be serialized as null.