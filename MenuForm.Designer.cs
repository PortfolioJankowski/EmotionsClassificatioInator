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
            downloadBtn = new Button();
            partyNameTxt = new TextBox();
            startPicker = new DateTimePicker();
            classifyGrid = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            classifyBtn = new Button();
            label4 = new Label();
            statusStrip1 = new StatusStrip();
            statusLbl = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            errProvider = new ErrorProvider(components);
            randomQuoteTimer = new System.Windows.Forms.Timer(components);
            randomQuoteTxt = new Label();
            ((System.ComponentModel.ISupportInitialize)classifyGrid).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            SuspendLayout();
            // 
            // downloadBtn
            // 
            downloadBtn.Location = new Point(76, 164);
            downloadBtn.Name = "downloadBtn";
            downloadBtn.Size = new Size(113, 26);
            downloadBtn.TabIndex = 0;
            downloadBtn.Text = "Download Tweets";
            downloadBtn.UseVisualStyleBackColor = true;
            downloadBtn.Click += downloadBtn_Click;
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
            classifyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            classifyGrid.Location = new Point(12, 242);
            classifyGrid.Name = "classifyGrid";
            classifyGrid.Size = new Size(497, 223);
            classifyGrid.TabIndex = 3;
            classifyGrid.CellDoubleClick += classifyGrid_CellDoubleClick;
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
            label3.Location = new Point(12, 21);
            label3.Name = "label3";
            label3.Size = new Size(418, 15);
            label3.TabIndex = 4;
            label3.Text = "Enter the name of the political party and the start date of the tweet collection. ";
            // 
            // classifyBtn
            // 
            classifyBtn.Location = new Point(12, 210);
            classifyBtn.Name = "classifyBtn";
            classifyBtn.Size = new Size(113, 26);
            classifyBtn.TabIndex = 0;
            classifyBtn.Text = "Clasify Tweets";
            classifyBtn.UseVisualStyleBackColor = true;
            classifyBtn.Click += classifyBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 36);
            label4.Name = "label4";
            label4.Size = new Size(384, 15);
            label4.TabIndex = 4;
            label4.Text = "Then press the Download Tweets button to download data from twitter.";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLbl, progressBar });
            statusStrip1.Location = new Point(0, 522);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(517, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
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
            randomQuoteTimer.Tick += timer1_Tick;
            // 
            // randomQuoteTxt
            // 
            randomQuoteTxt.AutoSize = true;
            randomQuoteTxt.Font = new Font("Yu Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            randomQuoteTxt.Location = new Point(21, 479);
            randomQuoteTxt.Name = "randomQuoteTxt";
            randomQuoteTxt.Size = new Size(0, 25);
            randomQuoteTxt.TabIndex = 4;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 544);
            Controls.Add(statusStrip1);
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
            Name = "MenuForm";
            Text = "Classify Emotions";
            Load += MenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)classifyGrid).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button downloadBtn;
        private TextBox partyNameTxt;
        private DateTimePicker startPicker;
        private DataGridView classifyGrid;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button classifyBtn;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLbl;
        private ToolStripProgressBar progressBar;
        private ErrorProvider errProvider;
        private System.Windows.Forms.Timer randomQuoteTimer;
        private Label randomQuoteTxt;
    }
}
