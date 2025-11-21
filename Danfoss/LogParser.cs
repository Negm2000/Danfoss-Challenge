namespace Danfoss;

using System.Text.RegularExpressions;

public static class LogParser
{
    public static String? GetTimestamp(String entry)
    {
        // Returns the first timestamp that has the form TEXT-TEXT-TEXT
        Regex timestampRegex = new Regex(@"\S+-\S+-\S+");
        if (timestampRegex.Match(entry).Success) return timestampRegex.Match(entry).Value;
        return null; // No timestamp 
    }

    public static String? GetLogLevel(String entry)
    {
        // Captures anything in the form of [TEXT]
        Regex errorRegex = new Regex(@"\[(\S+)\]");
        // Groups[0] is [TEXT] - Groups[1] is TEXT without the square brackets
        var match = errorRegex.Match(entry).Groups[1];
        if (match.Success) return match.Value;
        return null; // No error code
    }

    public static String? GetMessage(String entry)
    {
        // Captures all text after the first dash, until a new line begins.
        // Multi-line logic handled in main code
        Regex messageRegex = new Regex(@" - (.*)");
        // Groups[1] is the message without the ' - '
        var match = messageRegex.Match(entry).Groups[1];
        if (match.Success) return match.Value;
        return null;
    }
}