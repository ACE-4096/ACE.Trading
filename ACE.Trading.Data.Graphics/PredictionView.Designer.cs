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
            refreshDataBtn = new ToolStripMenuItem();
            clearGraphBtn = new ToolStripMenuItem();
            symbolsListToolStripMenuItem = new ToolStripMenuItem();
            symbolCombo = new ToolStripComboBox();
            timeIntervalCombo = new ToolStripComboBox();
            durationCombo = new ToolStripComboBox();
            collectedDataToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            addToSymbolListToolStripMenuItem = new ToolStripMenuItem();
            createNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).BeginInit();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new Point(262, 34);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(525, 501);
            formsPlot1.TabIndex = 0;
            // 
            // treeView
            // 
            treeView.Location = new Point(12, 34);
            treeView.Name = "treeView";
            treeView.Size = new Size(243, 406);
            treeView.TabIndex = 1;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            // 
            // predictBtn
            // 
            predictBtn.Location = new Point(162, 66);
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
            modelIdCombo.Size = new Size(231, 23);
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
            createNew.Location = new Point(12, 446);
            createNew.Name = "createNew";
            createNew.Size = new Size(243, 89);
            createNew.TabIndex = 6;
            createNew.TabStop = false;
            createNew.Text = "Predict Next Slopes";
            // 
            // promptNumLabel
            // 
            promptNumLabel.AutoSize = true;
            promptNumLabel.Location = new Point(10, 66);
            promptNumLabel.Name = "promptNumLabel";
            promptNumLabel.Size = new Size(77, 15);
            promptNumLabel.TabIndex = 7;
            promptNumLabel.Text = "PromptNum:";
            // 
            // promptNum
            // 
            promptNum.Location = new Point(93, 64);
            promptNum.Name = "promptNum";
            promptNum.Size = new Size(53, 23);
            promptNum.TabIndex = 6;
            promptNum.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { refreshDataBtn, clearGraphBtn, symbolsListToolStripMenuItem, collectedDataToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(800, 24);
            menuStrip2.TabIndex = 7;
            menuStrip2.Text = "menuStrip2";
            // 
            // refreshDataBtn
            // 
            refreshDataBtn.Name = "refreshDataBtn";
            refreshDataBtn.Size = new Size(58, 20);
            refreshDataBtn.Text = "Refresh";
            refreshDataBtn.Click += refreshDataBtn_Click;
            // 
            // clearGraphBtn
            // 
            clearGraphBtn.Name = "clearGraphBtn";
            clearGraphBtn.Size = new Size(81, 20);
            clearGraphBtn.Text = "Clear Graph";
            clearGraphBtn.Click += clearGraphBtn_Click;
            // 
            // symbolsListToolStripMenuItem
            // 
            symbolsListToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { symbolCombo, timeIntervalCombo, durationCombo });
            symbolsListToolStripMenuItem.Name = "symbolsListToolStripMenuItem";
            symbolsListToolStripMenuItem.Size = new Size(97, 20);
            symbolsListToolStripMenuItem.Text = "Market History";
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
            // PredictionView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 547);
            Controls.Add(createNew);
            Controls.Add(treeView);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private TreeView treeView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
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
        private ToolStripMenuItem clearGraphBtn;
        private ToolStripMenuItem collectedDataToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem addToSymbolListToolStripMenuItem;
        private ToolStripMenuItem symbolsListToolStripMenuItem;
        private ToolStripComboBox symbolCombo;
        private ToolStripComboBox timeIntervalCombo;
        private ToolStripComboBox durationCombo;
    }
}