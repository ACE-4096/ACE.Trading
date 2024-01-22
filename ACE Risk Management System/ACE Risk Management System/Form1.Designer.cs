namespace ACE_Risk_Management_System
{
    partial class Form1
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
            label12 = new Label();
            label11 = new Label();
            label2 = new Label();
            RemoveTpBtn = new Button();
            orderPanel = new Panel();
            AddTpBtn = new Button();
            tradeEntry = new NumericUpDown();
            label10 = new Label();
            calcBtn = new Button();
            tradeRR = new NumericUpDown();
            label3 = new Label();
            groupBox2 = new GroupBox();
            label7 = new Label();
            label5 = new Label();
            CancelBtn = new Button();
            MarketBtn = new Button();
            qty = new Label();
            label8 = new Label();
            potGain = new Label();
            label9 = new Label();
            potLoss = new Label();
            label6 = new Label();
            sizeToRisk = new Label();
            label4 = new Label();
            menuStrip1 = new MenuStrip();
            toolStripTextBox1 = new ToolStripTextBox();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripTextBox2 = new ToolStripTextBox();
            toolStripTextBox3 = new ToolStripTextBox();
            panel1 = new Panel();
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
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(RemoveTpBtn);
            groupBox1.Controls.Add(orderPanel);
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
            label2.Size = new Size(59, 15);
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
            RemoveTpBtn.Location = new Point(334, 42);
            RemoveTpBtn.Name = "RemoveTpBtn";
            RemoveTpBtn.Size = new Size(59, 23);
            RemoveTpBtn.TabIndex = 10;
            RemoveTpBtn.Text = "- TP";
            RemoveTpBtn.UseVisualStyleBackColor = false;
            // 
            // orderPanel
            // 
            orderPanel.Location = new Point(6, 99);
            orderPanel.Name = "orderPanel";
            orderPanel.Size = new Size(387, 119);
            orderPanel.TabIndex = 0;
            // 
            // AddTpBtn
            // 
            AddTpBtn.BackColor = SystemColors.ActiveCaptionText;
            AddTpBtn.FlatAppearance.BorderColor = Color.FromArgb(128, 255, 128);
            AddTpBtn.FlatStyle = FlatStyle.Flat;
            AddTpBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            AddTpBtn.ForeColor = Color.FromArgb(128, 255, 128);
            AddTpBtn.Location = new Point(334, 15);
            AddTpBtn.Name = "AddTpBtn";
            AddTpBtn.Size = new Size(59, 23);
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
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 50);
            label10.Name = "label10";
            label10.Size = new Size(114, 15);
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
            calcBtn.Click += button1_Click;
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
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(136, 15);
            label3.TabIndex = 5;
            label3.Text = "Trade Risk/Reward Ratio:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(CancelBtn);
            groupBox2.Controls.Add(MarketBtn);
            groupBox2.Controls.Add(qty);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(potGain);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(potLoss);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(sizeToRisk);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(12, 289);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(399, 145);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data";
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
            // qty
            // 
            qty.AutoSize = true;
            qty.ForeColor = Color.FromArgb(128, 128, 255);
            qty.Location = new Point(94, 65);
            qty.Name = "qty";
            qty.Size = new Size(0, 15);
            qty.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.FromArgb(128, 128, 255);
            label8.Location = new Point(6, 65);
            label8.Name = "label8";
            label8.Size = new Size(64, 15);
            label8.TabIndex = 7;
            label8.Text = "Target Qty:";
            // 
            // potGain
            // 
            potGain.AutoSize = true;
            potGain.ForeColor = Color.FromArgb(128, 128, 255);
            potGain.Location = new Point(94, 49);
            potGain.Name = "potGain";
            potGain.Size = new Size(0, 15);
            potGain.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(128, 128, 255);
            label9.Location = new Point(6, 49);
            label9.Name = "label9";
            label9.Size = new Size(86, 15);
            label9.TabIndex = 5;
            label9.Text = "Potencial Gain:";
            // 
            // potLoss
            // 
            potLoss.AutoSize = true;
            potLoss.ForeColor = Color.FromArgb(128, 128, 255);
            potLoss.Location = new Point(94, 34);
            potLoss.Name = "potLoss";
            potLoss.Size = new Size(0, 15);
            potLoss.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(128, 128, 255);
            label6.Location = new Point(6, 34);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 3;
            label6.Text = "Potencial Loss:";
            // 
            // sizeToRisk
            // 
            sizeToRisk.AutoSize = true;
            sizeToRisk.ForeColor = Color.FromArgb(128, 128, 255);
            sizeToRisk.Location = new Point(76, 19);
            sizeToRisk.Name = "sizeToRisk";
            sizeToRisk.Size = new Size(0, 15);
            sizeToRisk.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(128, 128, 255);
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 0;
            label4.Text = "Size to Risk:";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripTextBox1, settingsToolStripMenuItem, toolStripTextBox2, toolStripTextBox3 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(574, 27);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Alignment = ToolStripItemAlignment.Right;
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 23);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.ForeColor = Color.Black;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 23);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripTextBox2
            // 
            toolStripTextBox2.Alignment = ToolStripItemAlignment.Right;
            toolStripTextBox2.Name = "toolStripTextBox2";
            toolStripTextBox2.Size = new Size(100, 23);
            // 
            // toolStripTextBox3
            // 
            toolStripTextBox3.Alignment = ToolStripItemAlignment.Right;
            toolStripTextBox3.Name = "toolStripTextBox3";
            toolStripTextBox3.Size = new Size(100, 23);
            // 
            // panel1
            // 
            panel1.Location = new Point(417, 46);
            panel1.Name = "panel1";
            panel1.Size = new Size(145, 388);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(417, 30);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 10;
            label1.Text = "Trades";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(574, 446);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            ForeColor = Color.FromArgb(128, 255, 128);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "ACE Risk Management";
            Load += Form1_Load;
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
        private Label potGain;
        private Label label9;
        private Label potLoss;
        private Label label6;
        private Label sizeToRisk;
        private Label label4;
        private NumericUpDown tradeEntry;
        private Label label10;
        private Label qty;
        private Label label8;
        private MenuStrip menuStrip1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripTextBox toolStripTextBox3;
        private Panel orderPanel;
        private Panel list;
        private Button AddTpBtn;
        private Button RemoveTpBtn;
        private Label label2;
        private Label label5;
        private Button CancelBtn;
        private Button MarketBtn;
        private Panel panel1;
        private Label label1;
        private Label label7;
        private Label label12;
        private Label label11;
    }
}