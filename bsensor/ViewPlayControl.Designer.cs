namespace bsensor
{
    partial class ViewPlayControl
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
            this.btnPlayReverse = new System.Windows.Forms.Button();
            this.btnStepReverse = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayForward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPlayReverse
            // 
            this.btnPlayReverse.Location = new System.Drawing.Point(28, 0);
            this.btnPlayReverse.Name = "btnPlayReverse";
            this.btnPlayReverse.Size = new System.Drawing.Size(65, 32);
            this.btnPlayReverse.TabIndex = 18;
            this.btnPlayReverse.Text = "<<< Play";
            this.btnPlayReverse.UseVisualStyleBackColor = true;
            // 
            // btnStepReverse
            // 
            this.btnStepReverse.Location = new System.Drawing.Point(0, 0);
            this.btnStepReverse.Name = "btnStepReverse";
            this.btnStepReverse.Size = new System.Drawing.Size(22, 32);
            this.btnStepReverse.TabIndex = 17;
            this.btnStepReverse.Text = "<";
            this.btnStepReverse.UseVisualStyleBackColor = true;
            // 
            // btnStepForward
            // 
            this.btnStepForward.Location = new System.Drawing.Point(214, 0);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(21, 32);
            this.btnStepForward.TabIndex = 16;
            this.btnStepForward.Text = ">";
            this.btnStepForward.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(99, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 32);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnPlayForward
            // 
            this.btnPlayForward.Location = new System.Drawing.Point(143, 0);
            this.btnPlayForward.Name = "btnPlayForward";
            this.btnPlayForward.Size = new System.Drawing.Size(65, 32);
            this.btnPlayForward.TabIndex = 14;
            this.btnPlayForward.Text = "Play >>>";
            this.btnPlayForward.UseVisualStyleBackColor = true;
            // 
            // PlayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPlayReverse);
            this.Controls.Add(this.btnStepReverse);
            this.Controls.Add(this.btnStepForward);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlayForward);
            this.Name = "PlayControl";
            this.Size = new System.Drawing.Size(236, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayReverse;
        private System.Windows.Forms.Button btnStepReverse;
        private System.Windows.Forms.Button btnStepForward;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlayForward;
    }
}
