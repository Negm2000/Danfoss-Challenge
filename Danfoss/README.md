# Thought Process

In chronological order I started by doing the following, taking notes during the process.
20/11/2025
- [x] Analyze app.log, and take note of the different patterns.
  - All valid log entries had a timestamp (or at least a malformed timestamp), entries without a timestamp could be safely ignored.
    - All timestamps had the form 'text-text-text'
  - Some entries were multi-line, how do we know when an entry ends?
    - If we say an entry ends once the next entry begins, we risk including noise that should have been ignored.
      - If we assume only CRITICAL and ERROR could be multi-line then we can process this specific file, but I think 
        there has to be a better solution that scales better.
- [x] I know regex is the right tool for this, so I researched how to use regex in C# and found a cheat sheet for all 
      the regex commands which I kept as a reference.