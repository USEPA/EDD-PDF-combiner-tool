

namespace Automatic_PDF_Combiner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnBrowse = new Button();
            label1 = new Label();
            txtFolderPath = new TextBox();
            btnCombine = new Button();
            notifyIcon1 = new NotifyIcon(components);
            groupBox1 = new GroupBox();
            cancelBtn = new Button();
            combineOptionInfoLbl = new Label();
            label5 = new Label();
            combineOptionComboBox = new ComboBox();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            progressBar = new ProgressBar();
            lblProgressCount = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            fileNameLbl = new Label();
            statusTextBox = new TextBox();
            label4 = new Label();
            helpIconToolTip = new ToolTip(components);
            toolTip = new PictureBox();
            toolTipLbl = new Label();
            helpTextToolTip = new ToolTip(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)toolTip).BeginInit();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.BackColor = SystemColors.ButtonHighlight;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.Image = (Image)resources.GetObject("btnBrowse.Image");
            btnBrowse.Location = new Point(680, 39);
            btnBrowse.Margin = new Padding(4, 3, 4, 3);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(37, 28);
            btnBrowse.TabIndex = 0;
            helpTextToolTip.SetToolTip(btnBrowse, "Browse for a folder containing PDFs");
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 47);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 1;
            label1.Text = "Folder Path";
            // 
            // txtFolderPath
            // 
            txtFolderPath.BackColor = SystemColors.ControlLightLight;
            txtFolderPath.Location = new Point(124, 43);
            txtFolderPath.Margin = new Padding(4, 3, 4, 3);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(549, 23);
            txtFolderPath.TabIndex = 2;
            // 
            // btnCombine
            // 
            btnCombine.BackColor = Color.Honeydew;
            btnCombine.FlatStyle = FlatStyle.Flat;
            btnCombine.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCombine.ForeColor = SystemColors.ActiveCaptionText;
            btnCombine.Location = new Point(123, 181);
            btnCombine.Margin = new Padding(4, 3, 4, 3);
            btnCombine.Name = "btnCombine";
            btnCombine.Size = new Size(550, 35);
            btnCombine.TabIndex = 3;
            btnCombine.Text = "Start Combining PDFs";
            btnCombine.UseVisualStyleBackColor = false;
            btnCombine.Click += btnCombine_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(cancelBtn);
            groupBox1.Controls.Add(combineOptionInfoLbl);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(combineOptionComboBox);
            groupBox1.Controls.Add(btnCombine);
            groupBox1.Controls.Add(txtFolderPath);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnBrowse);
            groupBox1.Location = new Point(14, 70);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(760, 228);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Combine Settings";
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = Color.Transparent;
            cancelBtn.Enabled = false;
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Image = (Image)resources.GetObject("cancelBtn.Image");
            cancelBtn.Location = new Point(682, 181);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(35, 35);
            cancelBtn.TabIndex = 7;
            helpTextToolTip.SetToolTip(cancelBtn, "Cancel the PDF combining process");
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // combineOptionInfoLbl
            // 
            combineOptionInfoLbl.AutoSize = true;
            combineOptionInfoLbl.ForeColor = SystemColors.ControlDarkDark;
            combineOptionInfoLbl.Location = new Point(120, 102);
            combineOptionInfoLbl.Margin = new Padding(4, 0, 4, 0);
            combineOptionInfoLbl.Name = "combineOptionInfoLbl";
            combineOptionInfoLbl.Size = new Size(128, 15);
            combineOptionInfoLbl.TabIndex = 6;
            combineOptionInfoLbl.Text = "combineOptionInfoLbl";
            combineOptionInfoLbl.Visible = false;
            // 
            // label5
            // 
            label5.AutoEllipsis = true;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(21, 74);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 5;
            label5.Text = "Combine Option";
            // 
            // combineOptionComboBox
            // 
            combineOptionComboBox.FormattingEnabled = true;
            combineOptionComboBox.Items.AddRange(new object[] { "Single PDF", "Max of 100 MB" });
            combineOptionComboBox.Location = new Point(124, 74);
            combineOptionComboBox.Margin = new Padding(4, 3, 4, 3);
            combineOptionComboBox.Name = "combineOptionComboBox";
            combineOptionComboBox.Size = new Size(549, 23);
            combineOptionComboBox.TabIndex = 4;
            combineOptionComboBox.Text = " --Select--";
            combineOptionComboBox.SelectedIndexChanged += combineOptionComboBox_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(7, 573);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(34, 40);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.FlatStyle = FlatStyle.System;
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(25, 573);
            label2.Margin = new Padding(0, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(256, 15);
            label2.TabIndex = 10;
            label2.Text = "The US Environmental Protection Agency (EPA)";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(558, 2);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(218, 72);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(123, 194);
            progressBar.Margin = new Padding(4, 3, 4, 3);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(550, 22);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 6;
            // 
            // lblProgressCount
            // 
            lblProgressCount.AutoSize = true;
            lblProgressCount.BackColor = SystemColors.ButtonHighlight;
            lblProgressCount.FlatStyle = FlatStyle.Flat;
            lblProgressCount.Location = new Point(120, 219);
            lblProgressCount.Margin = new Padding(4, 0, 4, 0);
            lblProgressCount.Name = "lblProgressCount";
            lblProgressCount.Size = new Size(37, 15);
            lblProgressCount.TabIndex = 7;
            lblProgressCount.Text = "x of x ";
            lblProgressCount.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 51);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 15;
            label3.Text = "Status:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(fileNameLbl);
            groupBox2.Controls.Add(statusTextBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(lblProgressCount);
            groupBox2.Controls.Add(progressBar);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(14, 304);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(757, 256);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Progress Status";
            // 
            // fileNameLbl
            // 
            fileNameLbl.AutoSize = true;
            fileNameLbl.Location = new Point(120, 234);
            fileNameLbl.Margin = new Padding(4, 0, 4, 0);
            fileNameLbl.Name = "fileNameLbl";
            fileNameLbl.Size = new Size(55, 15);
            fileNameLbl.TabIndex = 18;
            fileNameLbl.Text = "NameLbl";
            fileNameLbl.Visible = false;
            // 
            // statusTextBox
            // 
            statusTextBox.BackColor = SystemColors.ButtonHighlight;
            statusTextBox.Location = new Point(121, 30);
            statusTextBox.Margin = new Padding(4, 3, 4, 3);
            statusTextBox.Multiline = true;
            statusTextBox.Name = "statusTextBox";
            statusTextBox.ReadOnly = true;
            statusTextBox.ScrollBars = ScrollBars.Both;
            statusTextBox.Size = new Size(549, 137);
            statusTextBox.TabIndex = 17;
            statusTextBox.WordWrap = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 194);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 16;
            label4.Text = "Progress:";
            // 
            // helpIconToolTip
            // 
            helpIconToolTip.AutomaticDelay = 1000;
            helpIconToolTip.BackColor = SystemColors.InfoText;
            helpIconToolTip.ShowAlways = true;
            helpIconToolTip.UseAnimation = false;
            // 
            // toolTip
            // 
            toolTip.Image = (Image)resources.GetObject("toolTip.Image");
            toolTip.Location = new Point(10, 16);
            toolTip.Name = "toolTip";
            toolTip.Size = new Size(24, 25);
            toolTip.TabIndex = 17;
            toolTip.TabStop = false;
            // 
            // toolTipLbl
            // 
            toolTipLbl.AutoSize = true;
            toolTipLbl.Location = new Point(34, 21);
            toolTipLbl.Name = "toolTipLbl";
            toolTipLbl.Size = new Size(86, 15);
            toolTipLbl.TabIndex = 18;
            toolTipLbl.Text = "About this tool";
            // 
            // helpTextToolTip
            // 
            helpTextToolTip.AutoPopDelay = 1200000;
            helpTextToolTip.InitialDelay = 500;
            helpTextToolTip.ReshowDelay = 100;
            helpTextToolTip.ShowAlways = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HighlightText;
            ClientSize = new Size(789, 592);
            Controls.Add(toolTipLbl);
            Controls.Add(toolTip);
            Controls.Add(groupBox2);
            Controls.Add(pictureBox2);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Auto PDF Combiner";
            TransparencyKey = SystemColors.ActiveBorder;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)toolTip).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgressCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combineOptionComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label combineOptionInfoLbl;
        private System.Windows.Forms.Label fileNameLbl;
        private ToolTip helpIconToolTip;
        private PictureBox toolTip;
        private Label toolTipLbl;
        private ToolTip helpTextToolTip;
        private Button cancelBtn;
    }
}

