using System.Text.Json;
using System.Text.RegularExpressions;

namespace Danfoss;

public static class LogFileParser
{
    /// <summary>
    /// Extracts log entries, serializes each entry, and prints it to the console.
    /// </summary>
    /// <remarks>
    /// This method reads the file line-by-line, which means the log file size won't matter for memory consumption,
    /// it's just as efficient on a 1KB log file as it is on a 10GB log file.
    /// </remarks>
    /// <param name="logPath">The absolute or relative path to the log file.</param>
    /// <param name="noiseFilter">
    /// A Regex pattern may be used to identify and ignore "noise", automatically filters out any lines matching the provided Regex.
    /// If null, all lines between timestamps are captured.
    /// </param>
    /// <param name="options">Optional JSON serialization settings to adjust outout formatting.</param>
    public static void ParseFile(string logPath, Regex? noiseFilter = null, JsonSerializerOptions? options = null)
    {
        // Read the file line by line
        var sr = new StreamReader(logPath);
        string? line = sr.ReadLine();
        // Read next line early to allow for peeking
        string? nextLine = sr.ReadLine();
        while (line is not null)
        {
            // An entry starts when a timestamp is found, otherwise we ignore it
            if (LogParser.GetTimestamp(line) is not null)
            {
                // Initialize entry to be serialized
                var entry = new LogEntry
                {
                    // Strict mode will yield null if the timestamp isn't ISO8601 compliant
                    // otherwise the invalid timestamp will be printed as is
                    timestamp = LogParser.GetTimestamp(line, strict: true),
                    logLevel = LogParser.GetLogLevel(line),
                    message = LogParser.GetMessage(line)
                };
                
                // Multi-line message ends only when we reach the next timestamp/EOF
                while (nextLine != null && LogParser.GetTimestamp(nextLine) is null)
                {
                    // No filter, append everything until the next timestamp
                    if (noiseFilter == null)
                        entry.message += $"\n{nextLine}";
                    // Apply filter, append line to message only if it's not noise
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