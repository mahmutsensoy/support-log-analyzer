namespace SupportLogAnalyzer;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    private Button btnOpenFile = null!;
    private Button btnAnalyze = null!;
    private Button btnExport = null!;
    private Label lblFilePath = null!;
    private GroupBox grpSummary = null!;
    private Label lblTotalLines = null!;
    private Label lblTotalLinesValue = null!;
    private Label lblErrorCount = null!;
    private Label lblErrorCountValue = null!;
    private Label lblWarnCount = null!;
    private Label lblWarnCountValue = null!;
    private Label lblExceptionCount = null!;
    private Label lblExceptionCountValue = null!;
    private Label lblSummary = null!;
    private GroupBox grpFilters = null!;
    private CheckBox chkErrors = null!;
    private CheckBox chkWarnings = null!;
    private CheckBox chkExceptions = null!;
    private ListView listEntries = null!;
    private Label lblResultCount = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        btnOpenFile = new Button();
        btnAnalyze = new Button();
        btnExport = new Button();
        lblFilePath = new Label();
        grpSummary = new GroupBox();
        lblTotalLines = new Label();
        lblTotalLinesValue = new Label();
        lblErrorCount = new Label();
        lblErrorCountValue = new Label();
        lblWarnCount = new Label();
        lblWarnCountValue = new Label();
        lblExceptionCount = new Label();
        lblExceptionCountValue = new Label();
        lblSummary = new Label();
        grpFilters = new GroupBox();
        chkErrors = new CheckBox();
        chkWarnings = new CheckBox();
        chkExceptions = new CheckBox();
        listEntries = new ListView();
        lblResultCount = new Label();

        SuspendLayout();
        grpSummary.SuspendLayout();
        grpFilters.SuspendLayout();

        // btnOpenFile
        btnOpenFile.Location = new Point(12, 12);
        btnOpenFile.Size = new Size(130, 32);
        btnOpenFile.Text = "Dosya Seç";
        btnOpenFile.UseVisualStyleBackColor = true;
        btnOpenFile.Click += BtnOpenFile_Click;

        // btnAnalyze
        btnAnalyze.Enabled = false;
        btnAnalyze.Location = new Point(148, 12);
        btnAnalyze.Size = new Size(130, 32);
        btnAnalyze.Text = "Analiz Et";
        btnAnalyze.UseVisualStyleBackColor = true;
        btnAnalyze.Click += BtnAnalyze_Click;

        // btnExport
        btnExport.Enabled = false;
        btnExport.Location = new Point(284, 12);
        btnExport.Size = new Size(130, 32);
        btnExport.Text = "Raporu Kaydet";
        btnExport.UseVisualStyleBackColor = true;
        btnExport.Click += BtnExport_Click;

        // lblFilePath
        lblFilePath.AutoEllipsis = true;
        lblFilePath.BorderStyle = BorderStyle.FixedSingle;
        lblFilePath.Location = new Point(12, 52);
        lblFilePath.Size = new Size(760, 28);
        lblFilePath.Text = "Henüz dosya seçilmedi";
        lblFilePath.TextAlign = ContentAlignment.MiddleLeft;

        // grpSummary
        grpSummary.Controls.Add(lblTotalLines);
        grpSummary.Controls.Add(lblTotalLinesValue);
        grpSummary.Controls.Add(lblErrorCount);
        grpSummary.Controls.Add(lblErrorCountValue);
        grpSummary.Controls.Add(lblWarnCount);
        grpSummary.Controls.Add(lblWarnCountValue);
        grpSummary.Controls.Add(lblExceptionCount);
        grpSummary.Controls.Add(lblExceptionCountValue);
        grpSummary.Controls.Add(lblSummary);
        grpSummary.Location = new Point(12, 88);
        grpSummary.Size = new Size(360, 220);
        grpSummary.Text = "Özet";

        lblTotalLines.Location = new Point(16, 28);
        lblTotalLines.Size = new Size(120, 20);
        lblTotalLines.Text = "Toplam satır:";

        lblTotalLinesValue.Font = new Font(Font, FontStyle.Bold);
        lblTotalLinesValue.Location = new Point(140, 28);
        lblTotalLinesValue.Size = new Size(80, 20);
        lblTotalLinesValue.Text = "-";

        lblErrorCount.Location = new Point(16, 56);
        lblErrorCount.Size = new Size(120, 20);
        lblErrorCount.Text = "ERROR:";

        lblErrorCountValue.Font = new Font(Font, FontStyle.Bold);
        lblErrorCountValue.ForeColor = Color.DarkRed;
        lblErrorCountValue.Location = new Point(140, 56);
        lblErrorCountValue.Size = new Size(80, 20);
        lblErrorCountValue.Text = "-";

        lblWarnCount.Location = new Point(16, 84);
        lblWarnCount.Size = new Size(120, 20);
        lblWarnCount.Text = "WARN:";

        lblWarnCountValue.Font = new Font(Font, FontStyle.Bold);
        lblWarnCountValue.ForeColor = Color.DarkGoldenrod;
        lblWarnCountValue.Location = new Point(140, 84);
        lblWarnCountValue.Size = new Size(80, 20);
        lblWarnCountValue.Text = "-";

        lblExceptionCount.Location = new Point(16, 112);
        lblExceptionCount.Size = new Size(120, 20);
        lblExceptionCount.Text = "Exception:";

        lblExceptionCountValue.Font = new Font(Font, FontStyle.Bold);
        lblExceptionCountValue.ForeColor = Color.DarkMagenta;
        lblExceptionCountValue.Location = new Point(140, 112);
        lblExceptionCountValue.Size = new Size(80, 20);
        lblExceptionCountValue.Text = "-";

        lblSummary.Location = new Point(16, 144);
        lblSummary.Size = new Size(328, 64);
        lblSummary.Text = "Henüz analiz yapılmadı.";

        // grpFilters
        grpFilters.Controls.Add(chkErrors);
        grpFilters.Controls.Add(chkWarnings);
        grpFilters.Controls.Add(chkExceptions);
        grpFilters.Location = new Point(384, 88);
        grpFilters.Size = new Size(388, 220);
        grpFilters.Text = "Filtreler";

        chkErrors.Checked = true;
        chkErrors.Location = new Point(20, 32);
        chkErrors.Size = new Size(200, 24);
        chkErrors.Text = "ERROR / FATAL / CRITICAL";
        chkErrors.CheckedChanged += Filter_CheckedChanged;

        chkWarnings.Checked = true;
        chkWarnings.Location = new Point(20, 68);
        chkWarnings.Size = new Size(200, 24);
        chkWarnings.Text = "WARN / WARNING";
        chkWarnings.CheckedChanged += Filter_CheckedChanged;

        chkExceptions.Checked = true;
        chkExceptions.Location = new Point(20, 104);
        chkExceptions.Size = new Size(320, 24);
        chkExceptions.Text = "Exception ve stack trace satırları";
        chkExceptions.CheckedChanged += Filter_CheckedChanged;

        // listEntries
        listEntries.FullRowSelect = true;
        listEntries.GridLines = true;
        listEntries.Location = new Point(12, 344);
        listEntries.Size = new Size(760, 260);
        listEntries.View = View.Details;
        listEntries.Columns.Add("Satır", 60);
        listEntries.Columns.Add("Seviye", 90);
        listEntries.Columns.Add("İçerik", 590);

        // lblResultCount
        lblResultCount.AutoSize = true;
        lblResultCount.Location = new Point(12, 318);
        lblResultCount.Text = string.Empty;

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 616);
        Controls.Add(btnOpenFile);
        Controls.Add(btnAnalyze);
        Controls.Add(btnExport);
        Controls.Add(lblFilePath);
        Controls.Add(grpSummary);
        Controls.Add(grpFilters);
        Controls.Add(lblResultCount);
        Controls.Add(listEntries);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Support Log Analyzer";

        grpFilters.ResumeLayout(false);
        grpSummary.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
