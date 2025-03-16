using Microsoft.ML;
using Models;
using Services;
using System.Diagnostics;
using Tensorflow;
using static TorchSharp.torch.nn;

namespace PostGraduate_MachineLearning
{
    public partial class MenuForm : Form
    {
        private readonly IDataDownloader _dataDownloader;
        private readonly BindingSource _bindingSource;
        private PredictionEngine<ClassificationResult, EmotionPrediction> _predictionEngine;
        private UserTweetRequest _tweetRequest = new();
        private MLContext _mlContext = new MLContext();
        private ITransformer _model;


        public MenuForm(IDataDownloader dataDownloader)
        {
            InitializeComponent();
            _dataDownloader = dataDownloader;
            _bindingSource = new BindingSource();
        }

        private async void MenuForm_Load(object sender, EventArgs e)
        {
            AddDataBindings();
            progressBar.Visible = false;
            randomQuoteTimer.Interval = 3000;
            randomQuoteTimer.Start();
            classifyBtn.Enabled = false;

            await Task.Run(LoadModelAsync); //Action Delegate
        }

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

        private void UpdateStatusLabelFromAnotherThread(string message)
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new Action(() => statusLbl.Text = message));
            }
            else
            {
                statusLbl.Text = message;
            }
        }

        /// <summary>
        /// Prepares the UI for downloading data
        /// </summary>
        private void PrepareUIForDownload()
        {
            downloadBtn.Enabled = false;
            statusLbl.Text = "Downloading data ...";
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
        }

        /// <summary>
        /// Configuring form data binding mechanism
        /// </summary>
        private void AddDataBindings()
        {
            partyNameTxt.DataBindings.Add("Text", _tweetRequest, "PoliticalGroup");
            startPicker.DataBindings.Add("Value", _tweetRequest, "StartDate");
            classifyGrid.DataSource = _bindingSource;

            var data = new string[] { "pisorgpl", "Platforma_org", "trzaskowski_x", "NawrockiKn", "SlawomirMentzen", "szymon_holownia", "ZandbergRAZEM", "MagdaBiejat", "GrzegorzBraun_" };
            AutoCompleteStringCollection source = new();
            source.AddRange(data);
            partyNameTxt.AutoCompleteCustomSource = source;
            partyNameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partyNameTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            startPicker.MinDate = DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// Processes tweets and updates UI accordingly
        /// </summary>
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

        /// <summary>
        /// Resets the UI after downloading is complete
        /// </summary>
        private void ResetUIAfterDownload()
        {
            downloadBtn.Enabled = true;
            progressBar.Visible = false;
            statusLbl.Text = "Download complete";
        }

        private void classifyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = classifyGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                MessageBox.Show(cellValue.ToString(), "Details", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private List<string> quotes = new List<string>
        {
            "Jestem za, a nawet przeciw. – Lech Wa³êsa",
            "Nie chcem, ale muszem. – Lech Wa³êsa",
            "Balcerowicz musi odejœæ! – Andrzej Lepper",
            "Spieprzaj dziadu! – Jaros³aw Kaczyñski",
            "Nicea albo œmieræ! – Jan Maria Rokita",
            "Plusy dodatnie, plusy ujemne. – Lech Wa³êsa",
            "Warto byæ przyzwoitym. – W³adys³aw Bartoszewski",
            "Prawdziwego mê¿czyznê poznaje siê nie po tym, jak zaczyna, ale jak koñczy. – Leszek Miller",
            "Nie bêdzie Niemiec plu³ nam w twarz. – Andrzej Duda",
            "Yes, yes, yes! – Kazimierz Marcinkiewicz"
        };
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int index = random.Next(quotes.Count);
            randomQuoteTxt.Text = quotes[index];
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
                    return new ClassificationResult
                    {
                        TweetText = row.TweetText,
                        Emotion = cleanEmotion!,
                    };
                }).ToList();
            });
            _bindingSource.DataSource = output;  
        }
    }
}
