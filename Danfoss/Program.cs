using System.Text.Encodings.Web;
using Danfoss;
using System.Text.Json;

// Optional but makes the output nicer
var options = new JsonSerializerOptions
{
    WriteIndented = true, // Prettier outputs
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Prevents turning 'admin' into \u0027admin\u0027 
};

string logPath = @"C:\Users\Karim Negm\Documents\Danfoss\Danfoss\Danfoss\app.log";
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
        // ERROR and CRITICAL entries could contain multi-line messages
        if (entry.logLevel is "ERROR" or "CRITICAL")
        {
            // Multi-line message ends only when we reach the next timestamp/EOF
            while (nextLine is not null && LogParser.GetTimestamp(nextLine) is null)
            {
                // Append line to message
                entry.message += $"\n{nextLine}";
                nextLine = sr.ReadLine();
            }
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