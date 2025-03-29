using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.ML;
using Models;
using Services;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using Tensorflow;
using static TorchSharp.torch.nn;

namespace PostGraduate_MachineLearning
{
    public partial class MenuForm : Form
    {
        #region Private Variables
        private readonly IDataDownloader _dataDownloader;
        private readonly BindingSource _bindingSource;
        private PredictionEngine<ClassificationResult, EmotionPrediction> _predictionEngine;
        private UserTweetRequest _tweetRequest = new();
        private MLContext _mlContext = new MLContext();
        private ITransformer _model;
        private NameValueCollection _appSettings = ConfigurationManager.AppSettings;
        #endregion

        #region Initialization, Form Loading
        public MenuForm(IDataDownloader dataDownloader)
        {
            InitializeComponent();
            _dataDownloader = dataDownloader;
            _bindingSource = new BindingSource();
        }

        private async void MenuForm_Load(object sender, EventArgs e)
        {
            var initialHeight = int.Parse(_appSettings["initialFormHeight"]!);
            var initialWidth = int.Parse(_appSettings["initialFormWidth"]!);  
            this.Size = new Size(initialWidth, initialHeight); //Initial form size
            AddDataBindings();
            progressBar.Visible = false;
            randomQuoteTimer.Interval = 3000;
            randomQuoteTimer.Start();
            classifyBtn.Enabled = false;

            await Task.Run(LoadModelAsync); //Action Delegate
        }

        private void AddDataBindings()
        {
            partyNameTxt.DataBindings.Add("Text", _tweetRequest, "PoliticalGroup");
            startPicker.DataBindings.Add("Value", _tweetRequest, "StartDate");
            classifyGrid.DataSource = _bindingSource;

            var data = MessageProvider.GetTwitterAccounts().ToArray();  
            AutoCompleteStringCollection source = new();
            source.AddRange(data);
            partyNameTxt.AutoCompleteCustomSource = source;
            partyNameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partyNameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            startPicker.MinDate = DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// Loading ML.NET Classification model
        /// </summary>
        private async Task LoadModelAsync()
        {
            try
            {
                UpdateStatusLabelFromAnotherThread("Loading ML model...");
                var modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "emotionModel.zip");
                _model = _mlContext.Model.Load(modelPath, out _);
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<ClassificationResult, EmotionPrediction>(_model);
                statusLbl.Text = "Ready ...";
                UpdateStatusLabelFromAnotherThread("ML Model Loaded");
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
        #endregion

        #region Event Handlers
        private async void downloadBtn_Click(object sender, EventArgs e)
        {
            if (!_tweetRequest.IsRequestValid())
            {
                errProvider.SetError(partyNameTxt, "Field cannot be empty!");
                return;
            }

            errProvider.Clear();
            PrepareUIForDownload();

            try
            {
                var tweets = await _dataDownloader.DownloadTwitterDataAsync(_tweetRequest);
                if (!tweets.Any())
                {
                    MessageBox.Show("No data found for indicated Tweeter account");
                    return;
                }
                classifyBtn.Enabled = true;
                await ProcessTweetsAsync(tweets.ToList());
                ShowTweetsDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading tweets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ResetUIAfterDownload();
            }
        }

        private void classifyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = classifyGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                MessageBox.Show(cellValue.ToString(), "Details", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            var quotes = MessageProvider.GetQuotes().ToList();
            int index = random.Next(quotes.Count);
            randomQuoteTxt.Text = quotes[index];
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region UI Updates - easy interactions with UI elements
        private void ShowTweetsDataGrid()
        {
            var finalHeight = int.Parse(_appSettings["finalFormHeight"]!);
            this.Height = finalHeight;
        }

        private void UpdateStatusLabelFromAnotherThread(string message)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => statusLbl.Text = message));
            }
            else
            {
                statusLbl.Text = message;
            }
        }

        private void PrepareUIForDownload()
        {
            downloadBtn.Enabled = false;
            statusLbl.Text = "Downloading data ...";
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
        }

        private void ResetUIAfterDownload()
        {
            downloadBtn.Enabled = true;
            progressBar.Visible = false;
            statusLbl.Text = "Download complete";
        }
        #endregion

        #region Tweets Processing

        private async Task ProcessTweetsAsync(List<string> tweets)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Maximum = tweets.Count;
            progressBar.Value = 0;

            var data = tweets
                .Select(tweet => new ClassificationResult { TweetText = tweet })
                .ToList();

            _bindingSource.DataSource = data;

            for (int i = 0; i < tweets.Count; i++)
            {
                progressBar.Value = i + 1;
                if (i % 10 == 0) // Update UI every 10 iterations
                {
                    await Task.Delay(10);
                }
            }
        }

        private async void classifyBtn_Click(object sender, EventArgs e)
        {
            var data = (List<ClassificationResult>)_bindingSource.DataSource;

            var output = await Task.Run(() =>
            {
                return data.Select(row =>
                {
                    var prediction = _predictionEngine.Predict(row);
                    var cleanEmotion = prediction.PredictedEmotion?.Trim().Replace(";", string.Empty);

                    if (cleanEmotion != null)
                    {
                        if (cleanEmotion.StartsWith("neutralny", StringComparison.OrdinalIgnoreCase))
                            cleanEmotion = "neutralny";
                        else if (cleanEmotion.StartsWith("negatywn", StringComparison.OrdinalIgnoreCase))
                            cleanEmotion = "negatywny";
                        else if (cleanEmotion.StartsWith("poz", StringComparison.OrdinalIgnoreCase))
                            cleanEmotion = "pozytywny";
                    }

                    return new ClassificationResult
                    {
                        TweetText = row.TweetText,
                        Emotion = cleanEmotion!,
                    };
                }).ToList();
            });

            _bindingSource.DataSource = output;
            CreatePieChart();
        }

        /// <summary>
        /// Creates data visualization for emotions distribution. It takes data from the grid view
        /// </summary>
        private void CreatePieChart()
        {
            pieChart.Visible = true;
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

            // Process data
            if (_bindingSource.DataSource is List<ClassificationResult> data)
            {
                var groupedData = data.GroupBy(d => d.Emotion)
                                      .Select(g => new { Emotion = g.Key, Count = g.Count() });

                foreach (var item in groupedData)
                {
                    pieChart.Series[seriesName].Points.AddXY(item.Emotion, item.Count);
                }
            }
        }
        #endregion
    }
}
