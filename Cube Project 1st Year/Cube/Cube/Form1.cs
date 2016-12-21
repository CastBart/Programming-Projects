using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuaternionCalculator;
using Lab6Starter;


namespace Cube
{
    /* Bartosz Zych 
     * C00205464
     * Starting time 14.03.16
     * Working Time 
     * 14.03.16      17:50 -- 19:00
     *               1 hour 10 min
     * 04.04.16      17:20 -- 19:00 
     *               1 hour 40 min
     * 
     * Total working time -- 02:50 min
     */
    public partial class Form1 : Form
    {
        Vector3[] cubeOnePoints, cubeTwoPoints; // array of vectors which contain points
        bool[] vertexsOne, vertexsTwo;
        const int MaxPointArray = 8;
        const int MaxVectorArray = 8;
        const int MaxVertex = 8;
        Vector3 move1 = new Vector3(200, 250, 0);
        Vector3 move2 = new Vector3(625, 200, 0);
        Font drawFont = new Font("Arial", 8);
        Pen black = new Pen(Color.Black, 2f);
        float angleOfRotation = 5f;
        int sizex = 100;
        int sizey = 100;
        int sizez = 100;
        readonly Vector3 MatrixToCentre = new Vector3(100,100,100);
        //const Vector3 QuatrernionToCentre;

        Matrix3 matrix;
        Quaternion quaternion;

        public enum Axis { X, Y, Z };

        public Form1()
        {
            InitializeComponent();
            cubeOnePoints = new Vector3[MaxVectorArray];
            cubeTwoPoints = new Vector3[MaxVectorArray];
            vertexsOne = new bool[MaxVertex];
            vertexsTwo = new bool[MaxVertex];
            
            DeclareCube();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Point[] drawingPointsOne = new Point[MaxPointArray];
            Point[] drawingPointsTwo = new Point[MaxPointArray];
            for (int i = 0; i < drawingPointsOne.Length; i++)
            {
                drawingPointsOne[i] = (cubeOnePoints[i] + move1).ToPoint();
                drawingPointsTwo[i] = (cubeTwoPoints[i] + move2).ToPoint();
            }

            #region FirstCubeFaces
            //faces
            Point[] topFaceOne = new Point[4];
            Point[] bottomFaceOne = new Point[4];
            Point[] frontLeftFaceOne = new Point[4];
            Point[] frontRightFaceOne = new Point[4];
            Point[] backLeftFaceOne = new Point[4];
            Point[] backRightFaceOne = new Point[4];
            #endregion

            #region SecondCubeFaces
            //faces
            Point[] topFaceTwo = new Point[4];
            Point[] bottomFaceTwo = new Point[4];
            Point[] frontLeftFaceTwo = new Point[4];
            Point[] frontRightFaceTwo = new Point[4];
            Point[] backLeftFaceTwo = new Point[4];
            Point[] backRightFaceTwo = new Point[4];
            #endregion

            #region Assigning Faces One
            //assigning the top face
            AssignFace(topFaceOne, drawingPointsOne[0], drawingPointsOne[1], drawingPointsOne[2], drawingPointsOne[3]);
            //assigning the bottom face
            AssignFace(bottomFaceOne, drawingPointsOne[4], drawingPointsOne[5], drawingPointsOne[6], drawingPointsOne[7]);
            //assigning the front left face
            AssignFace(frontLeftFaceOne, drawingPointsOne[0], drawingPointsOne[3], drawingPointsOne[4], drawingPointsOne[7]);
            //assigning the front right face
            AssignFace(frontRightFaceOne, drawingPointsOne[3], drawingPointsOne[2], drawingPointsOne[5], drawingPointsOne[4]);
            //assigning the back left face
            AssignFace(backLeftFaceOne, drawingPointsOne[0], drawingPointsOne[1], drawingPointsOne[6], drawingPointsOne[7]);
            //assigning the back right face
            AssignFace(backRightFaceOne, drawingPointsOne[1], drawingPointsOne[2], drawingPointsOne[5], drawingPointsOne[6]);
            #endregion

            #region Assigning Faces Two
            //assigning the top face
            AssignFace(topFaceTwo, drawingPointsTwo[0], drawingPointsTwo[1], drawingPointsTwo[2], drawingPointsTwo[3]);
            //assigning the bottom face
            AssignFace(bottomFaceTwo, drawingPointsTwo[4], drawingPointsTwo[5], drawingPointsTwo[6], drawingPointsTwo[7]);
            //assigning the front left face
            AssignFace(frontLeftFaceTwo, drawingPointsTwo[0], drawingPointsTwo[3], drawingPointsTwo[4], drawingPointsTwo[7]);
            //assigning the front right face
            AssignFace(frontRightFaceTwo, drawingPointsTwo[3], drawingPointsTwo[2], drawingPointsTwo[5], drawingPointsTwo[4]);
            //assigning the back left face
            AssignFace(backLeftFaceTwo, drawingPointsTwo[0], drawingPointsTwo[1], drawingPointsTwo[6], drawingPointsTwo[7]);
            //assigning the back right face
            AssignFace(backRightFaceTwo, drawingPointsTwo[1], drawingPointsTwo[2], drawingPointsTwo[5], drawingPointsTwo[6]);
            #endregion


            #region Checking Which Face to Draw
            //wather to show top side or not
            if (Vector3.FacingTowardsMe(cubeOnePoints[0], cubeOnePoints[1], cubeOnePoints[3]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Yellow), drawingPointsOne, 0, 1, 2, 3, vertexsOne);
                DrawFace(e.Graphics, black, topFaceOne);
            }
            //front left
            if (Vector3.FacingTowardsMe(cubeOnePoints[7], cubeOnePoints[0], cubeOnePoints[4]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Gray), drawingPointsOne, 0, 3, 4, 7, vertexsOne);
                DrawFace(e.Graphics, black, frontLeftFaceOne);
            }
            //front right
            if (Vector3.FacingTowardsMe(cubeOnePoints[4], cubeOnePoints[3], cubeOnePoints[2]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Blue), drawingPointsOne, 3, 2, 5, 4, vertexsOne);
                DrawFace(e.Graphics, black, frontRightFaceOne);
            }
            //back right
            if (Vector3.FacingTowardsMe(cubeOnePoints[6], cubeOnePoints[5], cubeOnePoints[1]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Red), drawingPointsOne, 1, 2, 5, 6, vertexsOne);
                DrawFace(e.Graphics, black, backRightFaceOne);
            }
            //bottom
            if (Vector3.FacingTowardsMe(cubeOnePoints[4], cubeOnePoints[5], cubeOnePoints[7]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Green), drawingPointsOne, 4, 5, 6, 7, vertexsOne);
                DrawFace(e.Graphics, black, bottomFaceOne);
            }
            //back left
            if (Vector3.FacingTowardsMe(cubeOnePoints[6], cubeOnePoints[1], cubeOnePoints[7]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Orange), drawingPointsOne, 0, 1, 6, 7, vertexsOne);
                DrawFace(e.Graphics, black, backLeftFaceOne);
            }
            #endregion

            #region Checking Which Face to Draw
            //wather to show top side or not
            if (Vector3.FacingTowardsMe(cubeTwoPoints[0], cubeTwoPoints[1], cubeTwoPoints[3]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Yellow), drawingPointsTwo, 0, 1, 2, 3, vertexsTwo);
                DrawFace(e.Graphics, black, topFaceTwo);
            }
            //front left
            if (Vector3.FacingTowardsMe(cubeTwoPoints[7], cubeTwoPoints[0], cubeTwoPoints[4]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Gray), drawingPointsTwo, 0, 3, 4, 7, vertexsTwo);
                DrawFace(e.Graphics, black, frontLeftFaceTwo);
            }
            //front right
            if (Vector3.FacingTowardsMe(cubeTwoPoints[4], cubeTwoPoints[3], cubeTwoPoints[2]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Blue), drawingPointsTwo, 3, 2, 5, 4, vertexsTwo);
                DrawFace(e.Graphics, black, frontRightFaceTwo);
            }
            //back right
            if (Vector3.FacingTowardsMe(cubeTwoPoints[6], cubeTwoPoints[5], cubeTwoPoints[1]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Red), drawingPointsTwo, 1, 2, 5, 6, vertexsTwo);
                DrawFace(e.Graphics, black, backRightFaceTwo);
            }
            //bottom
            if (Vector3.FacingTowardsMe(cubeTwoPoints[4], cubeTwoPoints[5], cubeTwoPoints[7]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Green), drawingPointsTwo, 4, 5, 6, 7, vertexsTwo);
                DrawFace(e.Graphics, black, bottomFaceTwo);
            }
            //back left
            if (Vector3.FacingTowardsMe(cubeTwoPoints[6], cubeTwoPoints[1], cubeTwoPoints[7]))
            {
                ColorIn(e.Graphics, new SolidBrush(Color.Orange), drawingPointsTwo, 0, 1, 6, 7, vertexsTwo);
                DrawFace(e.Graphics, black, backLeftFaceTwo);
            }
            #endregion


            LabelVertex(e.Graphics, drawingPointsOne, vertexsOne);
            LabelVertex(e.Graphics, drawingPointsTwo, vertexsTwo);
        }

        /// <summary>
        /// By taking in e.graphics a brush an array of points and ints
        /// by passing the appropriate ints to the method it will fill 
        /// a polygon that you have desired to fill in.
        /// this method also makes the vertexes of the polygon(face) set to true
        /// so it will display them on each point
        /// </summary>
        /// <param name="paint"></param>
        /// <param name="brush"></param>
        /// <param name="face"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        private void ColorIn(Graphics paint, SolidBrush brush, Point[] face ,int a, int b, int c, int d, bool[] vertex)
        {
            Point[] fill = new Point[4];
            fill[0] = face[a];
            fill[1] = face[b];
            fill[2] = face[c];
            fill[3] = face[d];
            paint.FillPolygon(brush, fill);
            vertex[a] = true;
            vertex[b] = true;
            vertex[c] = true;
            vertex[d] = true;
        }
        /// <summary>
        /// Draws a desired face by passing in the face(point array)
        /// a pen and graphics
        /// </summary>
        /// <param name="paint"></param>
        /// <param name="pen"></param>
        /// <param name="face"></param>
        private void DrawFace(Graphics paint, Pen pen, Point[] face)
        {
            paint.DrawPolygon(pen, face);
        }

        /// <summary>
        /// declaration of cubes position
        /// </summary>
        private void DeclareCube()
        {
            cubeOnePoints[0] = new Vector3(-69.9f, -49f, 14.6f);
            cubeOnePoints[1] = new Vector3(11.6f, -78.2f, -35.4f);
            cubeOnePoints[2] = new Vector3(69.5f, -37.6f, 35.4f);
            cubeOnePoints[3] = new Vector3(-12f, -8.4f, 85.4f);
            cubeOnePoints[4] = new Vector3(-11.6f, 78.2f, 35.4f);
            cubeOnePoints[5] = new Vector3(69.9f, 49f, -14.6f);
            cubeOnePoints[6] = new Vector3(12f, 8.4f, -85.4f);
            cubeOnePoints[7] = new Vector3(-69.5f, 37.6f, -35.4f);

            cubeTwoPoints[0] = new Vector3(0, 0, 0);
            cubeTwoPoints[1] = new Vector3(0, 0, -sizez);
            cubeTwoPoints[2] = new Vector3(sizex, 0, -sizez);
            cubeTwoPoints[3] = new Vector3(sizex, 0, 0);
            cubeTwoPoints[4] = new Vector3(sizex, sizey, 0);
            cubeTwoPoints[5] = new Vector3(sizex, sizey, -sizez);
            cubeTwoPoints[6] = new Vector3(0, sizey, -sizez);
            cubeTwoPoints[7] = new Vector3(0, sizey, 0f);
        }

        /// <summary>
        /// this method lebels each vertex if the showvertex is true
        /// it also creates a cirlce around the point and is numbered 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="point"></param>
        private void LabelVertex(Graphics graphics, Point[] point, bool[] vertexes)
        {
            for (int i = 0; i < vertexsOne.Length; i++)
            {
                point[i].X -= 6;
                point[i].Y -= 6;
                if (vertexes[i])
                {
                    graphics.DrawEllipse(black, (int)point[i].X, (int)point[i].Y, 12f, 12f);
                    graphics.FillEllipse(new SolidBrush(Color.Black), (int)point[i].X, (int)point[i].Y, 12f, 12f);
                    graphics.DrawString((i + 1).ToString(), drawFont, new SolidBrush(Color.White), point[i]);
                }
                

               
               
            }
        }

        /// <summary>
        /// assaigns desired points to a face
        /// by passing the face array and the
        /// points you want to be assigned to
        /// </summary>
        /// <param name="face"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        private void AssignFace(Point[] face, Point a, Point b, Point c, Point d)
        {
            face[0] = a;
            face[1] = b;
            face[2] = c;
            face[3] = d;
        }

        private void RotateUsingMatrix()
        {

        }

        private void btnXMinus_Click(object sender, EventArgs e)
        {
            if (chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(-angleOfRotation, Axis.Z);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                    
                }
            }
            if (chkQuaternion.Checked)
            {

                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f, 1f, 0f, 0f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], -angleOfRotation);
                }
            }
            this.Refresh();
        }

        private void btnXPlus_Click(object sender, EventArgs e)
        {
            if(chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(angleOfRotation, Axis.Z);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                }
            }

            if (chkQuaternion.Checked)
            {
                
                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f,1f,0f,0f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], angleOfRotation);
                }
            }
            this.Refresh();

        }

        private void btnYPlus_Click(object sender, EventArgs e)
        {
            if (chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(angleOfRotation, Axis.Y);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                }
            }

            if (chkQuaternion.Checked)
            {
                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f, 0f, 1f, 0f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], angleOfRotation);
                }
            }

            this.Refresh();
        }

        private void btnYMinus_Click(object sender, EventArgs e)
        {
            if (chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(-angleOfRotation, Axis.Y);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                }
            }

            if (chkQuaternion.Checked)
            {
                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f, 0f, 1f, 0f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], -angleOfRotation);
                }
            }

            this.Refresh();
        }

        private void btnZPlus_Click(object sender, EventArgs e)
        {
            if (chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(angleOfRotation, Axis.X);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                }
            }

            if (chkQuaternion.Checked)
            {
                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f, 0f, 0f, 1f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], angleOfRotation);
                }
            }

            this.Refresh();
        }

        private void btnZMinus_Click(object sender, EventArgs e)
        {
            if (chkMatrix.Checked)
            {
                matrix = Matrix3.Rotation(-angleOfRotation, Axis.X);
                for (int i = 0; i < cubeOnePoints.Length; i++)
                {
                    cubeOnePoints[i] = matrix * cubeOnePoints[i];
                }
            }

            if (chkQuaternion.Checked)
            {
                for (int i = 0; i < cubeTwoPoints.Length; i++)
                {
                    quaternion = new Quaternion(0f, 0f, 0f, 1f);
                    cubeTwoPoints[i] = quaternion.Rotate(cubeTwoPoints[i], -angleOfRotation);
                }
            }

            this.Refresh();
        }
    }
}
