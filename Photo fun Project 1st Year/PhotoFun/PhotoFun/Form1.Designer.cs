namespace PhotoFun
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.txtRotDeg = new System.Windows.Forms.TextBox();
            this.txtScaleX = new System.Windows.Forms.TextBox();
            this.txtRotX = new System.Windows.Forms.TextBox();
            this.txtRotY = new System.Windows.Forms.TextBox();
            this.txtTransY = new System.Windows.Forms.TextBox();
            this.txtScaleY = new System.Windows.Forms.TextBox();
            this.txtTransX = new System.Windows.Forms.TextBox();
            this.lblRotationDeg = new System.Windows.Forms.Label();
            this.lblRotationX = new System.Windows.Forms.Label();
            this.lblRotationY = new System.Windows.Forms.Label();
            this.lblTransY = new System.Windows.Forms.Label();
            this.lblTransX = new System.Windows.Forms.Label();
            this.lblScaleX = new System.Windows.Forms.Label();
            this.lblScaleY = new System.Windows.Forms.Label();
            this.trkRotDeg = new System.Windows.Forms.TrackBar();
            this.trkRotX = new System.Windows.Forms.TrackBar();
            this.trkRotY = new System.Windows.Forms.TrackBar();
            this.trkTransX = new System.Windows.Forms.TrackBar();
            this.trkTransY = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.trkScaleX = new System.Windows.Forms.TrackBar();
            this.trkScaleY = new System.Windows.Forms.TrackBar();
            this.chkRotation = new System.Windows.Forms.CheckBox();
            this.chkTranslation = new System.Windows.Forms.CheckBox();
            this.chkScale = new System.Windows.Forms.CheckBox();
            this.btnPhoto = new System.Windows.Forms.Button();
            this.btnAccessory = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lstDisplay = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotDeg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkScaleY)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRotDeg
            // 
            this.txtRotDeg.Location = new System.Drawing.Point(183, 467);
            this.txtRotDeg.Name = "txtRotDeg";
            this.txtRotDeg.Size = new System.Drawing.Size(31, 20);
            this.txtRotDeg.TabIndex = 0;
            this.txtRotDeg.TextChanged += new System.EventHandler(this.txtRotDeg_TextChanged);
            // 
            // txtScaleX
            // 
            this.txtScaleX.Location = new System.Drawing.Point(646, 467);
            this.txtScaleX.Name = "txtScaleX";
            this.txtScaleX.Size = new System.Drawing.Size(31, 20);
            this.txtScaleX.TabIndex = 1;
            this.txtScaleX.TextChanged += new System.EventHandler(this.txtScaleX_TextChanged);
            // 
            // txtRotX
            // 
            this.txtRotX.Location = new System.Drawing.Point(183, 502);
            this.txtRotX.Name = "txtRotX";
            this.txtRotX.Size = new System.Drawing.Size(31, 20);
            this.txtRotX.TabIndex = 2;
            this.txtRotX.TextChanged += new System.EventHandler(this.txtRotX_TextChanged);
            // 
            // txtRotY
            // 
            this.txtRotY.Location = new System.Drawing.Point(183, 537);
            this.txtRotY.Name = "txtRotY";
            this.txtRotY.Size = new System.Drawing.Size(31, 20);
            this.txtRotY.TabIndex = 3;
            this.txtRotY.TextChanged += new System.EventHandler(this.txtRotY_TextChanged);
            // 
            // txtTransY
            // 
            this.txtTransY.Location = new System.Drawing.Point(410, 505);
            this.txtTransY.Name = "txtTransY";
            this.txtTransY.Size = new System.Drawing.Size(31, 20);
            this.txtTransY.TabIndex = 4;
            this.txtTransY.TextChanged += new System.EventHandler(this.txtTransY_TextChanged);
            // 
            // txtScaleY
            // 
            this.txtScaleY.Location = new System.Drawing.Point(646, 498);
            this.txtScaleY.Name = "txtScaleY";
            this.txtScaleY.Size = new System.Drawing.Size(31, 20);
            this.txtScaleY.TabIndex = 5;
            this.txtScaleY.TextChanged += new System.EventHandler(this.txtScaleY_TextChanged);
            // 
            // txtTransX
            // 
            this.txtTransX.Location = new System.Drawing.Point(410, 467);
            this.txtTransX.Name = "txtTransX";
            this.txtTransX.Size = new System.Drawing.Size(31, 20);
            this.txtTransX.TabIndex = 6;
            this.txtTransX.TextChanged += new System.EventHandler(this.txtTransX_TextChanged);
            // 
            // lblRotationDeg
            // 
            this.lblRotationDeg.AutoSize = true;
            this.lblRotationDeg.Location = new System.Drawing.Point(12, 470);
            this.lblRotationDeg.Name = "lblRotationDeg";
            this.lblRotationDeg.Size = new System.Drawing.Size(47, 13);
            this.lblRotationDeg.TabIndex = 7;
            this.lblRotationDeg.Text = "Degrees";
            // 
            // lblRotationX
            // 
            this.lblRotationX.AutoSize = true;
            this.lblRotationX.Location = new System.Drawing.Point(12, 505);
            this.lblRotationX.Name = "lblRotationX";
            this.lblRotationX.Size = new System.Drawing.Size(52, 13);
            this.lblRotationX.TabIndex = 8;
            this.lblRotationX.Text = "Origin X%";
            // 
            // lblRotationY
            // 
            this.lblRotationY.AutoSize = true;
            this.lblRotationY.Location = new System.Drawing.Point(12, 540);
            this.lblRotationY.Name = "lblRotationY";
            this.lblRotationY.Size = new System.Drawing.Size(52, 13);
            this.lblRotationY.TabIndex = 9;
            this.lblRotationY.Text = "Origin Y%";
            // 
            // lblTransY
            // 
            this.lblTransY.AutoSize = true;
            this.lblTransY.Location = new System.Drawing.Point(235, 512);
            this.lblTransY.Name = "lblTransY";
            this.lblTransY.Size = new System.Drawing.Size(20, 13);
            this.lblTransY.TabIndex = 10;
            this.lblTransY.Text = "Dy";
            // 
            // lblTransX
            // 
            this.lblTransX.AutoSize = true;
            this.lblTransX.Location = new System.Drawing.Point(235, 474);
            this.lblTransX.Name = "lblTransX";
            this.lblTransX.Size = new System.Drawing.Size(20, 13);
            this.lblTransX.TabIndex = 11;
            this.lblTransX.Text = "Dx";
            // 
            // lblScaleX
            // 
            this.lblScaleX.AutoSize = true;
            this.lblScaleX.Location = new System.Drawing.Point(472, 470);
            this.lblScaleX.Name = "lblScaleX";
            this.lblScaleX.Size = new System.Drawing.Size(22, 13);
            this.lblScaleX.TabIndex = 12;
            this.lblScaleX.Text = "X%";
            // 
            // lblScaleY
            // 
            this.lblScaleY.AutoSize = true;
            this.lblScaleY.Location = new System.Drawing.Point(472, 505);
            this.lblScaleY.Name = "lblScaleY";
            this.lblScaleY.Size = new System.Drawing.Size(22, 13);
            this.lblScaleY.TabIndex = 13;
            this.lblScaleY.Text = "Y%";
            // 
            // trkRotDeg
            // 
            this.trkRotDeg.Location = new System.Drawing.Point(63, 467);
            this.trkRotDeg.Maximum = 90;
            this.trkRotDeg.Minimum = -90;
            this.trkRotDeg.Name = "trkRotDeg";
            this.trkRotDeg.Size = new System.Drawing.Size(104, 45);
            this.trkRotDeg.TabIndex = 14;
            this.trkRotDeg.Scroll += new System.EventHandler(this.trkRotDeg_Scroll);
            // 
            // trkRotX
            // 
            this.trkRotX.Location = new System.Drawing.Point(63, 502);
            this.trkRotX.Maximum = 100;
            this.trkRotX.Name = "trkRotX";
            this.trkRotX.Size = new System.Drawing.Size(104, 45);
            this.trkRotX.TabIndex = 15;
            this.trkRotX.Value = 50;
            this.trkRotX.Scroll += new System.EventHandler(this.trkRotX_Scroll);
            // 
            // trkRotY
            // 
            this.trkRotY.Location = new System.Drawing.Point(63, 537);
            this.trkRotY.Maximum = 100;
            this.trkRotY.Name = "trkRotY";
            this.trkRotY.Size = new System.Drawing.Size(104, 45);
            this.trkRotY.TabIndex = 16;
            this.trkRotY.Value = 50;
            this.trkRotY.Scroll += new System.EventHandler(this.trkRotY_Scroll);
            // 
            // trkTransX
            // 
            this.trkTransX.Location = new System.Drawing.Point(261, 467);
            this.trkTransX.Maximum = 300;
            this.trkTransX.Minimum = -300;
            this.trkTransX.Name = "trkTransX";
            this.trkTransX.Size = new System.Drawing.Size(134, 45);
            this.trkTransX.TabIndex = 17;
            this.trkTransX.Scroll += new System.EventHandler(this.trkTransX_Scroll);
            // 
            // trkTransY
            // 
            this.trkTransY.Location = new System.Drawing.Point(261, 505);
            this.trkTransY.Maximum = 300;
            this.trkTransY.Minimum = -300;
            this.trkTransY.Name = "trkTransY";
            this.trkTransY.Size = new System.Drawing.Size(134, 45);
            this.trkTransY.TabIndex = 18;
            this.trkTransY.Scroll += new System.EventHandler(this.trkTransY_Scroll);
            // 
            // trkScaleX
            // 
            this.trkScaleX.Location = new System.Drawing.Point(500, 467);
            this.trkScaleX.Maximum = 200;
            this.trkScaleX.Name = "trkScaleX";
            this.trkScaleX.Size = new System.Drawing.Size(131, 45);
            this.trkScaleX.TabIndex = 19;
            this.trkScaleX.Value = 100;
            this.trkScaleX.Scroll += new System.EventHandler(this.trkScaleX_Scroll);
            // 
            // trkScaleY
            // 
            this.trkScaleY.Location = new System.Drawing.Point(500, 498);
            this.trkScaleY.Maximum = 200;
            this.trkScaleY.Name = "trkScaleY";
            this.trkScaleY.Size = new System.Drawing.Size(131, 45);
            this.trkScaleY.TabIndex = 20;
            this.trkScaleY.Value = 100;
            this.trkScaleY.Scroll += new System.EventHandler(this.trkScaleY_Scroll);
            // 
            // chkRotation
            // 
            this.chkRotation.AutoSize = true;
            this.chkRotation.Location = new System.Drawing.Point(15, 433);
            this.chkRotation.Name = "chkRotation";
            this.chkRotation.Size = new System.Drawing.Size(66, 17);
            this.chkRotation.TabIndex = 21;
            this.chkRotation.Text = "Rotation";
            this.chkRotation.UseVisualStyleBackColor = true;
            this.chkRotation.CheckedChanged += new System.EventHandler(this.chkRotation_CheckedChanged);
            // 
            // chkTranslation
            // 
            this.chkTranslation.AutoSize = true;
            this.chkTranslation.Location = new System.Drawing.Point(238, 433);
            this.chkTranslation.Name = "chkTranslation";
            this.chkTranslation.Size = new System.Drawing.Size(78, 17);
            this.chkTranslation.TabIndex = 22;
            this.chkTranslation.Text = "Translation";
            this.chkTranslation.UseVisualStyleBackColor = true;
            this.chkTranslation.CheckedChanged += new System.EventHandler(this.chkTranslation_CheckedChanged);
            // 
            // chkScale
            // 
            this.chkScale.AutoSize = true;
            this.chkScale.Location = new System.Drawing.Point(475, 433);
            this.chkScale.Name = "chkScale";
            this.chkScale.Size = new System.Drawing.Size(53, 17);
            this.chkScale.TabIndex = 23;
            this.chkScale.Text = "Scale";
            this.chkScale.UseVisualStyleBackColor = true;
            this.chkScale.CheckedChanged += new System.EventHandler(this.chkScale_CheckedChanged);
            // 
            // btnPhoto
            // 
            this.btnPhoto.Location = new System.Drawing.Point(15, 76);
            this.btnPhoto.Name = "btnPhoto";
            this.btnPhoto.Size = new System.Drawing.Size(75, 23);
            this.btnPhoto.TabIndex = 24;
            this.btnPhoto.Text = "Load Photo";
            this.btnPhoto.UseVisualStyleBackColor = true;
            this.btnPhoto.Click += new System.EventHandler(this.btnPhoto_Click);
            // 
            // btnAccessory
            // 
            this.btnAccessory.Location = new System.Drawing.Point(15, 116);
            this.btnAccessory.Name = "btnAccessory";
            this.btnAccessory.Size = new System.Drawing.Size(75, 37);
            this.btnAccessory.TabIndex = 25;
            this.btnAccessory.Text = "Load Accessory";
            this.btnAccessory.UseVisualStyleBackColor = true;
            this.btnAccessory.Click += new System.EventHandler(this.btnAccessory_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(15, 197);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 26;
            this.btnUp.Text = "Move Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(15, 301);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 27;
            this.btnDown.Text = "Move Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lstDisplay
            // 
            this.lstDisplay.FormattingEnabled = true;
            this.lstDisplay.Location = new System.Drawing.Point(15, 226);
            this.lstDisplay.Name = "lstDisplay";
            this.lstDisplay.Size = new System.Drawing.Size(75, 69);
            this.lstDisplay.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 584);
            this.Controls.Add(this.lstDisplay);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnAccessory);
            this.Controls.Add(this.btnPhoto);
            this.Controls.Add(this.chkScale);
            this.Controls.Add(this.chkTranslation);
            this.Controls.Add(this.chkRotation);
            this.Controls.Add(this.trkScaleY);
            this.Controls.Add(this.trkScaleX);
            this.Controls.Add(this.trkTransY);
            this.Controls.Add(this.trkTransX);
            this.Controls.Add(this.trkRotY);
            this.Controls.Add(this.trkRotX);
            this.Controls.Add(this.trkRotDeg);
            this.Controls.Add(this.lblScaleY);
            this.Controls.Add(this.lblScaleX);
            this.Controls.Add(this.lblTransX);
            this.Controls.Add(this.lblTransY);
            this.Controls.Add(this.lblRotationY);
            this.Controls.Add(this.lblRotationX);
            this.Controls.Add(this.lblRotationDeg);
            this.Controls.Add(this.txtTransX);
            this.Controls.Add(this.txtScaleY);
            this.Controls.Add(this.txtTransY);
            this.Controls.Add(this.txtRotY);
            this.Controls.Add(this.txtRotX);
            this.Controls.Add(this.txtScaleX);
            this.Controls.Add(this.txtRotDeg);
            this.Name = "Form1";
            this.Text = "Bartek\'s Photo Fun";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.trkRotDeg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkScaleY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRotDeg;
        private System.Windows.Forms.TextBox txtScaleX;
        private System.Windows.Forms.TextBox txtRotX;
        private System.Windows.Forms.TextBox txtRotY;
        private System.Windows.Forms.TextBox txtTransY;
        private System.Windows.Forms.TextBox txtScaleY;
        private System.Windows.Forms.TextBox txtTransX;
        private System.Windows.Forms.Label lblRotationDeg;
        private System.Windows.Forms.Label lblRotationX;
        private System.Windows.Forms.Label lblRotationY;
        private System.Windows.Forms.Label lblTransY;
        private System.Windows.Forms.Label lblTransX;
        private System.Windows.Forms.Label lblScaleX;
        private System.Windows.Forms.Label lblScaleY;
        private System.Windows.Forms.TrackBar trkRotDeg;
        private System.Windows.Forms.TrackBar trkRotX;
        private System.Windows.Forms.TrackBar trkRotY;
        private System.Windows.Forms.TrackBar trkTransX;
        private System.Windows.Forms.TrackBar trkTransY;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TrackBar trkScaleX;
        private System.Windows.Forms.TrackBar trkScaleY;
        private System.Windows.Forms.CheckBox chkRotation;
        private System.Windows.Forms.CheckBox chkTranslation;
        private System.Windows.Forms.CheckBox chkScale;
        private System.Windows.Forms.Button btnPhoto;
        private System.Windows.Forms.Button btnAccessory;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ListBox lstDisplay;
    }
}

