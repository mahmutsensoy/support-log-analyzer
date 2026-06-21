namespace SupportLogAnalyzer.Models;

public sealed class LogEntry
{
    public int LineNumber { get; init; }
    public string Level { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}
