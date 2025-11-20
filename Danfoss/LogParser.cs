namespace Danfoss;
using System.Text.RegularExpressions;

public static class LogParser
{
    public static String? GetTimestamp(String entry)
    {
        Regex timestampRegex = new Regex(@"\S+-\S+-\S+");
        if (timestampRegex.Match(entry).Success) return timestampRegex.Match(entry).Value;
        return null; // No timestamp found in this format
    }

    public static String? GetErrorCode(String entry)
    {
        Regex errorRegex = new Regex(@"\[(\S+)\]");
        // Groups[0] is [ERROR] - Groups[1] is ERROR without the square brackets
        var match = errorRegex.Match(entry).Groups[1];
        if (match.Success) return match.Value;
        return null; // No error code
    }

    public static String? GetMessage(String entry)
    {
        Regex messageRegex = new Regex(@" - (.*)");
        // Groups[1] is the First Parenthesis
        var match = messageRegex.Match(entry).Groups[1];
        if (match.Success) return match.Value;
        return null; // No message
    }
}