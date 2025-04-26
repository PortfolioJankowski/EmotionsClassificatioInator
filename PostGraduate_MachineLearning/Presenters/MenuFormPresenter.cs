using EmotionClassifier.Configuration;
using EmotionClassifier.Models.FormModels;
using EmotionClassifier.Presenters;
using EmotionClassifier.View.Interfaces;
using Microsoft.Extensions.Logging;

namespace Services.Presenters
{
    public class MenuFormPresenter : BasePresenter
    {
        public  IMenuForm _view;
        private  MenuFormModel _model;
        public MenuFormPresenter(IMenuForm view, MenuFormModel model, AppSettings appSettings, ILoggerService logger) : base(appSettings, logger)
        {
            _view = view;
            _model = model;
            BindViewMethods();
        }

        #region Private methods
        private void InitializeModel()
        {
            _model = new MenuFormModel
            {
                ChoosenParty = string.Empty,
                ChoosenEndDate = DateTime.Now
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
            throw new NotImplementedException();
        }

        private void ClassifyGrid_DoubleClick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DownloadData_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RandomQuoteTimer_Tick(object? sender, EventArgs e)
        {
            Console.WriteLine("RandomQuoteTimer_Tick");
        }

        private void ClassifyBtn_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form_Load(object? sender, EventArgs e)
        {
            InitializeModel();
            _view.ChoosenParty = _model.ChoosenParty;
            _view.ChoosenEndDate = _model.ChoosenEndDate;
            _view.IsProgressBarVisible = false;
            _view.IsDownloadBtnEnabled = false;
            _view.IsClassifyBtnEnabled = false;
            _view.IsChartVisible = false;
            _view.IsDataGridVisible = false;
        }
        #endregion
    }
}
