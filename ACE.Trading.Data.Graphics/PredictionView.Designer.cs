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
            treeView = new TreeView();
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
            treeViewTab = new TabPage();
            slopeTab = new TabPage();
            checkBox1 = new CheckBox();
            tradingTab = new TabPage();
            predictionsTab = new TabPage();
            createNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).BeginInit();
            menuStrip2.SuspendLayout();
            tabControl1.SuspendLayout();
            treeViewTab.SuspendLayout();
            slopeTab.SuspendLayout();
            predictionsTab.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.Location = new Point(262, 34);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(525, 501);
            formsPlot1.TabIndex = 0;
            // 
            // treeView
            // 
            treeView.Location = new Point(6, 6);
            treeView.Name = "treeView";
            treeView.Size = new Size(259, 366);
            treeView.TabIndex = 1;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
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
            tabControl1.Controls.Add(treeViewTab);
            tabControl1.Controls.Add(slopeTab);
            tabControl1.Controls.Add(tradingTab);
            tabControl1.Controls.Add(predictionsTab);
            tabControl1.Location = new Point(12, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(279, 501);
            tabControl1.TabIndex = 8;
            // 
            // treeViewTab
            // 
            treeViewTab.Controls.Add(treeView);
            treeViewTab.Location = new Point(4, 24);
            treeViewTab.Name = "treeViewTab";
            treeViewTab.Padding = new Padding(3);
            treeViewTab.Size = new Size(271, 473);
            treeViewTab.TabIndex = 0;
            treeViewTab.Text = "Data View";
            treeViewTab.UseVisualStyleBackColor = true;
            // 
            // slopeTab
            // 
            slopeTab.Controls.Add(checkBox1);
            slopeTab.Location = new Point(4, 24);
            slopeTab.Name = "slopeTab";
            slopeTab.Padding = new Padding(3);
            slopeTab.Size = new Size(271, 473);
            slopeTab.TabIndex = 1;
            slopeTab.Text = "Slopes";
            slopeTab.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 6);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Use Slopes";
            checkBox1.UseVisualStyleBackColor = true;
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
            treeViewTab.ResumeLayout(false);
            slopeTab.ResumeLayout(false);
            slopeTab.PerformLayout();
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
        private Label label1;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox2;
        private Label label2;
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
        private TabPage treeViewTab;
        private TabPage slopeTab;
        private CheckBox checkBox1;
        private TabPage tradingTab;
        private TabPage predictionsTab;
    }
}