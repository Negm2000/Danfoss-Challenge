using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Danfoss;
using System.Text.Json;

// Optional but makes the output nicer
var options = new JsonSerializerOptions
{
    WriteIndented = true, // Prettier outputs
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Prevents turning 'admin' into \u0027admin\u0027 
};
const string logPath = @"C:\Users\Karim Negm\Documents\Danfoss\Danfoss\Danfoss\app.log";
var noiseFilter = new Regex(@"^#.*|^This is not a real log entry|^ +SYSTEM MAINTENANCE WINDOW|^=+");
LogFileParser.ParseFile(logPath, noiseFilter, options);