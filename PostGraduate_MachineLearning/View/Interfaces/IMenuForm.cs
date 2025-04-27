using EmotionClassifier.Services;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.View.Interfaces
{
    public interface IMenuForm
    {
        public string? ChoosenParty { get; set; }
        public DateTime ChoosenEndDate { get; set; }
        public string[] DataGridColumns { get; set; } 
        public string[] DataGridRows { get; set; } 
        public bool IsClassifyBtnEnabled { get; set; } 
        public bool IsDownloadBtnEnabled { get; set; }
        public bool IsProgressBarVisible { get; set; } 
        public int ProgressBarValue { get; set; }
        public bool IsChartVisible { get; set; }
        public bool IsDataGridVisible { get; set; }
        public long ProgressBarMaximumValue { get; set; }
        public string StatusLabelText { get; set; }
        public List<ClassificationResult> ModelList { get; set; }   
        public string RandomQuoteText { get; set; }

        event EventHandler ClassifyBtn_Click;
        event EventHandler RandomQuoteTimer_Tick;
        event EventHandler Form_Load;
        event EventHandler DownloadData_Click;
        event EventHandler ClassifyGrid_DoubleClick;
        event EventHandler exitToolStripMenuItem_Click;
        void SetControlError(Control control, string message);
        void ClearErrorProvider();
        void StretchMenuForm(List<ClassificationResult> results);
        void CreatePieChart();

    }
}
