 R&D Software Intern Test Task: Robust Log File Parser

### To the Candidate:

Welcome! This task is designed to simulate a common challenge faced by software developers: working with imperfect, real-world data. Your goal is to demonstrate your problem-solving skills, attention to detail, and ability to make and communicate technical decisions.

### Scenario:

At our company, we have a legacy application that generates logs in a text file named `app.log`. We need to ingest these logs into our new analytics platform, which requires a structured JSON format. Your task is to write a C# console application that parses the log file and converts it into the desired format.

### The Core Task:

- Write a C# console application that performs the following:
  - Reads the content of the provided `app.log` file.
  - Parses each log entry.
  - Prints a single JSON array of structured log objects to standard output.


### Required Output Format:

For a well-formed log line, the output JSON object should look like this:

```json
{
  "timestamp": "2023-10-27T10:00:00Z",
  "logLevel": "INFO",
  "message": "User 'admin' logged in."
}
```

### Deliverables:

Please submit a ZIP file containing the following:

- **The complete C# project source code**.
- **`README.md`**: A markdown file explaining your solution. **This is the most important part of your submission.**

The `README.md` file must contain:

- A brief explanation of the approach you took to parse the file.
- A detailed section titled **"Assumptions and Decisions"** where you explain how you handled each of the inconsistencies in `app.log`.

### Evaluation Criteria:

You will be evaluated on the following, in order of importance:

1.  **Communication and Assumptions**: The clarity and quality of your `README.md` file. We want to see *how* you think. A perfect script with a poor explanation is worse than a good script with a great explanation.
2.  **Robustness**: Your script should successfully process the entire `app.log` file without crashing and produce a valid JSON output.
3.  **Correctness**: The script should correctly parse the "well-formed" lines.
4.  **Code Quality**: Your code should be clean, readable, follow standard C# conventions, and be well-commented where necessary.

### What you don't need to do:

- You do not need to build a web server, a database, or a complex command-line interface. A simple console application that reads a file and prints to the console is all that's required.
- You do not need to use any external NuGet packages, you are free and encouraged to use the built-in .NET libraries.