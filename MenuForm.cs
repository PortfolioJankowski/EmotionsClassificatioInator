using Services;

namespace PostGraduate_MachineLearning
{
    public partial class MenuForm : Form
    {
        private readonly IDataDownloader _dataDownloader;
        private readonly IEmotionClassifier _emotionClassifier;

        public MenuForm(IDataDownloader dataDownloader, IEmotionClassifier emotionClassifier)
        {
            InitializeComponent();
            this._dataDownloader = dataDownloader;
            this._emotionClassifier = emotionClassifier;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
