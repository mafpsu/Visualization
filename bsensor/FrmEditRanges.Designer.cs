namespace bsensor
{
    partial class FrmEditColorRanges
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
            this.lvGraphColorRanges = new System.Windows.Forms.ListView();
            this.colRangeMin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRangeMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEnterRanges = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // lvGraphColorRanges
            // 
            this.lvGraphColorRanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGraphColorRanges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRangeMin,
            this.colRangeMax,
            this.colColor});
            this.lvGraphColorRanges.FullRowSelect = true;
            this.lvGraphColorRanges.GridLines = true;
            this.lvGraphColorRanges.Location = new System.Drawing.Point(12, 12);
            this.lvGraphColorRanges.MultiSelect = false;
            this.lvGraphColorRanges.Name = "lvGraphColorRanges";
            this.lvGraphColorRanges.Size = new System.Drawing.Size(298, 294);
            this.lvGraphColorRanges.TabIndex = 0;
            this.lvGraphColorRanges.UseCompatibleStateImageBehavior = false;
            this.lvGraphColorRanges.View = System.Windows.Forms.View.Details;
            this.lvGraphColorRanges.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGraphColorRanges_ItemSelectionChanged);
            // 
            // colRangeMin
            // 
            this.colRangeMin.Text = "Min";
            // 
            // colRangeMax
            // 
            this.colRangeMax.Text = "Max";
            // 
            // colColor
            // 
            this.colColor.Text = "Color";
            // 
            // btnEnterRanges
            // 
            this.btnEnterRanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnterRanges.Location = new System.Drawing.Point(316, 12);
            this.btnEnterRanges.Name = "btnEnterRanges";
            this.btnEnterRanges.Size = new System.Drawing.Size(92, 32);
            this.btnEnterRanges.TabIndex = 1;
            this.btnEnterRanges.Text = "Enter Ranges...";
            this.btnEnterRanges.UseVisualStyleBackColor = true;
            this.btnEnterRanges.Click += new System.EventHandler(this.btnEnterRanges_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(316, 236);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 32);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(316, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnEditColor
            // 
            this.btnEditColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditColor.Enabled = false;
            this.btnEditColor.Location = new System.Drawing.Point(316, 50);
            this.btnEditColor.Name = "btnEditColor";
            this.btnEditColor.Size = new System.Drawing.Size(92, 32);
            this.btnEditColor.TabIndex = 4;
            this.btnEditColor.Text = "Edit Color...";
            this.btnEditColor.UseVisualStyleBackColor = true;
            this.btnEditColor.Click += new System.EventHandler(this.btnEditColor_Click);
            // 
            // FrmEditColorRanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 319);
            this.Controls.Add(this.btnEditColor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnEnterRanges);
            this.Controls.Add(this.lvGraphColorRanges);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditColorRanges";
            this.Text = "Edit Graph Color Ranges";
            this.Load += new System.EventHandler(this.FrmEditColorRanges_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGraphColorRanges;
        private System.Windows.Forms.ColumnHeader colColor;
        private System.Windows.Forms.ColumnHeader colRangeMin;
        private System.Windows.Forms.Button btnEnterRanges;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader colRangeMax;
        private System.Windows.Forms.Button btnEditColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}