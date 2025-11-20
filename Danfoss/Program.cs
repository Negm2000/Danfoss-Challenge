using System.Text.Encodings.Web;
using Danfoss;
using System.Text.Json;

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};

string logPath = "C:\\Users\\Karim Negm\\Documents\\Danfoss\\Danfoss\\Danfoss\\app.log";
StreamReader sr = new StreamReader(logPath);
string? line = sr.ReadLine();
string? nextLine  = sr.ReadLine();
while (line is not null)
{
    var timestamp = LogParser.GetTimestamp(line);
    if (timestamp is not null)
    {
        LogEntry entry = new LogEntry();
        entry.timestamp = timestamp;
        entry.logLevel = LogParser.GetLogLevel(line);
        entry.message = LogParser.GetMessage(line);
        if (entry.logLevel is "ERROR" or "CRITICAL")
        {
            while (LogParser.GetTimestamp(nextLine) is null)
            {
                entry.message +=  nextLine;
                nextLine = sr.ReadLine();
            }
        }
        string finalJson = JsonSerializer.Serialize(entry, options);
        if (nextLine is not null) finalJson += ',';
        Console.WriteLine(finalJson);
    }
    line = nextLine;
    nextLine = sr.ReadLine();
}



