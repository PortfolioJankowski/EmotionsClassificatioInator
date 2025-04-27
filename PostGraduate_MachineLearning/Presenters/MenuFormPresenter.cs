using EmotionClassifier.Configuration;
using EmotionClassifier.Models.Attributes;
using EmotionClassifier.Models.FormModels;
using EmotionClassifier.Presenters;
using EmotionClassifier.Services;
using EmotionClassifier.View.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Models;
using PostGraduate_MachineLearning;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Services.Presenters
{
    [ServiceRegistration(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class MenuFormPresenter : BasePresenter
    {
        public  IMenuForm _view;
        private  MenuFormModel _model;
        private IDataDownloader _dataDownloader;
        private Random _random = new();
        private List<string> _randomQuotes = RandomQuoteProvider.GetQuotes().ToList();
        private EmotionModelService _emotionPredictior;
        
        public MenuFormPresenter(IMenuForm view, MenuFormModel model, AppSettingsProvider appSettings,
            ILoggerService logger, IDataDownloader dataDownloader, EmotionModelService emotionPredictor)
            : base(appSettings, logger)
        {
            _view = view;
            _model = model;
            _dataDownloader = dataDownloader;
            _emotionPredictior = emotionPredictor;
            BindViewMethods();
        }

        #region Private methods
        private void InitializeModel()
        {
            _model = new MenuFormModel
            {
                ChoosenParty = _view.ChoosenParty,
                ChoosenStartDate = _view.ChoosenEndDate,
            };
        }

        private void BindViewMethods()
        {
            _view.Form_Load += Form_Load;
            _view.ClassifyBtn_Click += ClassifyBtn_Click;
            _view.RandomQuoteTimer_Tick += RandomQuoteTimer_Tick;
            _view.DownloadData_Click += DownloadData_Click;
            _view.ClassifyGrid_DoubleClick += ClassifyGrid_DoubleClick;
            _view.exitToolStripMenuItem_Click += ExitToolStripMenuItem_Click;

        }

        private void ExitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is Control ctrl)
            {
                if (ctrl.Parent is MenuForm menuForm)
                {
                    menuForm.Close();
                }
            } 
        }

        private void ClassifyGrid_DoubleClick(object? sender, EventArgs e)
        {
            if (sender is DataGridView dataGridView)
            {
                var selectedRow = dataGridView.CurrentCell.RowIndex;
                var selectedColumn = dataGridView.CurrentCell.ColumnIndex;
                var selectedCell = dataGridView.Rows[selectedRow].Cells[selectedColumn];
                MessageBox.Show($"{selectedCell.Value}", "Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

         
        }

        private async void DownloadData_Click(object? sender, EventArgs e)
        {
            InitializeModel();
            if (!_model.IsRequestValid())
            {
                _view.SetControlError(((MenuForm)_view).partyNameTxt, "Field cannot be empty!");
                return;
            }

            // Prepare UI
            if (_view is MenuForm form)
            {
                form.Invoke(new Action(() => {
                    form.ClearErrorProvider();
                    form.IsDownloadBtnEnabled = false;
                    form.IsProgressBarVisible = true;
                    form.StatusLabelText = "Downloading tweets...   ";
                    //form.ProgressBarStyle = ProgressBarStyle.Marquee;
                }));
            }
            

            try
            {
                var tweets = await _dataDownloader.DownloadTwitterDataAsync(_model);
                if (!tweets.Any())
                {
                    MessageBox.Show("No data found for indicated Tweeter account");
                    return;
                }
                _view.IsClassifyBtnEnabled = true;
                await ProcessTweetsAsync(tweets.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading tweets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _view.IsDownloadBtnEnabled = true;
                _view.IsProgressBarVisible = false;
                _view.StatusLabelText = "Download completed.";
            }
        }

        private async Task ProcessTweetsAsync(List<string> tweets)
        {
            _view.ProgressBarMaximumValue = tweets.Count;
            _view.ProgressBarValue = 0;

            var data = new List<ClassificationResult>();

            foreach (var tweet in tweets)
            {
                await Task.Delay(10); 
                data.Add(new ClassificationResult
                {
                    TweetText = tweet,   
                });
                _view.ProgressBarValue++;
            }

            _view.IsDataGridVisible = true;
            _view.StretchMenuForm(data);
            _view.ModelList = data;
        }
        private void RandomQuoteTimer_Tick(object? sender, EventArgs e)
        {
            _view.RandomQuoteText = _randomQuotes[_random.Next(_randomQuotes.Count)];
        }

        private async void ClassifyBtn_Click(object? sender, EventArgs e)
        {
           var data = _view.ModelList;

            var output = await Task.Run(() =>
            {
                return data.Select(row =>
                {
                    var prediction = _emotionPredictior.PredictionEngine.Predict(row);
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
            _view.ModelList = output;
            _view.CreatePieChart();
            _view.IsChartVisible = true;
        }

        

        private void Form_Load(object? sender, EventArgs e)
        {
            InitializeModel();
            _view.ChoosenParty = _model.ChoosenParty;
            _view.ChoosenEndDate = _model.ChoosenStartDate;
            _view.IsProgressBarVisible = false;
            _view.IsDownloadBtnEnabled = false;
            _view.IsClassifyBtnEnabled = false;
            _view.IsChartVisible = false;
            _view.IsDataGridVisible = false;
        }
        #endregion
    }
}
