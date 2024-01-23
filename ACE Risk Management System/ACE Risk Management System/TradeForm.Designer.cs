namespace ACE_Risk_Management_System
{
    partial class TradeForm
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
            groupBox1 = new GroupBox();
            orderPanel = new Panel();
            label12 = new Label();
            label11 = new Label();
            label2 = new Label();
            RemoveTpBtn = new Button();
            AddTpBtn = new Button();
            tradeEntry = new NumericUpDown();
            label10 = new Label();
            calcBtn = new Button();
            tradeRR = new NumericUpDown();
            label3 = new Label();
            groupBox2 = new GroupBox();
            gainTxt = new Label();
            lossTxt = new Label();
            qtyTxt = new Label();
            label8 = new Label();
            label6 = new Label();
            label4 = new Label();
            label7 = new Label();
            label5 = new Label();
            CancelBtn = new Button();
            MarketBtn = new Button();
            menuStrip1 = new MenuStrip();
            balTxt = new ToolStripTextBox();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            DailyPnlTxt = new ToolStripTextBox();
            riskTxt = new ToolStripTextBox();
            tradePanel = new Panel();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tradeEntry).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tradeRR).BeginInit();
            groupBox2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(orderPanel);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(RemoveTpBtn);
            groupBox1.Controls.Add(AddTpBtn);
            groupBox1.Controls.Add(tradeEntry);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(calcBtn);
            groupBox1.Controls.Add(tradeRR);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(12, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(399, 253);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Risk Calculator";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // orderPanel
            // 
            orderPanel.Location = new Point(6, 99);
            orderPanel.Name = "orderPanel";
            orderPanel.Size = new Size(387, 119);
            orderPanel.TabIndex = 14;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(257, 81);
            label12.Name = "label12";
            label12.Size = new Size(39, 15);
            label12.TabIndex = 13;
            label12.Text = "Qty %";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(156, 81);
            label11.Name = "label11";
            label11.Size = new Size(63, 15);
            label11.TabIndex = 12;
            label11.Text = "Limit Price";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 1);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 11;
            label2.Text = "Trade Info";
            // 
            // RemoveTpBtn
            // 
            RemoveTpBtn.BackColor = SystemColors.ActiveCaptionText;
            RemoveTpBtn.FlatAppearance.BorderColor = Color.FromArgb(128, 255, 128);
            RemoveTpBtn.FlatStyle = FlatStyle.Flat;
            RemoveTpBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            RemoveTpBtn.ForeColor = Color.Red;
            RemoveTpBtn.Location = new Point(347, 42);
            RemoveTpBtn.Name = "RemoveTpBtn";
            RemoveTpBtn.Size = new Size(46, 23);
            RemoveTpBtn.TabIndex = 10;
            RemoveTpBtn.Text = "- TP";
            RemoveTpBtn.UseVisualStyleBackColor = false;
            RemoveTpBtn.Click += RemoveTpBtn_Click;
            // 
            // AddTpBtn
            // 
            AddTpBtn.BackColor = SystemColors.ActiveCaptionText;
            AddTpBtn.FlatAppearance.BorderColor = Color.FromArgb(128, 255, 128);
            AddTpBtn.FlatStyle = FlatStyle.Flat;
            AddTpBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            AddTpBtn.ForeColor = Color.FromArgb(128, 255, 128);
            AddTpBtn.Location = new Point(347, 15);
            AddTpBtn.Name = "AddTpBtn";
            AddTpBtn.Size = new Size(46, 23);
            AddTpBtn.TabIndex = 1;
            AddTpBtn.Text = "+ TP";
            AddTpBtn.UseVisualStyleBackColor = false;
            AddTpBtn.Click += addTp_Click;
            // 
            // tradeEntry
            // 
            tradeEntry.BackColor = SystemColors.ActiveCaptionText;
            tradeEntry.DecimalPlaces = 8;
            tradeEntry.ForeColor = Color.FromArgb(128, 255, 128);
            tradeEntry.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tradeEntry.Location = new Point(128, 48);
            tradeEntry.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tradeEntry.Name = "tradeEntry";
            tradeEntry.Size = new Size(105, 23);
            tradeEntry.TabIndex = 9;
            tradeEntry.ValueChanged += tradeEntry_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 50);
            label10.Name = "label10";
            label10.Size = new Size(116, 15);
            label10.TabIndex = 8;
            label10.Text = "Trade Entry Price ($):";
            // 
            // calcBtn
            // 
            calcBtn.BackColor = SystemColors.ActiveCaptionText;
            calcBtn.FlatStyle = FlatStyle.Flat;
            calcBtn.ForeColor = Color.RoyalBlue;
            calcBtn.Location = new Point(318, 224);
            calcBtn.Name = "calcBtn";
            calcBtn.Size = new Size(75, 23);
            calcBtn.TabIndex = 7;
            calcBtn.Text = "Calculate";
            calcBtn.UseVisualStyleBackColor = false;
            calcBtn.Click += calcBtn_Click;
            // 
            // tradeRR
            // 
            tradeRR.BackColor = SystemColors.ActiveCaptionText;
            tradeRR.DecimalPlaces = 2;
            tradeRR.ForeColor = Color.FromArgb(128, 255, 128);
            tradeRR.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tradeRR.Location = new Point(171, 17);
            tradeRR.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tradeRR.Name = "tradeRR";
            tradeRR.Size = new Size(62, 23);
            tradeRR.TabIndex = 6;
            tradeRR.ValueChanged += tradeEntry_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(138, 15);
            label3.TabIndex = 5;
            label3.Text = "Trade Risk/Reward Ratio:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(gainTxt);
            groupBox2.Controls.Add(lossTxt);
            groupBox2.Controls.Add(qtyTxt);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(CancelBtn);
            groupBox2.Controls.Add(MarketBtn);
            groupBox2.Location = new Point(12, 289);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(399, 145);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data";
            // 
            // gainTxt
            // 
            gainTxt.AutoSize = true;
            gainTxt.Location = new Point(110, 78);
            gainTxt.Name = "gainTxt";
            gainTxt.Size = new Size(0, 15);
            gainTxt.TabIndex = 18;
            // 
            // lossTxt
            // 
            lossTxt.AutoSize = true;
            lossTxt.ForeColor = Color.Red;
            lossTxt.Location = new Point(110, 53);
            lossTxt.Name = "lossTxt";
            lossTxt.Size = new Size(0, 15);
            lossTxt.TabIndex = 17;
            // 
            // qtyTxt
            // 
            qtyTxt.AutoSize = true;
            qtyTxt.ForeColor = Color.FromArgb(128, 128, 255);
            qtyTxt.Location = new Point(110, 29);
            qtyTxt.Name = "qtyTxt";
            qtyTxt.Size = new Size(0, 15);
            qtyTxt.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(11, 78);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 15;
            label8.Text = "Projected Gain:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Red;
            label6.Location = new Point(11, 53);
            label6.Name = "label6";
            label6.Size = new Size(86, 15);
            label6.TabIndex = 14;
            label6.Text = "Projected Loss:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(128, 128, 255);
            label4.Location = new Point(11, 29);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 13;
            label4.Text = "Target Quantity:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(128, 128, 255);
            label7.Location = new Point(7, 0);
            label7.Name = "label7";
            label7.Size = new Size(56, 15);
            label7.TabIndex = 12;
            label7.Text = "Algo Info";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, -1);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 12;
            // 
            // CancelBtn
            // 
            CancelBtn.BackColor = SystemColors.ActiveCaptionText;
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.ForeColor = Color.Red;
            CancelBtn.Location = new Point(8, 116);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 10;
            CancelBtn.Text = "Cancel Trade";
            CancelBtn.UseVisualStyleBackColor = false;
            // 
            // MarketBtn
            // 
            MarketBtn.BackColor = SystemColors.ActiveCaptionText;
            MarketBtn.FlatStyle = FlatStyle.Flat;
            MarketBtn.ForeColor = Color.Lime;
            MarketBtn.Location = new Point(318, 116);
            MarketBtn.Name = "MarketBtn";
            MarketBtn.Size = new Size(75, 23);
            MarketBtn.TabIndex = 9;
            MarketBtn.Text = "Market Entry";
            MarketBtn.UseVisualStyleBackColor = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { balTxt, settingsToolStripMenuItem, DailyPnlTxt, riskTxt });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size(574, 27);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // balTxt
            // 
            balTxt.Alignment = ToolStripItemAlignment.Right;
            balTxt.Name = "balTxt";
            balTxt.Size = new Size(100, 23);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.ForeColor = Color.Black;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 23);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // DailyPnlTxt
            // 
            DailyPnlTxt.Alignment = ToolStripItemAlignment.Right;
            DailyPnlTxt.Name = "DailyPnlTxt";
            DailyPnlTxt.Size = new Size(100, 23);
            // 
            // riskTxt
            // 
            riskTxt.Alignment = ToolStripItemAlignment.Right;
            riskTxt.Name = "riskTxt";
            riskTxt.Size = new Size(100, 23);
            // 
            // tradePanel
            // 
            tradePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tradePanel.AutoScroll = true;
            tradePanel.Location = new Point(417, 46);
            tradePanel.Name = "tradePanel";
            tradePanel.Size = new Size(145, 388);
            tradePanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(417, 30);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 10;
            label1.Text = "Trades";
            // 
            // TradeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(574, 446);
            Controls.Add(label1);
            Controls.Add(tradePanel);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            ForeColor = Color.FromArgb(128, 255, 128);
            MainMenuStrip = menuStrip1;
            Name = "TradeForm";
            Text = "ACE Risk Management";
            Load += TradeForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tradeEntry).EndInit();
            ((System.ComponentModel.ISupportInitialize)tradeRR).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown tradeRR;
        private Label label3;
        private Button calcBtn;
        private GroupBox groupBox2;
        private NumericUpDown tradeEntry;
        private Label label10;
        private MenuStrip menuStrip1;
        private ToolStripTextBox balTxt;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripTextBox DailyPnlTxt;
        private ToolStripTextBox riskTxt;
        private Panel list;
        private Button AddTpBtn;
        private Button RemoveTpBtn;
        private Label label2;
        private Label label5;
        private Button CancelBtn;
        private Button MarketBtn;
        private Panel tradePanel;
        private Label label1;
        private Label label7;
        private Label label12;
        private Label label11;
        private Panel orderPanel;
        private Label gainTxt;
        private Label lossTxt;
        private Label qtyTxt;
        private Label label8;
        private Label label6;
        private Label label4;
    }
}