using Models;
using Services;

namespace PostGraduate_MachineLearning
{
    public partial class MenuForm : Form
    {
        private readonly IDataDownloader _dataDownloader;
        private readonly IEmotionClassifier _emotionClassifier;
        private UserTweetRequest _tweetRequest = new();

        public MenuForm(IDataDownloader dataDownloader, IEmotionClassifier emotionClassifier)
        {
            InitializeComponent();
            this._dataDownloader = dataDownloader;
            this._emotionClassifier = emotionClassifier;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            AddDataBindings();
        }

        private async void downloadBtn_Click(object sender, EventArgs e)
        {
            var tweets = await _dataDownloader.DownloadTwitterDataAsync(_tweetRequest);     
        }




        private void AddDataBindings()
        {
            partyName.DataBindings.Add("Text", _tweetRequest, "PoliticalGroup");
            startPicker.DataBindings.Add("Value", _tweetRequest, "StartDate");
        }
    }
}
