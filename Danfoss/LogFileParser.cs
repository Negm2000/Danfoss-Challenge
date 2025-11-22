using System.Text.Json;
using System.Text.RegularExpressions;

namespace Danfoss;

public static class LogFileParser
{
    public static void ParseFile(string logPath, Regex? noiseFilter = null, JsonSerializerOptions? options = null)
    {
        // Read the file line by line
        StreamReader sr = new StreamReader(logPath);
        string? line = sr.ReadLine();
        // Read next line early to allow for peeking
        string? nextLine = sr.ReadLine();
        while (line is not null)
        {
            // An entry starts when a timestamp is found, otherwise we ignore
            var timestamp = LogParser.GetTimestamp(line);
            if (timestamp is not null)
            {
                // Initialize entry to be serialized
                LogEntry entry = new LogEntry
                {
                    timestamp = timestamp,
                    logLevel = LogParser.GetLogLevel(line),
                    message = LogParser.GetMessage(line)
                };
                // Multi-line message ends only when we reach the next timestamp/EOF
                while (nextLine != null && LogParser.GetTimestamp(nextLine) is null)
                {
                    // No filter, append everything until the next timestamp
                    if (noiseFilter == null)
                        entry.message += $"\n{nextLine}";
                    // Append line to message only if it's not noise
                    else if (!noiseFilter.IsMatch(nextLine)) 
                        entry.message += $"\n{nextLine}";
                    nextLine = sr.ReadLine();
                }

                string finalJson = JsonSerializer.Serialize(entry, options);
                // Add commas between JSON entries unless it's the last one
                if (nextLine is not null) finalJson += ',';
                Console.WriteLine(finalJson);
            }

            // Move on to the next log entry
            line = nextLine;
            nextLine = sr.ReadLine();
        }
    }
}