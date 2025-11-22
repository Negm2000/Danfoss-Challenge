namespace Danfoss;
using System.Text.RegularExpressions;


public static class LogParser
{   // Captures one of the following
    // Pattern 1: entry that begins with TEXT-TEXT-TEXT 
    // Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
    private static readonly Regex TimestampRegex = new Regex(@"^(\S+-\S+-\S+) |^\[\S+\] (\S+-\S+-\S+)");
    
    // Capturs only ISO8601 Timestamps
    private static readonly Regex IsoRegex = new Regex(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z");
    
    // Captures one of the following
    // Pattern 1: entry that begins with TEXT-TEXT-TEXT [TEXT]
    // Pattern 2: entry that begins with [TEXT]
    private static readonly Regex ErrorRegex = new Regex(@"^\S+-\S+-\S+ \[(\S+)\]|^\[(\S+)\]"); 
    
    // Captures all text after the first dash, until a new line begins.
    private static readonly Regex MessageRegex = new Regex(@" - (.*)");
    
    
    /// <summary>
    /// Extracts the timestamp from a log entry.
    /// </summary>
    /// <remarks>
    /// Timestamp must have the form TEXT-TEXT-TEXT, formats that don't have the dashes won't be detected.
    /// </remarks>
    /// <param name="entry">The log entry string to parse.</param>
    /// <param name="strict"> When true, will preform an extra check to validate that the timestamp is ISO8601 compliant.</param>
    public static string? GetTimestamp(string entry, bool strict = false)
    {
        // Pattern 1: entry that begins with TEXT-TEXT-TEXT
        // Pattern 2: entry that begins with [TEXT] TEXT-TEXT-TEXT
        var firstPattern = TimestampRegex.Match(entry).Groups[1];
        var secondPattern = TimestampRegex.Match(entry).Groups[2];
        if (!firstPattern.Success && !secondPattern.Success) return null;
        var result = firstPattern.Success ? firstPattern.Value : secondPattern.Value;
        // If in strict mode, make sure it also follows the ISO8601 pattern
        if (strict && !IsoRegex.IsMatch(result)) return null;
        return result;
    }


    /// <summary>
    /// Extracts the log level from a log entry.
    /// </summary>
    /// <remarks>
    /// Log level must be inside square brackets i.e [TEXT]
    /// </remarks>
    /// <param name="entry">The log entry string to parse.</param>
    public static string? GetLogLevel(string entry)
    {
        // Groups[0] is [TEXT] - Groups[1] is TEXT without the square brackets
        // Pattern 1: entry that begins with TEXT-TEXT-TEXT [TEXT]
        // Pattern 2: entry that begins with [TEXT]
        var firstPattern = ErrorRegex.Match(entry).Groups[1];
        var secondPattern = ErrorRegex.Match(entry).Groups[2];
        if (!firstPattern.Success && !secondPattern.Success) return null;
        return firstPattern.Success ? firstPattern.Value : secondPattern.Value;
    }

    /// <summary>
    /// Extracts the message from a log entry.
    /// </summary>
    /// <remarks>
    /// This only extracts the first line of a message.
    /// </remarks>
    /// <param name="entry">The log entry string to parse.</param>
    public static string? GetMessage(string entry)
    {
        // Groups[1] is the message without the ' - '
        var match = MessageRegex.Match(entry).Groups[1];
        return match.Success ? match.Value : null;
    }
}