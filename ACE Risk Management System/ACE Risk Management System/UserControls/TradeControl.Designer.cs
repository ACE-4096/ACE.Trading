namespace ACE_Risk_Management_System.UserControls
{
    partial class TradeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tickerLabel = new Label();
            qtyLabel = new Label();
            lossLabel = new Label();
            gainLabel = new Label();
            statusLabel = new Label();
            SuspendLayout();
            // 
            // tickerLabel
            // 
            tickerLabel.AutoSize = true;
            tickerLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tickerLabel.ForeColor = Color.FromArgb(192, 0, 192);
            tickerLabel.Location = new Point(5, 5);
            tickerLabel.Name = "tickerLabel";
            tickerLabel.Size = new Size(42, 15);
            tickerLabel.TabIndex = 0;
            tickerLabel.Text = "Ticker";
            // 
            // qtyLabel
            // 
            qtyLabel.AutoSize = true;
            qtyLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            qtyLabel.Location = new Point(5, 24);
            qtyLabel.Name = "qtyLabel";
            qtyLabel.Size = new Size(56, 15);
            qtyLabel.TabIndex = 1;
            qtyLabel.Text = "Quantity:";
            // 
            // lossLabel
            // 
            lossLabel.AutoSize = true;
            lossLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lossLabel.Location = new Point(5, 39);
            lossLabel.Name = "lossLabel";
            lossLabel.Size = new Size(54, 15);
            lossLabel.TabIndex = 2;
            lossLabel.Text = "Est. Loss:";
            // 
            // gainLabel
            // 
            gainLabel.AutoSize = true;
            gainLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            gainLabel.Location = new Point(5, 54);
            gainLabel.Name = "gainLabel";
            gainLabel.Size = new Size(55, 15);
            gainLabel.TabIndex = 3;
            gainLabel.Text = "Est. Gain:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.Location = new Point(88, 5);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(42, 15);
            statusLabel.TabIndex = 4;
            statusLabel.Text = "Status";
            // 
            // TradeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(statusLabel);
            Controls.Add(gainLabel);
            Controls.Add(lossLabel);
            Controls.Add(qtyLabel);
            Controls.Add(tickerLabel);
            Name = "TradeControl";
            Size = new Size(141, 77);
            Load += TradeControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tickerLabel;
        private Label qtyLabel;
        private Label lossLabel;
        private Label gainLabel;
        private Label statusLabel;
    }
}
