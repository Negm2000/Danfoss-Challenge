namespace Danfoss;

/// <summary>
/// Represents a structured singular log entry parsed from the raw log file.
/// </summary>
/// <remarks>
/// By bundling all the log contents in one structured record with field names matching the one in the JSON.
/// We can now easily serialize the entry using JsonSerializer.
/// </remarks>
public record LogEntry
{
    public string? timestamp { get; set; }
    public string? logLevel { get; set; }
    public string? message { get; set; }
}