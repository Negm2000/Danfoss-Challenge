using Danfoss;

var testString =
    "2023-11-01T08:08:00Z [INFO] - Report generated and sent to reports@example.com. \\nAttachment: report_2023-11-01.pdf\n";

var logEntry = new LogEntry();
Console.WriteLine(LogParser.GetTimestamp(testString));
Console.WriteLine(LogParser.GetErrorCode(testString));
Console.WriteLine(LogParser.GetMessage(testString));
