namespace ACE.Trading.Data.Graphics
{
    partial class PredictionView
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
            formsPlot1 = new ScottPlot.FormsPlot();
            predictBtn = new Button();
            modelIdCombo = new ComboBox();
            modelIdLabel = new Label();
            createNew = new GroupBox();
            promptNumLabel = new Label();
            promptNum = new NumericUpDown();
            menuStrip2 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            openTrainingFileToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem1 = new ToolStripMenuItem();
            clearGraphBtn = new ToolStripMenuItem();
            marketHistoryToolStripMenuItem = new ToolStripMenuItem();
            symbolCombo = new ToolStripComboBox();
            timeIntervalCombo = new ToolStripComboBox();
            durationCombo = new ToolStripComboBox();
            collectedDataToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            addToSymbolListToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            trainingTab = new TabPage();
            fineTuneModelCombo = new ComboBox();
            trainExistingRadioBtn = new RadioButton();
            trainNewRadioBtn = new RadioButton();
            button1 = new Button();
            numericUpDown5 = new NumericUpDown();
            label6 = new Label();
            numericUpDown4 = new NumericUpDown();
            label2 = new Label();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            numericUpDown3 = new NumericUpDown();
            label4 = new Label();
            label7 = new Label();
            label5 = new Label();
            modelSuffixTextBox = new TextBox();
            filesListBox = new ListBox();
            slopeTab = new TabPage();
            groupBox1 = new GroupBox();
            label13 = new Label();
            timeIntervalCombo2 = new ComboBox();
            trainingCompletionNum = new NumericUpDown();
            label11 = new Label();
            trainingPromptNum = new NumericUpDown();
            label10 = new Label();
            button3 = new Button();
            label9 = new Label();
            label8 = new Label();
            finishDateTime = new DateTimePicker();
            startDateTime = new DateTimePicker();
            genAndUploadBtn = new Button();
            tolleranceNum = new NumericUpDown();
            label3 = new Label();
            tradingTab = new TabPage();
            predictionsTab = new TabPage();
            createNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).BeginInit();
            menuStrip2.SuspendLayout();
            tabControl1.SuspendLayout();
            trainingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            slopeTab.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trainingCompletionNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trainingPromptNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tolleranceNum).BeginInit();
            predictionsTab.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.Location = new Point(291, 34);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(496, 501);
            formsPlot1.TabIndex = 0;
            // 
            // predictBtn
            // 
            predictBtn.Location = new Point(184, 62);
            predictBtn.Name = "predictBtn";
            predictBtn.Size = new Size(75, 23);
            predictBtn.TabIndex = 3;
            predictBtn.Text = "Predict";
            predictBtn.UseVisualStyleBackColor = true;
            predictBtn.Click += predictBtn_Click;
            // 
            // modelIdCombo
            // 
            modelIdCombo.FormattingEnabled = true;
            modelIdCombo.Location = new Point(6, 35);
            modelIdCombo.Name = "modelIdCombo";
            modelIdCombo.Size = new Size(253, 23);
            modelIdCombo.TabIndex = 4;
            // 
            // modelIdLabel
            // 
            modelIdLabel.AutoSize = true;
            modelIdLabel.Location = new Point(6, 17);
            modelIdLabel.Name = "modelIdLabel";
            modelIdLabel.Size = new Size(57, 15);
            modelIdLabel.TabIndex = 5;
            modelIdLabel.Text = "Model Id:";
            // 
            // createNew
            // 
            createNew.Controls.Add(promptNumLabel);
            createNew.Controls.Add(promptNum);
            createNew.Controls.Add(modelIdCombo);
            createNew.Controls.Add(predictBtn);
            createNew.Controls.Add(modelIdLabel);
            createNew.Location = new Point(6, 3);
            createNew.Name = "createNew";
            createNew.Size = new Size(265, 93);
            createNew.TabIndex = 6;
            createNew.TabStop = false;
            createNew.Text = "Fine Tunes";
            // 
            // promptNumLabel
            // 
            promptNumLabel.AutoSize = true;
            promptNumLabel.Location = new Point(10, 66);
            promptNumLabel.Name = "promptNumLabel";
            promptNumLabel.Size = new Size(110, 15);
            promptNumLabel.TabIndex = 7;
            promptNumLabel.Text = "Slopes Per Prompt: ";
            // 
            // promptNum
            // 
            promptNum.Location = new Point(126, 64);
            promptNum.Name = "promptNum";
            promptNum.Size = new Size(38, 23);
            promptNum.TabIndex = 6;
            promptNum.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem1, collectedDataToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(800, 24);
            menuStrip2.TabIndex = 7;
            menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openTrainingFileToolStripMenuItem });
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // openTrainingFileToolStripMenuItem
            // 
            openTrainingFileToolStripMenuItem.Name = "openTrainingFileToolStripMenuItem";
            openTrainingFileToolStripMenuItem.Size = new Size(137, 22);
            openTrainingFileToolStripMenuItem.Text = "Training File";
            openTrainingFileToolStripMenuItem.Click += openTrainingFileToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem1
            // 
            viewToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { clearGraphBtn, marketHistoryToolStripMenuItem });
            viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            viewToolStripMenuItem1.Size = new Size(44, 20);
            viewToolStripMenuItem1.Text = "View";
            // 
            // clearGraphBtn
            // 
            clearGraphBtn.Name = "clearGraphBtn";
            clearGraphBtn.Size = new Size(152, 22);
            clearGraphBtn.Text = "Clear Graph";
            clearGraphBtn.Click += clearGraphBtn_Click;
            // 
            // marketHistoryToolStripMenuItem
            // 
            marketHistoryToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { symbolCombo, timeIntervalCombo, durationCombo });
            marketHistoryToolStripMenuItem.Name = "marketHistoryToolStripMenuItem";
            marketHistoryToolStripMenuItem.Size = new Size(152, 22);
            marketHistoryToolStripMenuItem.Text = "Market History";
            // 
            // symbolCombo
            // 
            symbolCombo.Name = "symbolCombo";
            symbolCombo.Size = new Size(121, 23);
            symbolCombo.Text = "Symbol";
            // 
            // timeIntervalCombo
            // 
            timeIntervalCombo.Name = "timeIntervalCombo";
            timeIntervalCombo.Size = new Size(121, 23);
            timeIntervalCombo.Text = "Time Interval";
            // 
            // durationCombo
            // 
            durationCombo.Name = "durationCombo";
            durationCombo.Size = new Size(121, 23);
            durationCombo.Text = "Duration";
            durationCombo.SelectedIndexChanged += durationCombo_SelectedIndexChanged_1;
            // 
            // collectedDataToolStripMenuItem
            // 
            collectedDataToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, viewToolStripMenuItem, addToSymbolListToolStripMenuItem });
            collectedDataToolStripMenuItem.Enabled = false;
            collectedDataToolStripMenuItem.Name = "collectedDataToolStripMenuItem";
            collectedDataToolStripMenuItem.Size = new Size(96, 20);
            collectedDataToolStripMenuItem.Text = "Collected Data";
            collectedDataToolStripMenuItem.Visible = false;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(175, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(175, 22);
            viewToolStripMenuItem.Text = "View";
            // 
            // addToSymbolListToolStripMenuItem
            // 
            addToSymbolListToolStripMenuItem.Name = "addToSymbolListToolStripMenuItem";
            addToSymbolListToolStripMenuItem.Size = new Size(175, 22);
            addToSymbolListToolStripMenuItem.Text = "Add To Symbol List";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(trainingTab);
            tabControl1.Controls.Add(slopeTab);
            tabControl1.Controls.Add(tradingTab);
            tabControl1.Controls.Add(predictionsTab);
            tabControl1.Location = new Point(12, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(279, 501);
            tabControl1.TabIndex = 8;
            // 
            // trainingTab
            // 
            trainingTab.Controls.Add(fineTuneModelCombo);
            trainingTab.Controls.Add(trainExistingRadioBtn);
            trainingTab.Controls.Add(trainNewRadioBtn);
            trainingTab.Controls.Add(button1);
            trainingTab.Controls.Add(numericUpDown5);
            trainingTab.Controls.Add(label6);
            trainingTab.Controls.Add(numericUpDown4);
            trainingTab.Controls.Add(label2);
            trainingTab.Controls.Add(numericUpDown1);
            trainingTab.Controls.Add(label1);
            trainingTab.Controls.Add(numericUpDown3);
            trainingTab.Controls.Add(label4);
            trainingTab.Controls.Add(label7);
            trainingTab.Controls.Add(label5);
            trainingTab.Controls.Add(modelSuffixTextBox);
            trainingTab.Controls.Add(filesListBox);
            trainingTab.Location = new Point(4, 24);
            trainingTab.Name = "trainingTab";
            trainingTab.Padding = new Padding(3);
            trainingTab.Size = new Size(271, 473);
            trainingTab.TabIndex = 0;
            trainingTab.Text = "Training";
            trainingTab.UseVisualStyleBackColor = true;
            // 
            // fineTuneModelCombo
            // 
            fineTuneModelCombo.Enabled = false;
            fineTuneModelCombo.FormattingEnabled = true;
            fineTuneModelCombo.Location = new Point(13, 302);
            fineTuneModelCombo.Name = "fineTuneModelCombo";
            fineTuneModelCombo.Size = new Size(182, 23);
            fineTuneModelCombo.TabIndex = 23;
            // 
            // trainExistingRadioBtn
            // 
            trainExistingRadioBtn.AutoSize = true;
            trainExistingRadioBtn.Location = new Point(201, 303);
            trainExistingRadioBtn.Name = "trainExistingRadioBtn";
            trainExistingRadioBtn.Size = new Size(66, 19);
            trainExistingRadioBtn.TabIndex = 22;
            trainExistingRadioBtn.Text = "Existing";
            trainExistingRadioBtn.UseVisualStyleBackColor = true;
            trainExistingRadioBtn.CheckedChanged += TrainingModelType_CheckedChanged;
            // 
            // trainNewRadioBtn
            // 
            trainNewRadioBtn.AutoSize = true;
            trainNewRadioBtn.Checked = true;
            trainNewRadioBtn.Location = new Point(201, 274);
            trainNewRadioBtn.Name = "trainNewRadioBtn";
            trainNewRadioBtn.Size = new Size(49, 19);
            trainNewRadioBtn.TabIndex = 21;
            trainNewRadioBtn.TabStop = true;
            trainNewRadioBtn.Text = "New";
            trainNewRadioBtn.UseVisualStyleBackColor = true;
            trainNewRadioBtn.CheckedChanged += TrainingModelType_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(103, 331);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 20;
            button1.Text = "Fine Tune";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericUpDown5
            // 
            numericUpDown5.DecimalPlaces = 2;
            numericUpDown5.Location = new Point(153, 219);
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(54, 23);
            numericUpDown5.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 221);
            label6.Name = "label6";
            label6.Size = new Size(136, 15);
            label6.TabIndex = 18;
            label6.Text = "Learning Rate Multiplier:";
            // 
            // numericUpDown4
            // 
            numericUpDown4.DecimalPlaces = 2;
            numericUpDown4.Location = new Point(153, 190);
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(54, 23);
            numericUpDown4.TabIndex = 17;
            numericUpDown4.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 192);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 16;
            label2.Text = "Prompt Weight Loss:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(153, 161);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(54, 23);
            numericUpDown1.TabIndex = 15;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 163);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 14;
            label1.Text = "Epoch Count:";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(153, 132);
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(54, 23);
            numericUpDown3.TabIndex = 13;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 134);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 12;
            label4.Text = "Batch Size:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 14);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 8;
            label7.Text = "Uploaded Files";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 276);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 7;
            label5.Text = "Model:";
            // 
            // modelSuffixTextBox
            // 
            modelSuffixTextBox.Location = new Point(62, 273);
            modelSuffixTextBox.Name = "modelSuffixTextBox";
            modelSuffixTextBox.Size = new Size(133, 23);
            modelSuffixTextBox.TabIndex = 6;
            // 
            // filesListBox
            // 
            filesListBox.FormattingEnabled = true;
            filesListBox.ItemHeight = 15;
            filesListBox.Location = new Point(6, 32);
            filesListBox.Name = "filesListBox";
            filesListBox.Size = new Size(259, 94);
            filesListBox.TabIndex = 0;
            // 
            // slopeTab
            // 
            slopeTab.Controls.Add(groupBox1);
            slopeTab.Location = new Point(4, 24);
            slopeTab.Name = "slopeTab";
            slopeTab.Padding = new Padding(3);
            slopeTab.Size = new Size(271, 473);
            slopeTab.TabIndex = 1;
            slopeTab.Text = "Slopes";
            slopeTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(timeIntervalCombo2);
            groupBox1.Controls.Add(trainingCompletionNum);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(trainingPromptNum);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(finishDateTime);
            groupBox1.Controls.Add(startDateTime);
            groupBox1.Controls.Add(genAndUploadBtn);
            groupBox1.Controls.Add(tolleranceNum);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(259, 253);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Training Files";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(23, 121);
            label13.Name = "label13";
            label13.Size = new Size(49, 15);
            label13.TabIndex = 15;
            label13.Text = "Interval:";
            // 
            // timeIntervalCombo2
            // 
            timeIntervalCombo2.FormattingEnabled = true;
            timeIntervalCombo2.Location = new Point(78, 118);
            timeIntervalCombo2.Name = "timeIntervalCombo2";
            timeIntervalCombo2.Size = new Size(135, 23);
            timeIntervalCombo2.TabIndex = 13;
            // 
            // trainingCompletionNum
            // 
            trainingCompletionNum.Location = new Point(125, 80);
            trainingCompletionNum.Name = "trainingCompletionNum";
            trainingCompletionNum.Size = new Size(54, 23);
            trainingCompletionNum.TabIndex = 12;
            trainingCompletionNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(9, 82);
            label11.Name = "label11";
            label11.Size = new Size(110, 15);
            label11.TabIndex = 11;
            label11.Text = "Completion Slopes:";
            // 
            // trainingPromptNum
            // 
            trainingPromptNum.Location = new Point(125, 51);
            trainingPromptNum.Name = "trainingPromptNum";
            trainingPromptNum.Size = new Size(54, 23);
            trainingPromptNum.TabIndex = 10;
            trainingPromptNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(32, 53);
            label10.Name = "label10";
            label10.Size = new Size(87, 15);
            label10.TabIndex = 9;
            label10.Text = "Prompt Slopes:";
            // 
            // button3
            // 
            button3.Location = new Point(6, 203);
            button3.Name = "button3";
            button3.Size = new Size(101, 43);
            button3.TabIndex = 8;
            button3.Text = "Generate and Save";
            button3.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(4, 182);
            label9.Name = "label9";
            label9.Size = new Size(68, 15);
            label9.TabIndex = 7;
            label9.Text = "Finish Date:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(11, 153);
            label8.Name = "label8";
            label8.Size = new Size(61, 15);
            label8.TabIndex = 6;
            label8.Text = "Start Date:";
            // 
            // finishDateTime
            // 
            finishDateTime.Format = DateTimePickerFormat.Short;
            finishDateTime.Location = new Point(78, 176);
            finishDateTime.Name = "finishDateTime";
            finishDateTime.Size = new Size(101, 23);
            finishDateTime.TabIndex = 5;
            // 
            // startDateTime
            // 
            startDateTime.Format = DateTimePickerFormat.Short;
            startDateTime.Location = new Point(78, 147);
            startDateTime.Name = "startDateTime";
            startDateTime.Size = new Size(101, 23);
            startDateTime.TabIndex = 4;
            // 
            // genAndUploadBtn
            // 
            genAndUploadBtn.Location = new Point(144, 203);
            genAndUploadBtn.Name = "genAndUploadBtn";
            genAndUploadBtn.Size = new Size(109, 43);
            genAndUploadBtn.TabIndex = 3;
            genAndUploadBtn.Text = "Generate and Upload";
            genAndUploadBtn.UseVisualStyleBackColor = true;
            genAndUploadBtn.Click += genAndUploadBtn_Click;
            // 
            // tolleranceNum
            // 
            tolleranceNum.Location = new Point(125, 22);
            tolleranceNum.Name = "tolleranceNum";
            tolleranceNum.Size = new Size(54, 23);
            tolleranceNum.TabIndex = 2;
            tolleranceNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 24);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 0;
            label3.Text = "Tollerance:";
            // 
            // tradingTab
            // 
            tradingTab.Location = new Point(4, 24);
            tradingTab.Name = "tradingTab";
            tradingTab.Size = new Size(271, 473);
            tradingTab.TabIndex = 2;
            tradingTab.Text = "Trading";
            tradingTab.UseVisualStyleBackColor = true;
            // 
            // predictionsTab
            // 
            predictionsTab.Controls.Add(createNew);
            predictionsTab.Location = new Point(4, 24);
            predictionsTab.Name = "predictionsTab";
            predictionsTab.Size = new Size(271, 473);
            predictionsTab.TabIndex = 3;
            predictionsTab.Text = "Predictions";
            predictionsTab.UseVisualStyleBackColor = true;
            // 
            // PredictionView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 547);
            Controls.Add(tabControl1);
            Controls.Add(formsPlot1);
            Controls.Add(menuStrip2);
            MainMenuStrip = menuStrip2;
            Name = "PredictionView";
            Text = "PredictionView";
            FormClosing += PredictionView_FormClosing;
            Load += PredictionView_Load;
            createNew.ResumeLayout(false);
            createNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).EndInit();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            tabControl1.ResumeLayout(false);
            trainingTab.ResumeLayout(false);
            trainingTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            slopeTab.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trainingCompletionNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)trainingPromptNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)tolleranceNum).EndInit();
            predictionsTab.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private TreeView treeView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem openTrainingDataSlopeToolStripMenuItem;
        private ToolStripMenuItem pricePointsToolStripMenuItem;
        private ToolStripMenuItem unlanguisticToolStripMenuItem;
        private ToolStripMenuItem slopesToolStripMenuItem;
        private ToolStripMenuItem minimumTextToolStripMenuItem;
        private ToolStripMenuItem fluidLanguageToolStripMenuItem;
        private Button predictBtn;
        private Label modelIdLabel;
        private GroupBox createNew;
        private ComboBox comboBox2;
        private ComboBox modelIdCombo;
        private Label promptNumLabel;
        private NumericUpDown promptNum;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem refreshDataBtn;
        private ToolStripMenuItem collectedDataToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem addToSymbolListToolStripMenuItem;
        private ToolStripComboBox symbolCombo;
        private ToolStripComboBox timeIntervalCombo;
        private ToolStripComboBox durationCombo;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem openTrainingFileToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem1;
        private ToolStripMenuItem clearGraphBtn;
        private ToolStripMenuItem marketHistoryToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage trainingTab;
        private TabPage slopeTab;
        private TabPage tradingTab;
        private TabPage predictionsTab;
        private NumericUpDown tolleranceNum;
        private Label label3;
        private Label label5;
        private TextBox modelSuffixTextBox;
        private ListBox filesListBox;
        private ComboBox fineTuneModelCombo;
        private RadioButton trainExistingRadioBtn;
        private RadioButton trainNewRadioBtn;
        private Button button1;
        private NumericUpDown numericUpDown5;
        private Label label6;
        private NumericUpDown numericUpDown4;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private NumericUpDown numericUpDown3;
        private Label label4;
        private Label label7;
        private GroupBox groupBox1;
        private Button button3;
        private Label label9;
        private Label label8;
        private DateTimePicker finishDateTime;
        private DateTimePicker startDateTime;
        private Button genAndUploadBtn;
        private NumericUpDown trainingCompletionNum;
        private Label label11;
        private NumericUpDown trainingPromptNum;
        private Label label10;
        private Label label13;
        private ComboBox timeIntervalCombo2;
    }
}