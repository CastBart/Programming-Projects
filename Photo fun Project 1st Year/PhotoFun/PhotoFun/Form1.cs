using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6Starter;
using Matrix;

namespace PhotoFun
{
    /* Bartosz Zych - C00205464
     * This aplication loads up 2 images. A background image and a transparent image
     * the background image stays in place while you can rotate, translate or resize thye transparent one
     * 
     * Stared date---4.2.2016
     * 
     * Working time----4.2.2016----10.45-11.00
     *-----------------------------1/4 hours
     *-----------------10.2.2016---20:00-21:20
     *-----------------------------1 hrs 20 min
     *-----------------15.2.2016---17:00-19:00
     *-----------------------------2 hours
     *-----------------22.2.2016---9:50-10:00
     *-----------------------------1 hrs 10 min
     *-----------------23.2.2016---13:30-15:00
     *-----------------------------1 hrs 30 min
     *-----------------24.2.2016---9:00-11:00
     *-----------------------------2 hours
     *-----------------------------Total Time  6 hrs 15 min
     */
    public partial class Form1 : Form
    {

        Bitmap accessory, photo;
        Point[] points = new Point[3];
        Point[] photoPoints = new Point[3];

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            accessory = (Bitmap)Bitmap.FromFile(@"Content\baseball.png");
            points[0] = new Point(100, 50);
            points[1] = new Point(accessory.Width + 100, 50);
            points[2] = new Point(100, accessory.Height + 50);

            photo = (Bitmap)Bitmap.FromFile(@"Content\dk.jpg");
            photoPoints[0] = new Point(100, 50);
            photoPoints[1] = new Point(photo.Width + 100, 50);
            photoPoints[2] = new Point(100, photo.Height + 50);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            if (photo != null)
                e.Graphics.DrawImage(photo, photoPoints);
            if (accessory != null)
                e.Graphics.DrawImage(accessory, points);

        }

        private void Update()
        {
            Reset();
            for (int i = 0; i < lstDisplay.Items.Count; i++)
            {
                if (lstDisplay.Items[i] == "Rotation")
                {
                    RotatePoints();
                }
                if (lstDisplay.Items[i] == "Translation")
                {
                    TranslatePoints();
                }
                if (lstDisplay.Items[i] == "Scale")
                {
                    ScalePoints();
                }
            }
            this.Refresh();
        }

        #region CheckBoxes
        /// <summary>
        /// if the check box is checked the message of the check box will 
        /// print to the list box and well be removed when unchecked;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRotation_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxToDisplay(chkRotation);
        }
        /// <summary>
        /// if the check box is checked the message of the check box will 
        /// print to the list box and well be removed when unchecked;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTranslation_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxToDisplay(chkTranslation);
        }
        /// <summary>
        /// if the check box is checked the message of the check box will 
        /// print to the list box and well be removed when unchecked;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkScale_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxToDisplay(chkScale);
        }

        private void CheckBoxToDisplay(CheckBox chkBox)
        {
            if (chkBox.Checked)
                lstDisplay.Items.Add(chkBox.Text);

            else
                lstDisplay.Items.Remove(chkBox.Text);
            this.Refresh();
        }

        #endregion

        #region ScrollBars
        /// <summary>
        /// by passing a txtbox and trkBar it it will print the value
        /// of the track bar to the text box
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="trkBar"></param>
        public void ValueOfTrackBarToTextBox(TextBox txtBox, TrackBar trkBar)
        {
            txtBox.Text = Convert.ToString(trkBar.Value);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkRotDeg_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtRotDeg, trkRotDeg);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkRotX_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtRotX, trkRotX);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkRotY_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtRotY, trkRotY);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkTransX_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtTransX, trkTransX);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkTransY_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtTransY, trkTransY);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkScaleX_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtScaleX, trkScaleX);
        }
        /// <summary>
        /// prints the value fo the track bar to the appropriate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trkScaleY_Scroll(object sender, EventArgs e)
        {
            ValueOfTrackBarToTextBox(txtScaleY, trkScaleY);
        }
        #endregion

        #region Moving Items
        /// <summary>
        /// Moves the highlighted item up by one place untill it reaches the top 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = lstDisplay.SelectedIndex;
            if ((index) >= lstDisplay.Items.Count - 1) return;
            string s = (string)lstDisplay.Items[index];
            lstDisplay.Items.RemoveAt(index);
            lstDisplay.Items.Insert(index + 1, s);
            lstDisplay.SelectedIndex = index + 1;
            Update();
        }
        /// <summary>
        /// Moves the highlighted item up by one place untill it reaches the bottom 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            int index = lstDisplay.SelectedIndex;
            if ((index) <= 0) return;
            string s = (string)lstDisplay.Items[index];
            lstDisplay.Items.RemoveAt(index);
            lstDisplay.Items.Insert(index - 1, s);
            lstDisplay.SelectedIndex = index - 1;
            Update();
        }
        #endregion

        #region TextBoxes
        /// <summary>
        /// assignes the trackBars position according to the value in the textbox
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="trkBar"></param>
        private void MoveTheTrackBarByTextBox(TextBox txtBox, TrackBar trkBar)
        {
            int value = 0;
            if (int.TryParse(txtBox.Text, out value))
            {
                if (value >= trkBar.Minimum && value <= trkBar.Maximum)
                {
                    trkBar.Value = value;
                }
            }
            Update();
        }

        private void txtScaleX_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtScaleX, trkScaleX);
        }

        private void txtScaleY_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtScaleY, trkScaleY);
        }

        private void txtTransX_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtTransX, trkTransX);
        }

        private void txtTransY_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtTransY, trkTransY);
        }

        private void txtRotDeg_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtRotDeg, trkRotDeg);
        }

        private void txtRotX_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtRotX, trkRotX);
        }

        private void txtRotY_TextChanged(object sender, EventArgs e)
        {
            MoveTheTrackBarByTextBox(txtRotY, trkRotY);
        }
        #endregion

        #region Loading Photos
        private void btnPhoto_Click(object sender, EventArgs e)
        {
         
            OpenFileDialog openPhoto = new OpenFileDialog();
            openPhoto.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openPhoto.ShowDialog() == DialogResult.OK)
            {
                photo = new Bitmap(openPhoto.FileName);
                photoPoints[0] = new Point(100, 50);
                photoPoints[1] = new Point(photo.Width + 100, 50);
                photoPoints[2] = new Point(100, photo.Height + 50);
            }
            Update();
        }

        private void btnAccessory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openAccessory = new OpenFileDialog();
            openAccessory.Filter = "Image Files(*.png)|*.png";
            if (openAccessory.ShowDialog() == DialogResult.OK)
            {
                accessory = new Bitmap(openAccessory.FileName);
                points[0] = new Point(100, 50);
                points[1] = new Point(accessory.Width + 100, 50);
                points[2] = new Point(100, accessory.Height + 50);
            }
            Update();
        }
        #endregion
        /// <summary>
        /// reset the point of the image to default
        /// </summary>
        private void Reset()
        {
            points[0] = new Point(100, 50);
            points[1] = new Point(accessory.Width + 100, 50);
            points[2] = new Point(100, accessory.Height + 50);
        }
        /// <summary>
        /// rotates the 3 points of an image by the origin which is 
        /// set by trackbars 
        /// </summary>
        private void RotatePoints()
        {
           
            float x = points[0].X + (accessory.Width * (trkRotX.Value / 100.0f));
            float y = points[0].Y + (accessory.Height * (trkRotY.Value / 100.0f));
            Vector3 centre = new Vector3(x, y, 0);
            Vector3[] accessoryPoints = new Vector3[3];//vectors which contain the points of the accessory image
            Matrix3 rotation = Matrix3.Rotation(trkRotDeg.Value);
            for (int index = 0; index < points.Length; index++)
            {
                accessoryPoints[index] = new Vector3(points[index].X, points[index].Y, 1);
                accessoryPoints[index] -= centre;
                accessoryPoints[index] = rotation * accessoryPoints[index];
                accessoryPoints[index] += centre;
                points[index].X = (int)accessoryPoints[index].X;
                points[index].Y = (int)accessoryPoints[index].Y;
            }
            
        }
        /// <summary>
        /// creates a tranclate matrix passing the trackbar values
        /// and changing the original location of the points
        /// which are contained by an image
        /// </summary>
        private void TranslatePoints()
        {
            Matrix3 translate = Matrix3.Translation(trkTransX.Value, trkTransY.Value);
            for(int index = 0; index < points.Length; index++)
            {
                Vector3 temp = new Vector3(points[index].X, points[index].Y, 1);
                temp = translate * temp;
                points[index].X = (int)temp.X;
                points[index].Y = (int)temp.Y;
            }
        }
        /// <summary>
        /// increases the image by streching out the y or the x 
        /// components of a matrix 
        /// </summary>
        private void ScalePoints()  
        {
            float x = points[0].X + (accessory.Width  / 2f);
            float y = points[0].Y + (accessory.Height / 2f);
            Vector3 centre = new Vector3(x, y, 0);//centre of the image
            Vector3[] pointsOfAcc = new Vector3[3]; //array of three vector which will contain the position of the accessory points
            Matrix3 scale = Matrix3.Scale(trkScaleX.Value, trkScaleY.Value);
            for (int index = 0; index < points.Length; index++)
            {
                pointsOfAcc[index] = new Vector3(points[index].X, points[index].Y, 1);
                pointsOfAcc[index] -= centre;
                pointsOfAcc[index] = scale * pointsOfAcc[index];
                pointsOfAcc[index] += centre;
                points[index].X = (int)pointsOfAcc[index].X;
                points[index].Y = (int)pointsOfAcc[index].Y;
            }
        }
       
    }
}
