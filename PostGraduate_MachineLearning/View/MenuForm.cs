using EmotionClassifier.Configuration;
using EmotionClassifier.Services;
using EmotionClassifier.View.Interfaces;
using Services;


namespace PostGraduate_MachineLearning
{
    public partial class MenuForm : Form, IMenuForm
    {
        private AppSettings _appsettings;
        public MenuForm(ILoggerService logger, AppSettings appSettings)
        {
            _appsettings = appSettings;
            InitializeComponent();
            InitialViewConfiguration();
            StartRandomQuoteTimer();

        }

        #region Model properties

        public string? ChoosenParty { get => this.partyNameTxt.Text; set => this.partyNameTxt.Text = value; }
        public DateTime ChoosenEndDate { get => this.startPicker.Value; set => this.startPicker.Value = value; }

        #endregion Model properties

        #region Layout properties

        public string[] DataGridColumns
        {
            get
            {
                if (classifyGrid.ColumnCount > 0)
                {
                    return classifyGrid.Columns.Cast<DataGridViewColumn>()
                        .Select(col => col.HeaderText)
                        .ToArray();
                }
                return null;
            }
            set
            {
                classifyGrid.Columns.Clear();
                if (value != null)
                {
                    foreach (var columnHeader in value)
                    {
                        classifyGrid.Columns.Add(columnHeader, columnHeader);
                    }
                }
            }
        }

        public string[] DataGridRows
        {
            get
            {
                if (classifyGrid.Rows.Count > 0)
                {
                    return classifyGrid.Rows.Cast<DataGridViewRow>()
                        .Select(row => string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString())))
                        .ToArray();
                }
                return null;
            }
            set
            {
                classifyGrid.Rows.Clear();
                if (value != null)
                {
                    foreach (var rowData in value)
                    {
                        classifyGrid.Rows.Add(rowData.Split(';'));
                    }
                }
            }
        }

        public bool IsClassifyBtnEnabled
        {
            get => classifyBtn.Enabled;
            set => classifyBtn.Enabled = value;
        }

        public bool IsProgressBarVisible
        {
            get => progressBar.Visible;
            set => progressBar.Visible = value;
        }

        public int ProgressBarValue
        {
            get => progressBar.Value;
            set => progressBar.Value = value;
        }

        public bool IsDownloadBtnEnabled
        {
            get => downloadBtn.Enabled;
            set => downloadBtn.Enabled = value;
        }

        public bool IsChartVisible
        {
            get => pieChart.Visible;
            set => pieChart.Visible = value;
        }

        public bool IsDataGridVisible
        {
            get => classifyGrid.Visible;
            set => classifyGrid.Visible = value;
        }

        #endregion Layout properties

        #region EventHandlers

        public event EventHandler ClassifyGrid_DoubleClick
        {
            add => classifyGrid.DoubleClick += value;
            remove => classifyGrid.DoubleClick -= value;
        }

        public event EventHandler exitToolStripMenuItem_Click
        {
            add => exitToolStripMenuItem.Click += value;
            remove => exitToolStripMenuItem.Click -= value;
        }

        public event EventHandler ClassifyBtn_Click
        {
            add => classifyBtn.Click += value;
            remove => classifyBtn.Click -= value;
        }

        public event EventHandler Form_Load
        {
            add => this.Load += value;
            remove => this.Load -= value;
        }

        public event EventHandler RandomQuoteTimer_Tick
        {
            add => this.randomQuoteTimer.Tick += value;
            remove => this.randomQuoteTimer.Tick -= value;
        }

        public event EventHandler DownloadData_Click
        {
            add => downloadBtn.Click += value;
            remove => downloadBtn.Click -= value;
        }

        #endregion EventHandlers

        #region Private methods
        private void StartRandomQuoteTimer()
        {
            randomQuoteTimer.Interval = 3000;
            randomQuoteTimer.Start();
        }

        private void InitialViewConfiguration()
        {
            var data = MessageProvider.GetTwitterAccounts().ToArray();
            AutoCompleteStringCollection source = new();
            source.AddRange(data);
            partyNameTxt.AutoCompleteCustomSource = source;
            partyNameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partyNameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            startPicker.MinDate = DateTime.Now.AddDays(-7);
            this.Size = new Size(_appsettings.InitialFormWidth, _appsettings.InitialFormHeight);
        }

        #endregion Private methods
    }
}