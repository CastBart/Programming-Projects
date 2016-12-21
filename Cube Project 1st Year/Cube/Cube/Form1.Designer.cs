namespace Cube
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
            this.btnXMinus = new System.Windows.Forms.Button();
            this.btnXPlus = new System.Windows.Forms.Button();
            this.btnYMinus = new System.Windows.Forms.Button();
            this.btnYPlus = new System.Windows.Forms.Button();
            this.btnZMinus = new System.Windows.Forms.Button();
            this.btnZPlus = new System.Windows.Forms.Button();
            this.btnDecrease = new System.Windows.Forms.Button();
            this.btnIncrease1 = new System.Windows.Forms.Button();
            this.chkMatrix = new System.Windows.Forms.CheckBox();
            this.chkQuaternion = new System.Windows.Forms.CheckBox();
            this.prbProgression = new System.Windows.Forms.ProgressBar();
            this.btnRandom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnXMinus
            // 
            this.btnXMinus.Location = new System.Drawing.Point(433, 38);
            this.btnXMinus.Name = "btnXMinus";
            this.btnXMinus.Size = new System.Drawing.Size(37, 24);
            this.btnXMinus.TabIndex = 0;
            this.btnXMinus.Text = "X-";
            this.btnXMinus.UseVisualStyleBackColor = true;
            this.btnXMinus.Click += new System.EventHandler(this.btnXMinus_Click);
            // 
            // btnXPlus
            // 
            this.btnXPlus.Location = new System.Drawing.Point(490, 38);
            this.btnXPlus.Name = "btnXPlus";
            this.btnXPlus.Size = new System.Drawing.Size(37, 24);
            this.btnXPlus.TabIndex = 1;
            this.btnXPlus.Text = "X+";
            this.btnXPlus.UseVisualStyleBackColor = true;
            this.btnXPlus.Click += new System.EventHandler(this.btnXPlus_Click);
            // 
            // btnYMinus
            // 
            this.btnYMinus.Location = new System.Drawing.Point(433, 68);
            this.btnYMinus.Name = "btnYMinus";
            this.btnYMinus.Size = new System.Drawing.Size(37, 24);
            this.btnYMinus.TabIndex = 2;
            this.btnYMinus.Text = "Y-";
            this.btnYMinus.UseVisualStyleBackColor = true;
            this.btnYMinus.Click += new System.EventHandler(this.btnYMinus_Click);
            // 
            // btnYPlus
            // 
            this.btnYPlus.Location = new System.Drawing.Point(490, 68);
            this.btnYPlus.Name = "btnYPlus";
            this.btnYPlus.Size = new System.Drawing.Size(37, 24);
            this.btnYPlus.TabIndex = 3;
            this.btnYPlus.Text = "Y+";
            this.btnYPlus.UseVisualStyleBackColor = true;
            this.btnYPlus.Click += new System.EventHandler(this.btnYPlus_Click);
            // 
            // btnZMinus
            // 
            this.btnZMinus.Location = new System.Drawing.Point(433, 98);
            this.btnZMinus.Name = "btnZMinus";
            this.btnZMinus.Size = new System.Drawing.Size(37, 24);
            this.btnZMinus.TabIndex = 4;
            this.btnZMinus.Text = "Z-";
            this.btnZMinus.UseVisualStyleBackColor = true;
            this.btnZMinus.Click += new System.EventHandler(this.btnZMinus_Click);
            // 
            // btnZPlus
            // 
            this.btnZPlus.Location = new System.Drawing.Point(490, 98);
            this.btnZPlus.Name = "btnZPlus";
            this.btnZPlus.Size = new System.Drawing.Size(37, 24);
            this.btnZPlus.TabIndex = 5;
            this.btnZPlus.Text = "Z+";
            this.btnZPlus.UseVisualStyleBackColor = true;
            this.btnZPlus.Click += new System.EventHandler(this.btnZPlus_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Location = new System.Drawing.Point(433, 128);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(37, 24);
            this.btnDecrease.TabIndex = 6;
            this.btnDecrease.Text = "-";
            this.btnDecrease.UseVisualStyleBackColor = true;
            // 
            // btnIncrease1
            // 
            this.btnIncrease1.Location = new System.Drawing.Point(490, 128);
            this.btnIncrease1.Name = "btnIncrease1";
            this.btnIncrease1.Size = new System.Drawing.Size(37, 24);
            this.btnIncrease1.TabIndex = 7;
            this.btnIncrease1.Text = "+";
            this.btnIncrease1.UseVisualStyleBackColor = true;
            // 
            // chkMatrix
            // 
            this.chkMatrix.AutoSize = true;
            this.chkMatrix.Location = new System.Drawing.Point(334, 13);
            this.chkMatrix.Name = "chkMatrix";
            this.chkMatrix.Size = new System.Drawing.Size(54, 17);
            this.chkMatrix.TabIndex = 8;
            this.chkMatrix.Text = "Matrix";
            this.chkMatrix.UseVisualStyleBackColor = true;
            // 
            // chkQuaternion
            // 
            this.chkQuaternion.AutoSize = true;
            this.chkQuaternion.Location = new System.Drawing.Point(567, 13);
            this.chkQuaternion.Name = "chkQuaternion";
            this.chkQuaternion.Size = new System.Drawing.Size(78, 17);
            this.chkQuaternion.TabIndex = 9;
            this.chkQuaternion.Text = "Quaternion";
            this.chkQuaternion.UseVisualStyleBackColor = true;
            // 
            // prbProgression
            // 
            this.prbProgression.Location = new System.Drawing.Point(433, 318);
            this.prbProgression.Name = "prbProgression";
            this.prbProgression.Size = new System.Drawing.Size(100, 23);
            this.prbProgression.TabIndex = 10;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(443, 158);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 11;
            this.btnRandom.Text = "Random!";
            this.btnRandom.UseCompatibleTextRendering = true;
            this.btnRandom.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 438);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.prbProgression);
            this.Controls.Add(this.chkQuaternion);
            this.Controls.Add(this.chkMatrix);
            this.Controls.Add(this.btnIncrease1);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.btnZPlus);
            this.Controls.Add(this.btnZMinus);
            this.Controls.Add(this.btnYPlus);
            this.Controls.Add(this.btnYMinus);
            this.Controls.Add(this.btnXPlus);
            this.Controls.Add(this.btnXMinus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXMinus;
        private System.Windows.Forms.Button btnXPlus;
        private System.Windows.Forms.Button btnYMinus;
        private System.Windows.Forms.Button btnYPlus;
        private System.Windows.Forms.Button btnZMinus;
        private System.Windows.Forms.Button btnZPlus;
        private System.Windows.Forms.Button btnDecrease;
        private System.Windows.Forms.Button btnIncrease1;
        private System.Windows.Forms.CheckBox chkMatrix;
        private System.Windows.Forms.CheckBox chkQuaternion;
        private System.Windows.Forms.ProgressBar prbProgression;
        private System.Windows.Forms.Button btnRandom;
    }
}

