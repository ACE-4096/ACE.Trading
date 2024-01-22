namespace ACE_Risk_Management_System
{
    partial class SettingsForm
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
            groupBox3 = new GroupBox();
            fees = new NumericUpDown();
            label7 = new Label();
            groupBox1 = new GroupBox();
            leverage = new NumericUpDown();
            label2 = new Label();
            risk = new NumericUpDown();
            label1 = new Label();
            SaveBtn = new Button();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fees).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)leverage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)risk).BeginInit();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(fees);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(232, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(144, 93);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Fees";
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
            // groupBox1
            // 
            groupBox1.Controls.Add(leverage);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(risk);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(214, 93);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Risk / Leverage";
            // 
            // leverage
            // 
            leverage.DecimalPlaces = 2;
            leverage.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            leverage.Location = new Point(123, 53);
            leverage.Name = "leverage";
            leverage.Size = new Size(75, 23);
            leverage.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 55);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 14;
            label2.Text = "Leverage Multiplier:";
            // 
            // risk
            // 
            risk.DecimalPlaces = 2;
            risk.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            risk.Location = new Point(123, 25);
            risk.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            risk.Name = "risk";
            risk.Size = new Size(75, 23);
            risk.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 27);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 12;
            label1.Text = "Account Risk (%):";
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(302, 111);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 16;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 140);
            Controls.Add(SaveBtn);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            MaximumSize = new Size(405, 179);
            MinimumSize = new Size(405, 150);
            Name = "SettingsForm";
            Text = "Settings";
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fees).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)leverage).EndInit();
            ((System.ComponentModel.ISupportInitialize)risk).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox3;
        private NumericUpDown fees;
        private Label label7;
        private GroupBox groupBox1;
        private NumericUpDown leverage;
        private Label label2;
        private NumericUpDown risk;
        private Label label1;
        private Button SaveBtn;
    }
}