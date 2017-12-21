namespace Tkgl
{
    partial class ViewControls
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
            this.camXZTrack = new System.Windows.Forms.TrackBar();
            this.cameraGroupBox = new System.Windows.Forms.GroupBox();
            this.camYTrack = new System.Windows.Forms.TrackBar();
            this.camFovTrack = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.camXZTrack)).BeginInit();
            this.cameraGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camYTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camFovTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // camXZTrack
            // 
            this.camXZTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camXZTrack.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.camXZTrack.Location = new System.Drawing.Point(6, 19);
            this.camXZTrack.Maximum = 628;
            this.camXZTrack.Name = "camXZTrack";
            this.camXZTrack.Size = new System.Drawing.Size(414, 45);
            this.camXZTrack.TabIndex = 0;
            this.camXZTrack.TickFrequency = 31;
            this.camXZTrack.ValueChanged += new System.EventHandler(this.camXZTrack_ValueChanged);
            // 
            // cameraGroupBox
            // 
            this.cameraGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraGroupBox.Controls.Add(this.camFovTrack);
            this.cameraGroupBox.Controls.Add(this.camYTrack);
            this.cameraGroupBox.Controls.Add(this.camXZTrack);
            this.cameraGroupBox.Location = new System.Drawing.Point(12, 12);
            this.cameraGroupBox.Name = "cameraGroupBox";
            this.cameraGroupBox.Size = new System.Drawing.Size(426, 175);
            this.cameraGroupBox.TabIndex = 1;
            this.cameraGroupBox.TabStop = false;
            this.cameraGroupBox.Text = "Camera";
            // 
            // camYTrack
            // 
            this.camYTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camYTrack.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.camYTrack.Location = new System.Drawing.Point(6, 70);
            this.camYTrack.Maximum = 628;
            this.camYTrack.Name = "camYTrack";
            this.camYTrack.Size = new System.Drawing.Size(414, 45);
            this.camYTrack.TabIndex = 1;
            this.camYTrack.TickFrequency = 31;
            this.camYTrack.ValueChanged += new System.EventHandler(this.camYTrack_ValueChanged);
            // 
            // camFovTrack
            // 
            this.camFovTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camFovTrack.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.camFovTrack.Location = new System.Drawing.Point(6, 121);
            this.camFovTrack.Maximum = 100;
            this.camFovTrack.Name = "camFovTrack";
            this.camFovTrack.Size = new System.Drawing.Size(414, 45);
            this.camFovTrack.TabIndex = 2;
            this.camFovTrack.TickFrequency = 5;
            this.camFovTrack.Value = 50;
            this.camFovTrack.ValueChanged += new System.EventHandler(this.camFovTrack_ValueChanged);
            // 
            // ViewControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 275);
            this.ControlBox = false;
            this.Controls.Add(this.cameraGroupBox);
            this.Name = "ViewControls";
            this.Text = "SdfCad - Preview";
            ((System.ComponentModel.ISupportInitialize)(this.camXZTrack)).EndInit();
            this.cameraGroupBox.ResumeLayout(false);
            this.cameraGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camYTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camFovTrack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar camXZTrack;
        private System.Windows.Forms.GroupBox cameraGroupBox;
        private System.Windows.Forms.TrackBar camFovTrack;
        private System.Windows.Forms.TrackBar camYTrack;
    }
}