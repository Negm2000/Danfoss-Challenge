namespace Danfoss;

/// <summary>
/// Represents a structured singular log entry parsed from the raw log file.
/// </summary>
/// <remarks>
/// By bundling all the log conents in one structured record with field names matching the one in the JSON.
/// We can now easily serialize the entry using JsonSerializer.
/// </remarks>
public record LogEntry
{
    public String? timestamp { get; set; }
    public String? logLevel { get; set; }
    public String? message { get; set; }
}