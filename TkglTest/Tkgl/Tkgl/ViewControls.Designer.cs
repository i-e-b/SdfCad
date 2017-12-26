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
            this.label4 = new System.Windows.Forms.Label();
            this.camDistTrack = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.camFovTrack = new System.Windows.Forms.TrackBar();
            this.camYTrack = new System.Windows.Forms.TrackBar();
            this.ambientOccCheckbox = new System.Windows.Forms.CheckBox();
            this.shadowCheckbox = new System.Windows.Forms.CheckBox();
            this.reflectionsCheckbox = new System.Windows.Forms.CheckBox();
            this.nearFieldTrack = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.slicePreviewCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.camXZTrack)).BeginInit();
            this.cameraGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camDistTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camFovTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camYTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearFieldTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // camXZTrack
            // 
            this.camXZTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camXZTrack.AutoSize = false;
            this.camXZTrack.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.camXZTrack.Location = new System.Drawing.Point(32, 19);
            this.camXZTrack.Maximum = 628;
            this.camXZTrack.Name = "camXZTrack";
            this.camXZTrack.Size = new System.Drawing.Size(596, 24);
            this.camXZTrack.TabIndex = 0;
            this.camXZTrack.TickFrequency = 31;
            this.camXZTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.camXZTrack.ValueChanged += new System.EventHandler(this.camXZTrack_ValueChanged);
            // 
            // cameraGroupBox
            // 
            this.cameraGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraGroupBox.Controls.Add(this.label4);
            this.cameraGroupBox.Controls.Add(this.camDistTrack);
            this.cameraGroupBox.Controls.Add(this.label3);
            this.cameraGroupBox.Controls.Add(this.label2);
            this.cameraGroupBox.Controls.Add(this.label1);
            this.cameraGroupBox.Controls.Add(this.camFovTrack);
            this.cameraGroupBox.Controls.Add(this.camYTrack);
            this.cameraGroupBox.Controls.Add(this.camXZTrack);
            this.cameraGroupBox.Location = new System.Drawing.Point(12, 12);
            this.cameraGroupBox.Name = "cameraGroupBox";
            this.cameraGroupBox.Size = new System.Drawing.Size(691, 133);
            this.cameraGroupBox.TabIndex = 1;
            this.cameraGroupBox.TabStop = false;
            this.cameraGroupBox.Text = "Camera";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(618, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Field of View";
            // 
            // camDistTrack
            // 
            this.camDistTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camDistTrack.AutoSize = false;
            this.camDistTrack.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.camDistTrack.Location = new System.Drawing.Point(32, 82);
            this.camDistTrack.Maximum = 1000;
            this.camDistTrack.Name = "camDistTrack";
            this.camDistTrack.Size = new System.Drawing.Size(596, 24);
            this.camDistTrack.TabIndex = 6;
            this.camDistTrack.TickFrequency = 10;
            this.camDistTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.camDistTrack.Value = 500;
            this.camDistTrack.ValueChanged += new System.EventHandler(this.camDistTrack_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "D";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "θ";
            // 
            // camFovTrack
            // 
            this.camFovTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.camFovTrack.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.camFovTrack.Location = new System.Drawing.Point(640, 19);
            this.camFovTrack.Maximum = 100;
            this.camFovTrack.Name = "camFovTrack";
            this.camFovTrack.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.camFovTrack.Size = new System.Drawing.Size(45, 87);
            this.camFovTrack.TabIndex = 2;
            this.camFovTrack.TickFrequency = 5;
            this.camFovTrack.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.camFovTrack.Value = 50;
            this.camFovTrack.ValueChanged += new System.EventHandler(this.camFovTrack_ValueChanged);
            // 
            // camYTrack
            // 
            this.camYTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camYTrack.AutoSize = false;
            this.camYTrack.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.camYTrack.Location = new System.Drawing.Point(32, 50);
            this.camYTrack.Maximum = 1000;
            this.camYTrack.Name = "camYTrack";
            this.camYTrack.Size = new System.Drawing.Size(596, 24);
            this.camYTrack.TabIndex = 1;
            this.camYTrack.TickFrequency = 31;
            this.camYTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.camYTrack.Value = 100;
            this.camYTrack.ValueChanged += new System.EventHandler(this.camYTrack_ValueChanged);
            // 
            // ambientOccCheckbox
            // 
            this.ambientOccCheckbox.AutoSize = true;
            this.ambientOccCheckbox.Checked = true;
            this.ambientOccCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ambientOccCheckbox.Location = new System.Drawing.Point(12, 151);
            this.ambientOccCheckbox.Name = "ambientOccCheckbox";
            this.ambientOccCheckbox.Size = new System.Drawing.Size(114, 17);
            this.ambientOccCheckbox.TabIndex = 2;
            this.ambientOccCheckbox.Text = "Ambient Occlusion";
            this.ambientOccCheckbox.UseVisualStyleBackColor = true;
            this.ambientOccCheckbox.CheckedChanged += new System.EventHandler(this.ambientOccCheckbox_CheckedChanged);
            // 
            // shadowCheckbox
            // 
            this.shadowCheckbox.AutoSize = true;
            this.shadowCheckbox.Location = new System.Drawing.Point(132, 151);
            this.shadowCheckbox.Name = "shadowCheckbox";
            this.shadowCheckbox.Size = new System.Drawing.Size(70, 17);
            this.shadowCheckbox.TabIndex = 3;
            this.shadowCheckbox.Text = "Shadows";
            this.shadowCheckbox.UseVisualStyleBackColor = true;
            this.shadowCheckbox.CheckedChanged += new System.EventHandler(this.shadowCheckbox_CheckedChanged);
            // 
            // reflectionsCheckbox
            // 
            this.reflectionsCheckbox.AutoSize = true;
            this.reflectionsCheckbox.Location = new System.Drawing.Point(208, 151);
            this.reflectionsCheckbox.Name = "reflectionsCheckbox";
            this.reflectionsCheckbox.Size = new System.Drawing.Size(79, 17);
            this.reflectionsCheckbox.TabIndex = 4;
            this.reflectionsCheckbox.Text = "Reflections";
            this.reflectionsCheckbox.UseVisualStyleBackColor = true;
            this.reflectionsCheckbox.CheckedChanged += new System.EventHandler(this.reflectionsCheckbox_CheckedChanged);
            // 
            // nearFieldTrack
            // 
            this.nearFieldTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nearFieldTrack.AutoSize = false;
            this.nearFieldTrack.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.nearFieldTrack.Location = new System.Drawing.Point(12, 211);
            this.nearFieldTrack.Maximum = 1000;
            this.nearFieldTrack.Name = "nearFieldTrack";
            this.nearFieldTrack.Size = new System.Drawing.Size(685, 24);
            this.nearFieldTrack.TabIndex = 9;
            this.nearFieldTrack.TickFrequency = 10;
            this.nearFieldTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.nearFieldTrack.Value = 50;
            this.nearFieldTrack.ValueChanged += new System.EventHandler(this.nearFieldTrack_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Slice start";
            // 
            // slicePreviewCheckBox
            // 
            this.slicePreviewCheckBox.AutoSize = true;
            this.slicePreviewCheckBox.Location = new System.Drawing.Point(12, 174);
            this.slicePreviewCheckBox.Name = "slicePreviewCheckBox";
            this.slicePreviewCheckBox.Size = new System.Drawing.Size(90, 17);
            this.slicePreviewCheckBox.TabIndex = 10;
            this.slicePreviewCheckBox.Text = "Slice Preview";
            this.slicePreviewCheckBox.UseVisualStyleBackColor = true;
            this.slicePreviewCheckBox.CheckedChanged += new System.EventHandler(this.slicePreviewCheckBox_CheckedChanged);
            // 
            // ViewControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 481);
            this.ControlBox = false;
            this.Controls.Add(this.slicePreviewCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nearFieldTrack);
            this.Controls.Add(this.reflectionsCheckbox);
            this.Controls.Add(this.shadowCheckbox);
            this.Controls.Add(this.ambientOccCheckbox);
            this.Controls.Add(this.cameraGroupBox);
            this.Name = "ViewControls";
            this.Text = "SdfCad - Control";
            ((System.ComponentModel.ISupportInitialize)(this.camXZTrack)).EndInit();
            this.cameraGroupBox.ResumeLayout(false);
            this.cameraGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camDistTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camFovTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camYTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nearFieldTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar camXZTrack;
        private System.Windows.Forms.GroupBox cameraGroupBox;
        private System.Windows.Forms.TrackBar camFovTrack;
        private System.Windows.Forms.TrackBar camYTrack;
        private System.Windows.Forms.TrackBar camDistTrack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ambientOccCheckbox;
        private System.Windows.Forms.CheckBox shadowCheckbox;
        private System.Windows.Forms.CheckBox reflectionsCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar nearFieldTrack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox slicePreviewCheckBox;
    }
}