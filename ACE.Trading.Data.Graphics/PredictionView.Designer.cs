﻿namespace ACE.Trading.Data.Graphics
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
            symbolCombo = new ComboBox();
            symbolLabel = new Label();
            promptNumLabel = new Label();
            promptNum = new NumericUpDown();
            createNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).BeginInit();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new Point(262, 12);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(525, 523);
            formsPlot1.TabIndex = 0;
            // 
            // treeView
            // 
            treeView.Location = new Point(12, 12);
            treeView.Name = "treeView";
            treeView.Size = new Size(243, 380);
            treeView.TabIndex = 1;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            // 
            // predictBtn
            // 
            predictBtn.Location = new Point(162, 108);
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
            modelIdCombo.Location = new Point(6, 77);
            modelIdCombo.Name = "modelIdCombo";
            modelIdCombo.Size = new Size(231, 23);
            modelIdCombo.TabIndex = 4;
            // 
            // modelIdLabel
            // 
            modelIdLabel.AutoSize = true;
            modelIdLabel.Location = new Point(6, 59);
            modelIdLabel.Name = "modelIdLabel";
            modelIdLabel.Size = new Size(57, 15);
            modelIdLabel.TabIndex = 5;
            modelIdLabel.Text = "Model Id:";
            // 
            // createNew
            // 
            createNew.Controls.Add(symbolCombo);
            createNew.Controls.Add(symbolLabel);
            createNew.Controls.Add(promptNumLabel);
            createNew.Controls.Add(promptNum);
            createNew.Controls.Add(modelIdCombo);
            createNew.Controls.Add(predictBtn);
            createNew.Controls.Add(modelIdLabel);
            createNew.Location = new Point(12, 398);
            createNew.Name = "createNew";
            createNew.Size = new Size(243, 137);
            createNew.TabIndex = 6;
            createNew.TabStop = false;
            createNew.Text = "Predict Next Slopes";
            // 
            // symbolCombo
            // 
            symbolCombo.FormattingEnabled = true;
            symbolCombo.Location = new Point(54, 24);
            symbolCombo.Name = "symbolCombo";
            symbolCombo.Size = new Size(92, 23);
            symbolCombo.TabIndex = 9;
            // 
            // symbolLabel
            // 
            symbolLabel.AutoSize = true;
            symbolLabel.Location = new Point(10, 27);
            symbolLabel.Name = "symbolLabel";
            symbolLabel.Size = new Size(47, 15);
            symbolLabel.TabIndex = 8;
            symbolLabel.Text = "Symbol";
            // 
            // promptNumLabel
            // 
            promptNumLabel.AutoSize = true;
            promptNumLabel.Location = new Point(10, 108);
            promptNumLabel.Name = "promptNumLabel";
            promptNumLabel.Size = new Size(77, 15);
            promptNumLabel.TabIndex = 7;
            promptNumLabel.Text = "PromptNum:";
            // 
            // promptNum
            // 
            promptNum.Location = new Point(93, 106);
            promptNum.Name = "promptNum";
            promptNum.Size = new Size(53, 23);
            promptNum.TabIndex = 6;
            promptNum.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // PredictionView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 547);
            Controls.Add(createNew);
            Controls.Add(treeView);
            Controls.Add(formsPlot1);
            Name = "PredictionView";
            Text = "PredictionView";
            FormClosing += PredictionView_FormClosing;
            Load += PredictionView_Load;
            createNew.ResumeLayout(false);
            createNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)promptNum).EndInit();
            ResumeLayout(false);
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
        private ComboBox symbolCombo;
        private Label symbolLabel;
        private Label promptNumLabel;
        private NumericUpDown promptNum;
    }
}