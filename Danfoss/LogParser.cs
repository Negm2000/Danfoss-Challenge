namespace Danfoss;
using System.Text.RegularExpressions;
public static class LogParser
{
    public static String getTimestamp(String entry)
    {
        Regex timestampRegex = new Regex(@"\S+-\S+-\S+");
        return timestampRegex.Match(entry).Value;
    }
}