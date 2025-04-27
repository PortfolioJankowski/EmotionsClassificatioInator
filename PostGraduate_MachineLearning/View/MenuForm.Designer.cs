namespace PostGraduate_MachineLearning
{
    partial class MenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            downloadBtn = new Button();
            partyNameTxt = new TextBox();
            startPicker = new DateTimePicker();
            classifyGrid = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            classifyBtn = new Button();
            label4 = new Label();
            statusStrip = new StatusStrip();
            statusLbl = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            errProvider = new ErrorProvider(components);
            randomQuoteTimer = new System.Windows.Forms.Timer(components);
            randomQuoteTxt = new Label();
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            xlsxToolStripMenuItem = new ToolStripMenuItem();
            csvToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            pieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)classifyGrid).BeginInit();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pieChart).BeginInit();
            SuspendLayout();
            // 
            // downloadBtn
            // 
            downloadBtn.Location = new Point(21, 146);
            downloadBtn.Name = "downloadBtn";
            downloadBtn.Size = new Size(113, 26);
            downloadBtn.TabIndex = 0;
            downloadBtn.Text = "Download Tweets";
            downloadBtn.UseVisualStyleBackColor = true;
            
            // 
            // partyNameTxt
            // 
            partyNameTxt.AutoCompleteCustomSource.AddRange(new string[] { "pisorgpl", "Platforma_org", "trzaskowski_x", "NawrockiKn", "SlawomirMentzen", "szymon_holownia", "ZandbergRAZEM", "MagdaBiejat", "GrzegorzBraun_", "" });
            partyNameTxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partyNameTxt.Location = new Point(112, 83);
            partyNameTxt.Name = "partyNameTxt";
            partyNameTxt.Size = new Size(200, 23);
            partyNameTxt.TabIndex = 1;
            // 
            // startPicker
            // 
            startPicker.Format = DateTimePickerFormat.Short;
            startPicker.Location = new Point(112, 117);
            startPicker.Name = "startPicker";
            startPicker.Size = new Size(200, 23);
            startPicker.TabIndex = 2;
            // 
            // classifyGrid
            // 
            classifyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            classifyGrid.BackgroundColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            classifyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            classifyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            classifyGrid.Location = new Point(21, 242);
            classifyGrid.Name = "classifyGrid";
            classifyGrid.Size = new Size(409, 255);
            classifyGrid.TabIndex = 3;
  
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 83);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 4;
            label1.Text = "Political Group";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 123);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 4;
            label2.Text = "Start Date";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 36);
            label3.Name = "label3";
            label3.Size = new Size(418, 15);
            label3.TabIndex = 4;
            label3.Text = "Enter the name of the political party and the start date of the tweet collection. ";
            // 
            // classifyBtn
            // 
            classifyBtn.Location = new Point(21, 210);
            classifyBtn.Name = "classifyBtn";
            classifyBtn.Size = new Size(113, 26);
            classifyBtn.TabIndex = 0;
            classifyBtn.Text = "Clasify Tweets";
            classifyBtn.UseVisualStyleBackColor = true;

            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 51);
            label4.Name = "label4";
            label4.Size = new Size(384, 15);
            label4.TabIndex = 4;
            label4.Text = "Then press the Download Tweets button to download data from twitter.";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLbl, progressBar });
            statusStrip.Location = new Point(0, 541);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(732, 22);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            statusLbl.Name = "statusLbl";
            statusLbl.Size = new Size(39, 17);
            statusLbl.Text = "Ready";
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(100, 16);
            // 
            // errProvider
            // 
            errProvider.ContainerControl = this;
            // 
            // randomQuoteTimer
            // 

            // 
            // randomQuoteTxt
            // 
            randomQuoteTxt.AutoSize = true;
            randomQuoteTxt.Font = new Font("Yu Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            randomQuoteTxt.Location = new Point(21, 512);
            randomQuoteTxt.Name = "randomQuoteTxt";
            randomQuoteTxt.Size = new Size(0, 17);
            randomQuoteTxt.TabIndex = 4;
            // 
            // MenuStrip
            // 
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(732, 24);
            MenuStrip.TabIndex = 6;
            MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { xlsxToolStripMenuItem, csvToolStripMenuItem });
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(124, 22);
            saveAsToolStripMenuItem.Text = "&Save as ...";
            // 
            // xlsxToolStripMenuItem
            // 
            xlsxToolStripMenuItem.Name = "xlsxToolStripMenuItem";
            xlsxToolStripMenuItem.Size = new Size(95, 22);
            xlsxToolStripMenuItem.Text = ".xlsx";
            // 
            // csvToolStripMenuItem
            // 
            csvToolStripMenuItem.Name = "csvToolStripMenuItem";
            csvToolStripMenuItem.Size = new Size(95, 22);
            csvToolStripMenuItem.Text = ".csv";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(124, 22);
            exitToolStripMenuItem.Text = "E&xit";
           
            // 
            // pieChart
            // 
            chartArea1.Name = "ChartArea1";
            pieChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            pieChart.Legends.Add(legend1);
            pieChart.Location = new Point(436, 242);
            pieChart.Name = "pieChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            pieChart.Series.Add(series1);
            pieChart.Size = new Size(284, 255);
            pieChart.TabIndex = 7;
            pieChart.Text = "chart1";
            pieChart.Visible = false;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 563);
            Controls.Add(pieChart);
            Controls.Add(statusStrip);
            Controls.Add(MenuStrip);
            Controls.Add(randomQuoteTxt);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(classifyGrid);
            Controls.Add(startPicker);
            Controls.Add(partyNameTxt);
            Controls.Add(classifyBtn);
            Controls.Add(downloadBtn);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = MenuStrip;
            Name = "MenuForm";
            Text = "Classify Emotions";

            ((System.ComponentModel.ISupportInitialize)classifyGrid).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pieChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button downloadBtn;
        public TextBox partyNameTxt;
        private DateTimePicker startPicker;
        private DataGridView classifyGrid;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button classifyBtn;
        private Label label4;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLbl;
        private ToolStripProgressBar progressBar;
        private ErrorProvider errProvider;
        private System.Windows.Forms.Timer randomQuoteTimer;
        private Label randomQuoteTxt;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem xlsxToolStripMenuItem;
        private ToolStripMenuItem csvToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart pieChart;
    }
}
