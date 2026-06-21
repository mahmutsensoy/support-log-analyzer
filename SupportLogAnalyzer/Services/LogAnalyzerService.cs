using System.Text.RegularExpressions;
using SupportLogAnalyzer.Models;

namespace SupportLogAnalyzer.Services;

public sealed class LogAnalyzerService
{
    private static readonly Regex ErrorPattern = new(@"\b(ERROR|FATAL|CRITICAL)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex WarnPattern = new(@"\b(WARN|WARNING)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ExceptionPattern = new(
        @"\b(Exception|Stack\s*Trace|NullReferenceException|SqlException|IOException|ArgumentException)\b",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public LogAnalysisResult Analyze(string filePath, bool includeErrors, bool includeWarnings, bool includeExceptions)
    {
        var lines = File.ReadAllLines(filePath);
        var entries = new List<LogEntry>();
        var errorCount = 0;
        var warnCount = 0;
        var exceptionCount = 0;

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var isError = ErrorPattern.IsMatch(line);
            var isWarn = WarnPattern.IsMatch(line);
            var isException = ExceptionPattern.IsMatch(line);

            if (isError)
            {
                errorCount++;
            }

            if (isWarn)
            {
                warnCount++;
            }

            if (isException)
            {
                exceptionCount++;
            }

            var shouldInclude = (includeErrors && isError)
                || (includeWarnings && isWarn)
                || (includeExceptions && isException);

            if (!shouldInclude)
            {
                continue;
            }

            entries.Add(new LogEntry
            {
                LineNumber = i + 1,
                Level = ResolveLevel(isError, isWarn, isException),
                Content = line.Trim()
            });
        }

        return new LogAnalysisResult
        {
            FilePath = filePath,
            TotalLines = lines.Length,
            ErrorCount = errorCount,
            WarnCount = warnCount,
            ExceptionCount = exceptionCount,
            Entries = entries
        };
    }

    public void ExportReport(LogAnalysisResult result, string outputPath)
    {
        using var writer = new StreamWriter(outputPath);

        writer.WriteLine(result.BuildSummaryText());
        writer.WriteLine();
        writer.WriteLine("Filtrelenmiş Kayıtlar");
        writer.WriteLine("=====================");
        writer.WriteLine();

        foreach (var entry in result.Entries)
        {
            writer.WriteLine($"[{entry.LineNumber}] [{entry.Level}] {entry.Content}");
        }
    }

    private static string ResolveLevel(bool isError, bool isWarn, bool isException)
    {
        if (isError)
        {
            return "ERROR";
        }

        if (isWarn)
        {
            return "WARN";
        }

        return isException ? "EXCEPTION" : "INFO";
    }
}
