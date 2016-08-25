namespace bsensor
{
    partial class FrmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbBusRouteStrokeWeight = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBusSpeedStrokeWeight = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bus Route Stroke Weight:";
            // 
            // cbBusRouteStrokeWeight
            // 
            this.cbBusRouteStrokeWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBusRouteStrokeWeight.FormattingEnabled = true;
            this.cbBusRouteStrokeWeight.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbBusRouteStrokeWeight.Location = new System.Drawing.Point(159, 25);
            this.cbBusRouteStrokeWeight.Name = "cbBusRouteStrokeWeight";
            this.cbBusRouteStrokeWeight.Size = new System.Drawing.Size(42, 21);
            this.cbBusRouteStrokeWeight.TabIndex = 1;
            this.cbBusRouteStrokeWeight.SelectedValueChanged += new System.EventHandler(this.cbBusRouteStrokeWeight_SelectedValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(234, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(328, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bus Speed Stroke Weight:";
            // 
            // cbBusSpeedStrokeWeight
            // 
            this.cbBusSpeedStrokeWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBusSpeedStrokeWeight.FormattingEnabled = true;
            this.cbBusSpeedStrokeWeight.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbBusSpeedStrokeWeight.Location = new System.Drawing.Point(159, 63);
            this.cbBusSpeedStrokeWeight.Name = "cbBusSpeedStrokeWeight";
            this.cbBusSpeedStrokeWeight.Size = new System.Drawing.Size(42, 21);
            this.cbBusSpeedStrokeWeight.TabIndex = 4;
            this.cbBusSpeedStrokeWeight.SelectedValueChanged += new System.EventHandler(this.cbBusSpeedStrokeWeight_SelectedValueChanged);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 272);
            this.Controls.Add(this.cbBusSpeedStrokeWeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbBusRouteStrokeWeight);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBusRouteStrokeWeight;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBusSpeedStrokeWeight;
    }
}