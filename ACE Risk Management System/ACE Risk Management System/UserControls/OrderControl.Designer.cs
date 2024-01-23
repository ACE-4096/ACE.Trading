namespace ACE_Risk_Management_System.UserControls
{
    partial class OrderControl
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
            limitPrice = new NumericUpDown();
            label = new Label();
            percentage = new NumericUpDown();
            ValueLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)limitPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)percentage).BeginInit();
            SuspendLayout();
            // 
            // limitPrice
            // 
            limitPrice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            limitPrice.DecimalPlaces = 8;
            limitPrice.Location = new Point(45, 3);
            limitPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            limitPrice.Name = "limitPrice";
            limitPrice.Size = new Size(107, 23);
            limitPrice.TabIndex = 0;
            limitPrice.ValueChanged += percentage_ValueChanged;
            // 
            // label
            // 
            label.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label.AutoSize = true;
            label.Location = new Point(3, 5);
            label.Name = "label";
            label.Size = new Size(31, 15);
            label.TabIndex = 1;
            label.Text = "Tp 0:";
            // 
            // percentage
            // 
            percentage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            percentage.DecimalPlaces = 2;
            percentage.Location = new Point(158, 3);
            percentage.Name = "percentage";
            percentage.Size = new Size(65, 23);
            percentage.TabIndex = 2;
            percentage.ValueChanged += percentage_ValueChanged;
            // 
            // ValueLabel
            // 
            ValueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ValueLabel.AutoSize = true;
            ValueLabel.Location = new Point(238, 5);
            ValueLabel.Name = "ValueLabel";
            ValueLabel.Size = new Size(0, 15);
            ValueLabel.TabIndex = 3;
            // 
            // OrderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(ValueLabel);
            Controls.Add(percentage);
            Controls.Add(label);
            Controls.Add(limitPrice);
            Name = "OrderControl";
            Size = new Size(319, 27);
            Load += OrderControl_Load;
            ((System.ComponentModel.ISupportInitialize)limitPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)percentage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown limitPrice;
        private Label label;
        private NumericUpDown percentage;
        private Label ValueLabel;
    }
}
