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
            tradeStop = new NumericUpDown();
            label5 = new Label();
            tradeEntry = new NumericUpDown();
            label10 = new Label();
            button1 = new Button();
            tradeRR = new NumericUpDown();
            label3 = new Label();
            riskLimit = new NumericUpDown();
            label2 = new Label();
            accSize = new NumericUpDown();
            label1 = new Label();
            groupBox2 = new GroupBox();
            qty = new Label();
            label8 = new Label();
            potGain = new Label();
            label9 = new Label();
            potLoss = new Label();
            label6 = new Label();
            sizeToRisk = new Label();
            label4 = new Label();
            groupBox3 = new GroupBox();
            useFees = new CheckBox();
            fees = new NumericUpDown();
            label7 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tradeStop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tradeEntry).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tradeRR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)riskLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accSize).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fees).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tradeStop);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(tradeEntry);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(tradeRR);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(riskLimit);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(accSize);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(252, 210);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Risk Calculator";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // tradeStop
            // 
            tradeStop.DecimalPlaces = 8;
            tradeStop.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tradeStop.Location = new Point(130, 152);
            tradeStop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tradeStop.Name = "tradeStop";
            tradeStop.Size = new Size(105, 23);
            tradeStop.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 154);
            label5.Name = "label5";
            label5.Size = new Size(77, 15);
            label5.TabIndex = 10;
            label5.Text = "Stop Loss ($):";
            // 
            // tradeEntry
            // 
            tradeEntry.DecimalPlaces = 8;
            tradeEntry.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tradeEntry.Location = new Point(130, 123);
            tradeEntry.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tradeEntry.Name = "tradeEntry";
            tradeEntry.Size = new Size(105, 23);
            tradeEntry.TabIndex = 9;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 125);
            label10.Name = "label10";
            label10.Size = new Size(116, 15);
            label10.TabIndex = 8;
            label10.Text = "Trade Entry Price ($):";
            // 
            // button1
            // 
            button1.Location = new Point(76, 181);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 7;
            button1.Text = "Calc";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tradeRR
            // 
            tradeRR.DecimalPlaces = 2;
            tradeRR.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tradeRR.Location = new Point(173, 86);
            tradeRR.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            tradeRR.Name = "tradeRR";
            tradeRR.Size = new Size(62, 23);
            tradeRR.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 88);
            label3.Name = "label3";
            label3.Size = new Size(138, 15);
            label3.TabIndex = 5;
            label3.Text = "Trade Risk/Reward Ratio:";
            // 
            // riskLimit
            // 
            riskLimit.DecimalPlaces = 2;
            riskLimit.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            riskLimit.Location = new Point(173, 57);
            riskLimit.Name = "riskLimit";
            riskLimit.Size = new Size(62, 23);
            riskLimit.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 59);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 3;
            label2.Text = "Risk Limit (%):";
            // 
            // accSize
            // 
            accSize.DecimalPlaces = 2;
            accSize.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            accSize.Location = new Point(128, 28);
            accSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            accSize.Name = "accSize";
            accSize.Size = new Size(107, 23);
            accSize.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 30);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 1;
            label1.Text = "Account Size ($):";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(qty);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(potGain);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(potLoss);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(sizeToRisk);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(12, 345);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(252, 89);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data";
            // 
            // qty
            // 
            qty.AutoSize = true;
            qty.Location = new Point(94, 65);
            qty.Name = "qty";
            qty.Size = new Size(0, 15);
            qty.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 65);
            label8.Name = "label8";
            label8.Size = new Size(66, 15);
            label8.TabIndex = 7;
            label8.Text = "Target Qty:";
            // 
            // potGain
            // 
            potGain.AutoSize = true;
            potGain.Location = new Point(94, 49);
            potGain.Name = "potGain";
            potGain.Size = new Size(0, 15);
            potGain.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 49);
            label9.Name = "label9";
            label9.Size = new Size(86, 15);
            label9.TabIndex = 5;
            label9.Text = "Potencial Gain:";
            // 
            // potLoss
            // 
            potLoss.AutoSize = true;
            potLoss.Location = new Point(94, 34);
            potLoss.Name = "potLoss";
            potLoss.Size = new Size(0, 15);
            potLoss.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 34);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 3;
            label6.Text = "Potencial Loss:";
            // 
            // sizeToRisk
            // 
            sizeToRisk.AutoSize = true;
            sizeToRisk.Location = new Point(76, 19);
            sizeToRisk.Name = "sizeToRisk";
            sizeToRisk.Size = new Size(0, 15);
            sizeToRisk.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 0;
            label4.Text = "Size to Risk:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(useFees);
            groupBox3.Controls.Add(fees);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(270, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(144, 109);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Trading Fees";
            // 
            // useFees
            // 
            useFees.AutoSize = true;
            useFees.Location = new Point(16, 57);
            useFees.Name = "useFees";
            useFees.Size = new Size(103, 19);
            useFees.TabIndex = 14;
            useFees.Text = "Calc With Fees";
            useFees.UseVisualStyleBackColor = true;
            // 
            // fees
            // 
            fees.DecimalPlaces = 2;
            fees.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            fees.Location = new Point(57, 25);
            fees.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            fees.Name = "fees";
            fees.Size = new Size(75, 23);
            fees.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 27);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 12;
            label7.Text = "Fee ($):";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 446);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "ACE Risk Management";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tradeStop).EndInit();
            ((System.ComponentModel.ISupportInitialize)tradeEntry).EndInit();
            ((System.ComponentModel.ISupportInitialize)tradeRR).EndInit();
            ((System.ComponentModel.ISupportInitialize)riskLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)accSize).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fees).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown riskLimit;
        private Label label2;
        private NumericUpDown accSize;
        private Label label1;
        private NumericUpDown tradeRR;
        private Label label3;
        private Button button1;
        private GroupBox groupBox2;
        private Label potGain;
        private Label label9;
        private Label potLoss;
        private Label label6;
        private Label sizeToRisk;
        private Label label4;
        private NumericUpDown tradeEntry;
        private Label label10;
        private NumericUpDown tradeStop;
        private Label label5;
        private Label qty;
        private Label label8;
        private GroupBox groupBox3;
        private NumericUpDown fees;
        private Label label7;
        private CheckBox useFees;
    }
}