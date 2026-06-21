using SupportLogAnalyzer.Models;
using SupportLogAnalyzer.Services;

namespace SupportLogAnalyzer;

public partial class MainForm : Form
{
    private readonly LogAnalyzerService _analyzer = new();
    private LogAnalysisResult? _lastResult;
    private string? _selectedFilePath;

    public MainForm()
    {
        InitializeComponent();
    }

    private void BtnOpenFile_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Log dosyası seçin",
            Filter = "Log dosyaları (*.log;*.txt)|*.log;*.txt|Tüm dosyalar (*.*)|*.*",
            InitialDirectory = ResolveSamplesDirectory()
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        _selectedFilePath = dialog.FileName;
        lblFilePath.Text = _selectedFilePath;
        btnAnalyze.Enabled = true;
        ClearResults();
    }

    private void BtnAnalyze_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_selectedFilePath) || !File.Exists(_selectedFilePath))
        {
            MessageBox.Show("Lütfen önce geçerli bir log dosyası seçin.", "Uyarı",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            _lastResult = _analyzer.Analyze(
                _selectedFilePath,
                chkErrors.Checked,
                chkWarnings.Checked,
                chkExceptions.Checked);

            DisplayResults(_lastResult);
            btnExport.Enabled = _lastResult.Entries.Count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Dosya okunurken hata oluştu:\n{ex.Message}", "Hata",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        if (_lastResult is null)
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = "Raporu kaydet",
            Filter = "Metin dosyası (*.txt)|*.txt",
            FileName = $"log-raporu-{DateTime.Now:yyyyMMdd-HHmmss}.txt"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        try
        {
            _analyzer.ExportReport(_lastResult, dialog.FileName);
            MessageBox.Show($"Rapor kaydedildi:\n{dialog.FileName}", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Rapor kaydedilemedi:\n{ex.Message}", "Hata",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Filter_CheckedChanged(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_selectedFilePath) && File.Exists(_selectedFilePath))
        {
            BtnAnalyze_Click(sender, e);
        }
    }

    private void DisplayResults(LogAnalysisResult result)
    {
        lblTotalLinesValue.Text = result.TotalLines.ToString();
        lblErrorCountValue.Text = result.ErrorCount.ToString();
        lblWarnCountValue.Text = result.WarnCount.ToString();
        lblExceptionCountValue.Text = result.ExceptionCount.ToString();
        lblSummary.Text = result.BuildSummaryText();

        listEntries.BeginUpdate();
        listEntries.Items.Clear();

        foreach (var entry in result.Entries)
        {
            var item = new ListViewItem(entry.LineNumber.ToString());
            item.SubItems.Add(entry.Level);
            item.SubItems.Add(entry.Content);
            item.ForeColor = entry.Level switch
            {
                "ERROR" => Color.DarkRed,
                "WARN" => Color.DarkGoldenrod,
                "EXCEPTION" => Color.DarkMagenta,
                _ => Color.Black
            };
            listEntries.Items.Add(item);
        }

        listEntries.EndUpdate();
        lblResultCount.Text = $"{result.Entries.Count} kayıt listelendi";
    }

    private void ClearResults()
    {
        _lastResult = null;
        btnExport.Enabled = false;
        lblTotalLinesValue.Text = "-";
        lblErrorCountValue.Text = "-";
        lblWarnCountValue.Text = "-";
        lblExceptionCountValue.Text = "-";
        lblSummary.Text = "Henüz analiz yapılmadı.";
        lblResultCount.Text = string.Empty;
        listEntries.Items.Clear();
    }

    private static string ResolveSamplesDirectory()
    {
        string[] candidates =
        [
            Path.Combine(AppContext.BaseDirectory, "samples"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "samples"),
            Path.Combine(Directory.GetCurrentDirectory(), "samples")
        ];

        foreach (var candidate in candidates)
        {
            var fullPath = Path.GetFullPath(candidate);
            if (Directory.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
