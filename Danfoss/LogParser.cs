namespace Danfoss;

using System.Text.RegularExpressions;

public static class LogParser
{   // Captures one of the following
    // Pattern 1: entry that begins with TEXT-TEXT-TEXT
    // Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
    private static readonly Regex TimestampRegex = new Regex(@"^(\S+-\S+-\S+) |^\S+ (\S+-\S+-\S+)");
    // Captures anything in the form of [TEXT]
    private static readonly Regex ErrorRegex = new Regex(@"\[(\S+)\]");
    // Captures all text after the first dash, until a new line begins.
    private static readonly Regex MessageRegex = new Regex(@" - (.*)");
    public static String? GetTimestamp(String entry)
    {
        // Pattern 1: entry that begins with TEXT-TEXT-TEXT
        var firstPattern = TimestampRegex.Match(entry).Groups[1];
        // Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
        var secondPattern = TimestampRegex.Match(entry).Groups[2];
        if (!firstPattern.Success && !secondPattern.Success) return null;
        return firstPattern.Success ? firstPattern.Value : secondPattern.Value;
    }

    public static String? GetLogLevel(String entry)
    {
        // Groups[0] is [TEXT] - Groups[1] is TEXT without the square brackets
        var match = ErrorRegex.Match(entry).Groups[1];
        return match.Success ? match.Value : null; // No error code
    }

    public static String? GetMessage(String entry)
    {
        // Groups[1] is the message without the ' - '
        var match = MessageRegex.Match(entry).Groups[1];
        return match.Success ? match.Value : null;
    }
}