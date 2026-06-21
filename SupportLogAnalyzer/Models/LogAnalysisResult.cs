namespace SupportLogAnalyzer.Models;

public sealed class LogAnalysisResult
{
    public string FilePath { get; init; } = string.Empty;
    public int TotalLines { get; init; }
    public int ErrorCount { get; init; }
    public int WarnCount { get; init; }
    public int ExceptionCount { get; init; }
    public IReadOnlyList<LogEntry> Entries { get; init; } = [];

    public string BuildSummaryText()
    {
        return $"""
            Log Analiz Özeti
            ================
            Dosya       : {FilePath}
            Toplam satır: {TotalLines}
            ERROR       : {ErrorCount}
            WARN        : {WarnCount}
            Exception   : {ExceptionCount}
            """;
    }
}
