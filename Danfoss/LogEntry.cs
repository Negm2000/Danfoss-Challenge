namespace Danfoss;

public record LogEntry()
{
    public String? timestamp{get;set;}
    public String? logLevel { get; set; } = "INFO";
    
    public String? message{get;set;}
}
