namespace Danfoss;

using System.Text.RegularExpressions;

public static class LogParser
{
    public static String? GetTimestamp(String entry)
    {
        // Pattern 1: entry that begins with TEXT-TEXT-TEXT
        // Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
        var timestampRegex = new Regex(@"^(\S+-\S+-\S+) |^\S+ (\S+-\S+-\S+)");
        var firstPattern = timestampRegex.Match(entry).Groups[1];
        var secondPattern = timestampRegex.Match(entry).Groups[2];
        if (!firstPattern.Success && !secondPattern.Success) return null;
        return firstPattern.Success ? firstPattern.Value : secondPattern.Value;
    }

    public static String? GetLogLevel(String entry)
    {
        // Captures anything in the form of [TEXT]
        Regex errorRegex = new Regex(@"\[(\S+)\]");
        // Groups[0] is [TEXT] - Groups[1] is TEXT without the square brackets
        var match = errorRegex.Match(entry).Groups[1];
        return match.Success ? match.Value : null; // No error code
    }

    public static String? GetMessage(String entry)
    {
        // Captures all text after the first dash, until a new line begins.
        // Multi-line logic handled in main code
        Regex messageRegex = new Regex(@" - (.*)");
        // Groups[1] is the message without the ' - '
        var match = messageRegex.Match(entry).Groups[1];
        return match.Success ? match.Value : null;
    }
}