namespace Danfoss;
using System.IO;
public static class FileParser
{
    private static String? _FilePath
    {
        get;
        set
        {
            if (File.Exists(value)) field = value;
        }
        
        
    }
}