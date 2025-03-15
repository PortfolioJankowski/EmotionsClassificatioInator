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
            downloadBtn = new Button();
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            classifyBtn = new Button();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            // 
            // textBox1
            // 
            textBox1.Location = new Point(112, 83);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 23);
            textBox1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(112, 117);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 242);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(426, 209);
            dataGridView1.TabIndex = 3;
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
            label3.Click += label3_Click;
            // 
            // classifyBtn
            // 
            classifyBtn.Location = new Point(12, 210);
            classifyBtn.Name = "classifyBtn";
            classifyBtn.Size = new Size(113, 26);
            classifyBtn.TabIndex = 0;
            classifyBtn.Text = "Clasify Tweets";
            classifyBtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 36);
            label4.Name = "label4";
            label4.Size = new Size(384, 15);
            label4.TabIndex = 4;
            label4.Text = "Then press the Download Tweets button to download data from twitter.";
            label4.Click += label3_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 450);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox1);
            Controls.Add(classifyBtn);
            Controls.Add(downloadBtn);
            Name = "MenuForm";
            Text = "Classify Emotions";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button downloadBtn;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button classifyBtn;
        private Label label4;
    }
}
