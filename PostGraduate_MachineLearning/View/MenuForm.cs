using EmotionClassifier.Configuration;
using EmotionClassifier.Models.Attributes;
using EmotionClassifier.Services;
using EmotionClassifier.View.Interfaces;
using Models;
using Services;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;


namespace PostGraduate_MachineLearning
{
    [ServiceRegistration(typeof(IMenuForm), Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public partial class MenuForm : Form, IMenuForm, INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler? PropertyChanged;

        public MenuForm(ILoggerService logger, AppSettingsProvider appSettings)
        {
            _appsettings = appSettings;
            InitializeComponent();
            InitialViewConfiguration();
            StartRandomQuoteTimer();
        }

        #region Private fields
        private AppSettingsProvider _appsettings;
        private string _statusLabelText;
        private bool _isProgressBarVisible;
        private int _progressBarValue;
        private ProgressBarStyle _progressBarDisplayStyle;
        private string _choosenParty;
        private bool _isDownloadButtonEnabled;
        private bool _isClassifyBtnEnabled;
        private long _progressBarMaximumValue;
        private string _randomQuoteText;
        private List<ClassificationResult> _models;
        #endregion
        #region Model properties

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? ChoosenParty
        {
            get => _choosenParty;
            set
            {
                if (_choosenParty != value)
                {
                    _choosenParty = value;
                    OnPropertyChanged(nameof(ChoosenParty));
                    IsDownloadBtnEnabled = !string.IsNullOrEmpty(_choosenParty);
                }
            }
        }
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
            get => _isClassifyBtnEnabled;
            set
            {
                _isClassifyBtnEnabled = value;
                OnPropertyChanged(nameof(IsClassifyBtnEnabled));
            }
        }


        public bool IsProgressBarVisible
        {
            get => _isProgressBarVisible;
            set
            {
                _isProgressBarVisible = value;
                OnPropertyChanged(nameof(IsProgressBarVisible));
            }
        }

        public int ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue));
            }
        }


        public bool IsDownloadBtnEnabled
        {
            get => _isDownloadButtonEnabled;
            set
            {
                _isDownloadButtonEnabled = value;
                OnPropertyChanged(nameof(IsDownloadBtnEnabled));
            }
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



        public string StatusLabelText
        {
            get => _statusLabelText;
            set
            {
                _statusLabelText = value;
                OnPropertyChanged(nameof(StatusLabelText));
            }
        }

        public ProgressBarStyle ProgressBarDisplayStyle
        {
            get => _progressBarDisplayStyle;
            set
            {
                _progressBarDisplayStyle = value;
                progressBar.Style = value; // Breakpoint here
                OnPropertyChanged(nameof(ProgressBarDisplayStyle));
            }
        }


        public long ProgressBarMaximumValue
        {
            get => _progressBarMaximumValue;
            set
            {
                _progressBarMaximumValue = value;
                OnPropertyChanged(nameof(ProgressBarMaximumValue));
            }
        }

        public string RandomQuoteText
        {
            get => _randomQuoteText;
            set
            {
                _randomQuoteText = value;
                OnPropertyChanged(nameof(RandomQuoteText));
            }
        }

        public List<ClassificationResult> ModelList
        {
            get => _models;
            set
            {
                _models = value;
                OnPropertyChanged(nameof(ModelList));
            }
        }

        #endregion Layout properties

        #region Layout methods
        public void CreatePieChart()
        {
            var groupedData = ModelList
                .GroupBy(x => x.Emotion)
                .Select(g => new { Emotion = g.Key, Count = g.Count() })
                .ToList();

            const string seriesName = "Series1";

            // Reset chart settings
            pieChart.Series[seriesName].Points.Clear();
            pieChart.Legends.Clear();
            pieChart.Titles.Clear();

            // Configure chart properties
            pieChart.Titles.Add("Emotions Distribution");
            pieChart.Series[seriesName].IsValueShownAsLabel = true;
            pieChart.Series[seriesName].ChartType = SeriesChartType.Pie;
            pieChart.Series[seriesName].IsVisibleInLegend = true;

            // Add legend
            pieChart.Legends.Add(new Legend("Legend1"));
            foreach (var item in groupedData)
            {
                pieChart.Series[seriesName].Points.AddXY(item.Emotion, item.Count);
            }
        }
        public void SetControlError(Control control, string message)
        {
            errProvider.SetError(control, message);
        }

        public void ClearErrorProvider()
        {
            errProvider.Clear();
        }

        public void StretchMenuForm(List<ClassificationResult> results)
        {
            if (results == null || results.Count == 0)
            {
                return;
            }
            this.Size = new Size(_appsettings.InitialFormWidth, _appsettings.FinalFormHeight);
        }


        #endregion

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

            this.progressBar.DataBindings.Add("Maximum", this, nameof(ProgressBarMaximumValue), true, DataSourceUpdateMode.OnPropertyChanged);
            this.statusLbl.DataBindings.Add("Text", this, nameof(StatusLabelText), true, DataSourceUpdateMode.OnPropertyChanged);
            this.progressBar.DataBindings.Add("Visible", this, nameof(IsProgressBarVisible), true, DataSourceUpdateMode.OnPropertyChanged);
            this.progressBar.DataBindings.Add("Value", this, nameof(ProgressBarValue), true, DataSourceUpdateMode.OnPropertyChanged);
            this.randomQuoteTxt.DataBindings.Add("Text", this, nameof(RandomQuoteText), true, DataSourceUpdateMode.OnPropertyChanged);
            this.classifyBtn.DataBindings.Add("Enabled", this, nameof(IsClassifyBtnEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            this.classifyGrid.DataBindings.Add("Visible", this, nameof(IsDataGridVisible), true, DataSourceUpdateMode.OnPropertyChanged);
            this.pieChart.DataBindings.Add("Visible", this, nameof(IsChartVisible), true, DataSourceUpdateMode.OnPropertyChanged);
            this.classifyGrid.DataBindings.Add("DataSource", this, nameof(ModelList), true, DataSourceUpdateMode.OnPropertyChanged);
            this.downloadBtn.DataBindings.Add("Enabled", this, nameof(IsDownloadBtnEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            this.partyNameTxt.DataBindings.Add("Text", this, nameof(ChoosenParty), true, DataSourceUpdateMode.OnPropertyChanged);
            var data = RandomQuoteProvider.GetTwitterAccounts().ToArray();
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