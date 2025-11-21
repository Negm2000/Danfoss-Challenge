namespace Danfoss;

public record LogEntry()
{
    public String? timestamp { get; set; }
    public String? logLevel { get; set; }
    public String? message { get; set; }
}