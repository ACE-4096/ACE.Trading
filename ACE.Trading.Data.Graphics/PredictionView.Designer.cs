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
            slopeTab = new TabPage();
            groupBox1 = new GroupBox();
            uploadFileName = new TextBox();
            label12 = new Label();
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
            trainingTab = new TabPage();
            uploadFileBtn = new Button();
            deleteFileBtn = new Button();
            fineTuneGroupBox = new GroupBox();
            learningMultiplierHelpBtn = new Button();
            weightLossHelpBtn = new Button();
            epochCountHelpBtn = new Button();
            batchSizeHelpBtn = new Button();
            label4 = new Label();
            fineTuneModelCombo = new ComboBox();
            modelSuffixTextBox = new TextBox();
            trainExistingRadioBtn = new RadioButton();
            label5 = new Label();
            trainNewRadioBtn = new RadioButton();
            batchSizeNum = new NumericUpDown();
            button1 = new Button();
            label1 = new Label();
            learningRateNum = new NumericUpDown();
            epochCountNum = new NumericUpDown();
            label6 = new Label();
            label2 = new Label();
            promptWeightNum = new NumericUpDown();
            label7 = new Label();
            filesListBox = new ListBox();
            tunesTab = new TabPage();
            groupBox4 = new GroupBox();
            tunesEventsList = new ListBox();
            groupBox3 = new GroupBox();
            tunesLearningMultiplierLabel = new Label();
            label25 = new Label();
            tunesWeightLossLabel = new Label();
            label23 = new Label();
            label18 = new Label();
            tunesBatchSizeLabel = new Label();
            tunesEpochCountLabel = new Label();
            label21 = new Label();
            groupBox2 = new GroupBox();
            tunesSlopeNumLabel = new Label();
            label20 = new Label();
            tunesBaseModelLabel = new Label();
            label19 = new Label();
            tunesCreatedAtLabel = new Label();
            label17 = new Label();
            tunesStatusLabel = new Label();
            label15 = new Label();
            label14 = new Label();
            tunesCombo = new ComboBox();
            predictionsTab = new TabPage();
            tradingTab = new TabPage();
            button2 = new Button();
            createNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).BeginInit();
            menuStrip2.SuspendLayout();
            tabControl1.SuspendLayout();
            slopeTab.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trainingCompletionNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trainingPromptNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tolleranceNum).BeginInit();
            trainingTab.SuspendLayout();
            fineTuneGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)batchSizeNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)learningRateNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epochCountNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)promptWeightNum).BeginInit();
            tunesTab.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            predictionsTab.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.Location = new Point(294, 34);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(493, 501);
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
            createNew.Location = new Point(3, 3);
            createNew.Name = "createNew";
            createNew.Size = new Size(265, 97);
            createNew.TabIndex = 6;
            createNew.TabStop = false;
            createNew.Text = "Fine Tunes";
            createNew.Enter += createNew_Enter;
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
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // openTrainingFileToolStripMenuItem
            // 
            openTrainingFileToolStripMenuItem.Name = "openTrainingFileToolStripMenuItem";
            openTrainingFileToolStripMenuItem.Size = new Size(180, 22);
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
            saveToolStripMenuItem.Size = new Size(177, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(177, 22);
            viewToolStripMenuItem.Text = "View";
            // 
            // addToSymbolListToolStripMenuItem
            // 
            addToSymbolListToolStripMenuItem.Name = "addToSymbolListToolStripMenuItem";
            addToSymbolListToolStripMenuItem.Size = new Size(177, 22);
            addToSymbolListToolStripMenuItem.Text = "Add To Symbol List";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(slopeTab);
            tabControl1.Controls.Add(trainingTab);
            tabControl1.Controls.Add(tunesTab);
            tabControl1.Controls.Add(predictionsTab);
            tabControl1.Controls.Add(tradingTab);
            tabControl1.Location = new Point(12, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(279, 501);
            tabControl1.TabIndex = 8;
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
            groupBox1.Controls.Add(uploadFileName);
            groupBox1.Controls.Add(label12);
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
            groupBox1.Size = new Size(259, 290);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Training Files";
            // 
            // uploadFileName
            // 
            uploadFileName.Location = new Point(79, 210);
            uploadFileName.Name = "uploadFileName";
            uploadFileName.Size = new Size(174, 23);
            uploadFileName.TabIndex = 17;
            uploadFileName.Text = "temp.jsonl";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(4, 213);
            label12.Name = "label12";
            label12.Size = new Size(61, 15);
            label12.TabIndex = 16;
            label12.Text = "File name:";
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
            button3.Location = new Point(6, 241);
            button3.Name = "button3";
            button3.Size = new Size(101, 43);
            button3.TabIndex = 8;
            button3.Text = "Generate and Save";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
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
            genAndUploadBtn.Location = new Point(144, 241);
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
            label3.Size = new Size(65, 15);
            label3.TabIndex = 0;
            label3.Text = "Tollerance:";
            // 
            // trainingTab
            // 
            trainingTab.Controls.Add(button2);
            trainingTab.Controls.Add(uploadFileBtn);
            trainingTab.Controls.Add(deleteFileBtn);
            trainingTab.Controls.Add(fineTuneGroupBox);
            trainingTab.Controls.Add(label7);
            trainingTab.Controls.Add(filesListBox);
            trainingTab.Location = new Point(4, 24);
            trainingTab.Name = "trainingTab";
            trainingTab.Padding = new Padding(3);
            trainingTab.Size = new Size(271, 473);
            trainingTab.TabIndex = 0;
            trainingTab.Text = "Training";
            trainingTab.UseVisualStyleBackColor = true;
            // 
            // uploadFileBtn
            // 
            uploadFileBtn.Location = new Point(6, 202);
            uploadFileBtn.Name = "uploadFileBtn";
            uploadFileBtn.Size = new Size(75, 23);
            uploadFileBtn.TabIndex = 26;
            uploadFileBtn.Text = "Upload";
            uploadFileBtn.UseVisualStyleBackColor = true;
            uploadFileBtn.Click += uploadFileBtn_Click;
            // 
            // deleteFileBtn
            // 
            deleteFileBtn.Enabled = false;
            deleteFileBtn.Location = new Point(189, 202);
            deleteFileBtn.Name = "deleteFileBtn";
            deleteFileBtn.Size = new Size(75, 23);
            deleteFileBtn.TabIndex = 25;
            deleteFileBtn.Text = "Delete";
            deleteFileBtn.UseVisualStyleBackColor = true;
            deleteFileBtn.Click += deleteFileBtn_Click;
            // 
            // fineTuneGroupBox
            // 
            fineTuneGroupBox.Controls.Add(learningMultiplierHelpBtn);
            fineTuneGroupBox.Controls.Add(weightLossHelpBtn);
            fineTuneGroupBox.Controls.Add(epochCountHelpBtn);
            fineTuneGroupBox.Controls.Add(batchSizeHelpBtn);
            fineTuneGroupBox.Controls.Add(label4);
            fineTuneGroupBox.Controls.Add(fineTuneModelCombo);
            fineTuneGroupBox.Controls.Add(modelSuffixTextBox);
            fineTuneGroupBox.Controls.Add(trainExistingRadioBtn);
            fineTuneGroupBox.Controls.Add(label5);
            fineTuneGroupBox.Controls.Add(trainNewRadioBtn);
            fineTuneGroupBox.Controls.Add(batchSizeNum);
            fineTuneGroupBox.Controls.Add(button1);
            fineTuneGroupBox.Controls.Add(label1);
            fineTuneGroupBox.Controls.Add(learningRateNum);
            fineTuneGroupBox.Controls.Add(epochCountNum);
            fineTuneGroupBox.Controls.Add(label6);
            fineTuneGroupBox.Controls.Add(label2);
            fineTuneGroupBox.Controls.Add(promptWeightNum);
            fineTuneGroupBox.Enabled = false;
            fineTuneGroupBox.Location = new Point(3, 231);
            fineTuneGroupBox.Name = "fineTuneGroupBox";
            fineTuneGroupBox.Size = new Size(265, 239);
            fineTuneGroupBox.TabIndex = 24;
            fineTuneGroupBox.TabStop = false;
            fineTuneGroupBox.Text = "Fine Tune";
            // 
            // learningMultiplierHelpBtn
            // 
            learningMultiplierHelpBtn.Location = new Point(206, 116);
            learningMultiplierHelpBtn.Name = "learningMultiplierHelpBtn";
            learningMultiplierHelpBtn.Size = new Size(28, 23);
            learningMultiplierHelpBtn.TabIndex = 27;
            learningMultiplierHelpBtn.Text = "?";
            learningMultiplierHelpBtn.UseVisualStyleBackColor = true;
            learningMultiplierHelpBtn.Click += learningMultiplierHelpBtn_Click;
            // 
            // weightLossHelpBtn
            // 
            weightLossHelpBtn.Location = new Point(206, 87);
            weightLossHelpBtn.Name = "weightLossHelpBtn";
            weightLossHelpBtn.Size = new Size(28, 23);
            weightLossHelpBtn.TabIndex = 26;
            weightLossHelpBtn.Text = "?";
            weightLossHelpBtn.UseVisualStyleBackColor = true;
            weightLossHelpBtn.Click += weightLossHelpBtn_Click;
            // 
            // epochCountHelpBtn
            // 
            epochCountHelpBtn.Location = new Point(206, 58);
            epochCountHelpBtn.Name = "epochCountHelpBtn";
            epochCountHelpBtn.Size = new Size(28, 23);
            epochCountHelpBtn.TabIndex = 25;
            epochCountHelpBtn.Text = "?";
            epochCountHelpBtn.UseVisualStyleBackColor = true;
            epochCountHelpBtn.Click += epochCountHelpBtn_Click;
            // 
            // batchSizeHelpBtn
            // 
            batchSizeHelpBtn.Location = new Point(206, 29);
            batchSizeHelpBtn.Name = "batchSizeHelpBtn";
            batchSizeHelpBtn.Size = new Size(28, 23);
            batchSizeHelpBtn.TabIndex = 24;
            batchSizeHelpBtn.Text = "?";
            batchSizeHelpBtn.UseVisualStyleBackColor = true;
            batchSizeHelpBtn.Click += batchSizeHelpBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 33);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 12;
            label4.Text = "Batch Size:";
            // 
            // fineTuneModelCombo
            // 
            fineTuneModelCombo.Enabled = false;
            fineTuneModelCombo.FormattingEnabled = true;
            fineTuneModelCombo.Location = new Point(8, 176);
            fineTuneModelCombo.Name = "fineTuneModelCombo";
            fineTuneModelCombo.Size = new Size(182, 23);
            fineTuneModelCombo.TabIndex = 23;
            // 
            // modelSuffixTextBox
            // 
            modelSuffixTextBox.Location = new Point(57, 147);
            modelSuffixTextBox.Name = "modelSuffixTextBox";
            modelSuffixTextBox.Size = new Size(133, 23);
            modelSuffixTextBox.TabIndex = 6;
            // 
            // trainExistingRadioBtn
            // 
            trainExistingRadioBtn.AutoSize = true;
            trainExistingRadioBtn.Location = new Point(196, 177);
            trainExistingRadioBtn.Name = "trainExistingRadioBtn";
            trainExistingRadioBtn.Size = new Size(65, 19);
            trainExistingRadioBtn.TabIndex = 22;
            trainExistingRadioBtn.Text = "Existing";
            trainExistingRadioBtn.UseVisualStyleBackColor = true;
            trainExistingRadioBtn.CheckedChanged += TrainingModelType_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 150);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 7;
            label5.Text = "Model:";
            // 
            // trainNewRadioBtn
            // 
            trainNewRadioBtn.AutoSize = true;
            trainNewRadioBtn.Checked = true;
            trainNewRadioBtn.Location = new Point(196, 148);
            trainNewRadioBtn.Name = "trainNewRadioBtn";
            trainNewRadioBtn.Size = new Size(49, 19);
            trainNewRadioBtn.TabIndex = 21;
            trainNewRadioBtn.TabStop = true;
            trainNewRadioBtn.Text = "New";
            trainNewRadioBtn.UseVisualStyleBackColor = true;
            trainNewRadioBtn.CheckedChanged += TrainingModelType_CheckedChanged;
            // 
            // batchSizeNum
            // 
            batchSizeNum.Location = new Point(146, 31);
            batchSizeNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            batchSizeNum.Name = "batchSizeNum";
            batchSizeNum.Size = new Size(54, 23);
            batchSizeNum.TabIndex = 13;
            batchSizeNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new Point(94, 205);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 20;
            button1.Text = "Fine Tune";
            button1.UseVisualStyleBackColor = true;
            button1.Click += fineTuneBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 62);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 14;
            label1.Text = "Epoch Count:";
            // 
            // learningRateNum
            // 
            learningRateNum.DecimalPlaces = 2;
            learningRateNum.Location = new Point(146, 118);
            learningRateNum.Name = "learningRateNum";
            learningRateNum.Size = new Size(54, 23);
            learningRateNum.TabIndex = 19;
            learningRateNum.Value = new decimal(new int[] { 2, 0, 0, 131072 });
            // 
            // epochCountNum
            // 
            epochCountNum.Location = new Point(146, 60);
            epochCountNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            epochCountNum.Name = "epochCountNum";
            epochCountNum.Size = new Size(54, 23);
            epochCountNum.TabIndex = 15;
            epochCountNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 120);
            label6.Name = "label6";
            label6.Size = new Size(136, 15);
            label6.TabIndex = 18;
            label6.Text = "Learning Rate Multiplier:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 91);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 16;
            label2.Text = "Prompt Weight Loss:";
            // 
            // promptWeightNum
            // 
            promptWeightNum.DecimalPlaces = 2;
            promptWeightNum.Location = new Point(146, 89);
            promptWeightNum.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            promptWeightNum.Name = "promptWeightNum";
            promptWeightNum.Size = new Size(54, 23);
            promptWeightNum.TabIndex = 17;
            promptWeightNum.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(4, 9);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 8;
            label7.Text = "Uploaded Files";
            // 
            // filesListBox
            // 
            filesListBox.FormattingEnabled = true;
            filesListBox.ItemHeight = 15;
            filesListBox.Location = new Point(6, 27);
            filesListBox.Name = "filesListBox";
            filesListBox.Size = new Size(259, 169);
            filesListBox.TabIndex = 0;
            filesListBox.SelectedIndexChanged += filesListBox_SelectedIndexChanged;
            // 
            // tunesTab
            // 
            tunesTab.Controls.Add(groupBox4);
            tunesTab.Controls.Add(groupBox3);
            tunesTab.Controls.Add(groupBox2);
            tunesTab.Controls.Add(label14);
            tunesTab.Controls.Add(tunesCombo);
            tunesTab.Location = new Point(4, 24);
            tunesTab.Name = "tunesTab";
            tunesTab.Size = new Size(271, 473);
            tunesTab.TabIndex = 3;
            tunesTab.Text = "Tunes";
            tunesTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = SystemColors.AppWorkspace;
            groupBox4.Controls.Add(tunesEventsList);
            groupBox4.Location = new Point(3, 270);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(265, 200);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Events";
            // 
            // tunesEventsList
            // 
            tunesEventsList.FormattingEnabled = true;
            tunesEventsList.ItemHeight = 15;
            tunesEventsList.Location = new Point(6, 22);
            tunesEventsList.Name = "tunesEventsList";
            tunesEventsList.Size = new Size(253, 169);
            tunesEventsList.TabIndex = 0;
            tunesEventsList.SelectedIndexChanged += tunesEventsList_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.AppWorkspace;
            groupBox3.Controls.Add(tunesLearningMultiplierLabel);
            groupBox3.Controls.Add(label25);
            groupBox3.Controls.Add(tunesWeightLossLabel);
            groupBox3.Controls.Add(label23);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(tunesBatchSizeLabel);
            groupBox3.Controls.Add(tunesEpochCountLabel);
            groupBox3.Controls.Add(label21);
            groupBox3.Location = new Point(3, 164);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(265, 100);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Hyper Parameters";
            // 
            // tunesLearningMultiplierLabel
            // 
            tunesLearningMultiplierLabel.AutoSize = true;
            tunesLearningMultiplierLabel.Location = new Point(157, 70);
            tunesLearningMultiplierLabel.Name = "tunesLearningMultiplierLabel";
            tunesLearningMultiplierLabel.Size = new Size(0, 15);
            tunesLearningMultiplierLabel.TabIndex = 13;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(12, 69);
            label25.Name = "label25";
            label25.Size = new Size(136, 15);
            label25.TabIndex = 12;
            label25.Text = "Learning Rate Multiplier:";
            // 
            // tunesWeightLossLabel
            // 
            tunesWeightLossLabel.AutoSize = true;
            tunesWeightLossLabel.Location = new Point(157, 50);
            tunesWeightLossLabel.Name = "tunesWeightLossLabel";
            tunesWeightLossLabel.Size = new Size(0, 15);
            tunesWeightLossLabel.TabIndex = 11;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(12, 19);
            label23.Name = "label23";
            label23.Size = new Size(63, 15);
            label23.TabIndex = 6;
            label23.Text = "Batch Size:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(12, 51);
            label18.Name = "label18";
            label18.Size = new Size(117, 15);
            label18.TabIndex = 10;
            label18.Text = "Prompt Weight Loss:";
            // 
            // tunesBatchSizeLabel
            // 
            tunesBatchSizeLabel.AutoSize = true;
            tunesBatchSizeLabel.Location = new Point(157, 20);
            tunesBatchSizeLabel.Name = "tunesBatchSizeLabel";
            tunesBatchSizeLabel.Size = new Size(0, 15);
            tunesBatchSizeLabel.TabIndex = 7;
            // 
            // tunesEpochCountLabel
            // 
            tunesEpochCountLabel.AutoSize = true;
            tunesEpochCountLabel.Location = new Point(157, 35);
            tunesEpochCountLabel.Name = "tunesEpochCountLabel";
            tunesEpochCountLabel.Size = new Size(0, 15);
            tunesEpochCountLabel.TabIndex = 9;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(12, 35);
            label21.Name = "label21";
            label21.Size = new Size(84, 15);
            label21.TabIndex = 8;
            label21.Text = "Epochs Count:";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.AppWorkspace;
            groupBox2.Controls.Add(tunesSlopeNumLabel);
            groupBox2.Controls.Add(label20);
            groupBox2.Controls.Add(tunesBaseModelLabel);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(tunesCreatedAtLabel);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(tunesStatusLabel);
            groupBox2.Controls.Add(label15);
            groupBox2.Location = new Point(3, 58);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(265, 100);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Details";
            // 
            // tunesSlopeNumLabel
            // 
            tunesSlopeNumLabel.AutoSize = true;
            tunesSlopeNumLabel.Location = new Point(119, 70);
            tunesSlopeNumLabel.Name = "tunesSlopeNumLabel";
            tunesSlopeNumLabel.Size = new Size(0, 15);
            tunesSlopeNumLabel.TabIndex = 7;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(6, 70);
            label20.Name = "label20";
            label20.Size = new Size(107, 15);
            label20.TabIndex = 6;
            label20.Text = "Slopes Per Prompt:";
            // 
            // tunesBaseModelLabel
            // 
            tunesBaseModelLabel.AutoSize = true;
            tunesBaseModelLabel.Location = new Point(83, 55);
            tunesBaseModelLabel.Name = "tunesBaseModelLabel";
            tunesBaseModelLabel.Size = new Size(0, 15);
            tunesBaseModelLabel.TabIndex = 5;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(6, 53);
            label19.Name = "label19";
            label19.Size = new Size(71, 15);
            label19.TabIndex = 4;
            label19.Text = "Base Model:";
            // 
            // tunesCreatedAtLabel
            // 
            tunesCreatedAtLabel.AutoSize = true;
            tunesCreatedAtLabel.Location = new Point(97, 36);
            tunesCreatedAtLabel.Name = "tunesCreatedAtLabel";
            tunesCreatedAtLabel.Size = new Size(0, 15);
            tunesCreatedAtLabel.TabIndex = 3;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 36);
            label17.Name = "label17";
            label17.Size = new Size(85, 15);
            label17.TabIndex = 2;
            label17.Text = "Creation Time:";
            // 
            // tunesStatusLabel
            // 
            tunesStatusLabel.AutoSize = true;
            tunesStatusLabel.Location = new Point(57, 19);
            tunesStatusLabel.Name = "tunesStatusLabel";
            tunesStatusLabel.Size = new Size(0, 15);
            tunesStatusLabel.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 19);
            label15.Name = "label15";
            label15.Size = new Size(45, 15);
            label15.TabIndex = 0;
            label15.Text = "Status: ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(3, 11);
            label14.Name = "label14";
            label14.Size = new Size(79, 15);
            label14.TabIndex = 1;
            label14.Text = "Model Name:";
            // 
            // tunesCombo
            // 
            tunesCombo.FormattingEnabled = true;
            tunesCombo.Location = new Point(3, 29);
            tunesCombo.Name = "tunesCombo";
            tunesCombo.Size = new Size(265, 23);
            tunesCombo.TabIndex = 0;
            tunesCombo.SelectedIndexChanged += tunesCombo_SelectedIndexChanged;
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
            // tradingTab
            // 
            tradingTab.Location = new Point(4, 24);
            tradingTab.Name = "tradingTab";
            tradingTab.Size = new Size(271, 473);
            tradingTab.TabIndex = 2;
            tradingTab.Text = "Trading";
            tradingTab.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(97, 202);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 27;
            button2.Text = "Download";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
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
            slopeTab.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trainingCompletionNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)trainingPromptNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)tolleranceNum).EndInit();
            trainingTab.ResumeLayout(false);
            trainingTab.PerformLayout();
            fineTuneGroupBox.ResumeLayout(false);
            fineTuneGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)batchSizeNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)learningRateNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)epochCountNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)promptWeightNum).EndInit();
            tunesTab.ResumeLayout(false);
            tunesTab.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
        private TabPage tunesTab;
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
        private NumericUpDown learningRateNum;
        private Label label6;
        private NumericUpDown promptWeightNum;
        private Label label2;
        private NumericUpDown epochCountNum;
        private Label label1;
        private NumericUpDown batchSizeNum;
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
        private GroupBox fineTuneGroupBox;
        private Button deleteFileBtn;
        private TextBox uploadFileName;
        private Label label12;
        private Button uploadFileBtn;
        private Button learningMultiplierHelpBtn;
        private Button weightLossHelpBtn;
        private Button epochCountHelpBtn;
        private Button batchSizeHelpBtn;
        private Label label14;
        private ComboBox tunesCombo;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private Label tunesStatusLabel;
        private Label label15;
        private Label tunesBaseModelLabel;
        private Label label19;
        private Label tunesCreatedAtLabel;
        private Label label17;
        private Label tunesWeightLossLabel;
        private Label label23;
        private Label label18;
        private Label tunesBatchSizeLabel;
        private Label tunesEpochCountLabel;
        private Label label21;
        private Label tunesLearningMultiplierLabel;
        private Label label25;
        private ListBox tunesEventsList;
        private Label tunesSlopeNumLabel;
        private Label label20;
        private Button button2;
    }
}