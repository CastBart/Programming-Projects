// Written by Peter Lowe
// Jan 2014
// complete the methods and improve the comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab6Starter;

/* Bartosz Zych
 * C00205464
 * Started on
 * 1.2.2016 at 17:00 -- 19:00
 *             2 hours
 * 2.2.2016 at 17:00 -- 18:00
 *             1 hours
 * 3.2.2016 at 17:00 -- 21:00
 *             4 hours
 * Total time: 7 hours
 * 
 * Known Bugs: Every thing works 
 * 
 */
namespace Matrix
{
    class Matrix3
    {// The class has nine variables, 3 rows and 3 columns
        private float m11;
        /// <summary>
        /// first column, first row
        /// </summary>
        public float M11
        {
            get { return m11; }
            set { m11 = value; }
        }
        private float m12;
        /// <summary>
        /// second column, first row
        /// </summary>
        public float M12
        {
            get { return m12; }
            set { m12 = value; }
        }
        private float m13;
        /// <summary>
        /// third column, first row
        /// </summary>
        public float M13
        {
            get { return m13; }
            set { m13 = value; }
        }
        private float m21;
        /// <summary>
        /// first column, second row
        /// </summary>
        public float M21
        {
            get { return m21; }
            set { m21 = value; }
        }
        private float m22;
        /// <summary>
        /// second column, second row
        /// </summary>
        public float M22
        {
            get { return m22; }
            set { m22 = value; }
        }
        private float m23;
        /// <summary>
        /// third column, second row
        /// </summary>
        public float M23
        {
            get { return m23; }
            set { m23 = value; }
        }
        private float m31;
        /// <summary>
        /// first column, third row
        /// </summary>
        public float M31
        {
            get { return m31; }
            set { m31 = value; }
        }
        private float m32;
        /// <summary>
        /// second column, third row
        /// </summary>
        public float M32
        {
            get { return m32; }
            set { m32 = value; }
        }
        private float m33;
        /// <summary>
        /// third column, third row
        /// </summary>
        public float M33
        {
            get { return m33; }
            set { m33 = value; }
        }

        // Constructor 1 create a zero matrix
        public Matrix3()
        {
            m11 = 0.0f;
            m12 = 0.0f;
            m13 = 0.0f;
            m21 = 0.0f;
            m22 = 0.0f;
            m23 = 0.0f;
            m31 = 0.0f;
            m32 = 0.0f;
            m33 = 0.0f;
        }

       /// <summary>
       /// constructor using 3 vectors as rows
       /// </summary>
       /// <param name="Row1"></param>
       /// <param name="Row2"></param>
       /// <param name="Row3"></param>
        public Matrix3(Vector3 Row1, Vector3 Row2, Vector3 Row3)
        {  // To allow 3 rows of vectors to be declared as a matrix
            m11 = Row1.X; m12 = Row1.Y; m13 = Row1.Z;
            m21 = Row2.X; m22 = Row2.Y; m23 = Row2.Z;
            m31 = Row3.X; m32 = Row3.Y; m33 = Row3.Z;
        }
        /// <summary>
        /// a constructor for all number 
        /// of the matrix 
        /// </summary>
        /// <param name="_A11"></param>
        /// <param name="_A12"></param>
        /// <param name="_A13"></param>
        /// <param name="_A21"></param>
        /// <param name="_A22"></param>
        /// <param name="_A23"></param>
        /// <param name="_A31"></param>
        /// <param name="_A32"></param>
        /// <param name="_A33"></param>
        public Matrix3(  float _A11,float _A12,float _A13,
            float _A21, float _A22, float _A23,
            float _A31, float _A32,float _A33)
        {// to allow nine float values 

            m11 = _A11; m12 = _A12; m13 = _A13;
            m21 = _A21; m22 = _A22; m23 = _A23;
            m31 = _A31; m32 = _A32; m33 = _A33;

        }
        /// <summary>
        /// mat
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="V1"></param>
        /// <returns></returns>
        public static Vector3 operator *(Matrix3 M1, Vector3 V1)
        {// An overloaded operator * to return the  product of the matrix by a vector
            
            return new Vector3((M1.M11 * V1.X + M1.M12 * V1.Y + M1.M13 * V1.Z),
                               (M1.M21 * V1.X + M1.M22 * V1.Y + M1.M23 * V1.Z),
                               (M1.M31 * V1.X + M1.M32 * V1.Y + M1.M33 * V1.Z));
        }
        /// <summary>
        /// This method gets the transpose of a matrix
        /// row 1 becomes column 1
        /// row 2 becomes column 2
        /// row 3 becomes column 3
        /// </summary>
        /// <param name="M1"></param>
        /// <returns></returns>
        public static Matrix3 Transpose(Matrix3 M1)
        {// a method to transpose a given matrix

            return new Matrix3(M1.Column(0), M1.Column(1), M1.Column(2));
        }
        /// <summary>
        /// simply adds each row from m1 to each row from m2 
        /// using the Row(i)method below returning the added matrix
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <returns></returns>
        public static Matrix3 operator +(Matrix3 M1, Matrix3 M2)
        {// An overloaded operator + to return the  sum of two matrix 
            return new Matrix3(M1.Row(0) + M2.Row(0), M1.Row(1) + M2.Row(1), M1.Row(2) + M2.Row(2));
        }
        public static Matrix3 operator -(Matrix3 M1, Matrix3 M2)
        {// An overloaded operator * to return the  difference of two matrix
            return new Matrix3(M1.Row(0) - M2.Row(0), M1.Row(1) - M2.Row(1), M1.Row(2) - M2.Row(2));
        }
        /// <summary>
        /// Using the scaler form vector 3 class 
        /// x is multiplyed by each row returning the answer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="M1"></param>
        /// <returns></returns>
        public static Matrix3 operator *(float x, Matrix3 M1)
        {// An overloaded operator * to return the  product of the matrix by a scalar
            Matrix3 answer = new Matrix3(M1.Row(0).Scale(x), M1.Row(1).Scale(x), M1.Row(2).Scale(x));         
            return answer;
        }

        /// <summary>
        /// rotates the matrix by an angle ussing the following formula
        /// [ Cos(Theta), Sin(Theta),   0,]
        /// [-Sin(Theta), Cos(Theta),   0,]
        /// [     0     ,     0     ,   1)]
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Matrix3 Rotation(float angle)
        {
            double d =angle * Math.PI/180 ;
            return new Matrix3((float)Math.Cos(d), (float)Math.Sin(d), 0,
                              (float)-Math.Sin(d), (float)Math.Cos(d), 0,
                                        0, 0, 1);
        }
        /// <summary>
        /// a translate matrix is used to 
        /// move the image from left or right
        /// using the following formula
        /// [1, 0, x,]
        /// [0, 1, y,]
        /// [0, 0, 1 ]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Matrix3 Translation(float x, float y)
        {
            return new Matrix3(1, 0, x,
                               0, 1, y,
                               0, 0, 1);
        }

        /// <summary>
        /// creating a scale matrix which 
        /// is used to increase or decrease an image
        /// using the following formula
        /// [(x / 100 ,    0    ,  0,]
        /// [    0    , y / 100 ,  0,]
        /// [    0    ,    0    ,  1 ]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Matrix3 Scale(float x, float y)
        {
            return new Matrix3(x / 100 ,    0    ,  0,
                                  0    , y / 100 ,  0,
                                  0    ,    0    ,  1);
        } 

        /// <summary>
        /// Using the cross prduct(returns a matrix)
        /// it gets the dot product of
        /// ( ROW 1 of matrix A by COLUMN 1 of matris B, ROW 1 of matrix A by COLUMN 2 of matris B, ROW 1 of matrix A by COLUMN 3 of matris B ) 
        /// ( ROW 2 of matrix A by COLUMN 1 of matris B, ROW 1 of matrix A by COLUMN 2 of matris B, ROW 1 of matrix A by COLUMN 3 of matris B ) 
        /// ( ROW 3 of matrix A by COLUMN 1 of matris B, ROW 1 of matrix A by COLUMN 2 of matris B, ROW 1 of matrix A by COLUMN 3 of matris B )
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <returns></returns>
        public static Matrix3 operator *(Matrix3 M1, Matrix3 M2)
        {// An overloaded operator * to return the  product of two matrix
            Matrix3 answer = new Matrix3(M1.Row(0).DotProduct(M2.Column(0)), M1.Row(0).DotProduct(M2.Column(1)), M1.Row(0).DotProduct(M2.Column(3)),
                                         M1.Row(1).DotProduct(M2.Column(0)), M1.Row(1).DotProduct(M2.Column(1)), M1.Row(1).DotProduct(M2.Column(3)),
                                         M1.Row(2).DotProduct(M2.Column(0)), M1.Row(2).DotProduct(M2.Column(1)), M1.Row(2).DotProduct(M2.Column(3)));
            return answer;
        }

        /// <summary>
        /// using the the following formula im getting the determinant of a matrix
        /// A11(A22 * A33 – A32*A23) – A21(A33*A12 – A32*A13) + A31(A23*A12 – A22*A13)
        /// </summary>
        /// <param name="M1"></param>
        /// <returns></returns>
        public static float Determinant(Matrix3 M1)
        {// method to return the determinant of a 3x3 matrix

            float det;
            det = (M1.M11 * ((M1.M22 * M1.M33) - (M1.M32 * M1.M23))
                 - M1.M21 * ((M1.M33 * M1.M12) - (M1.M32 * M1.M13))
                 + M1.M31 * ((M1.M23 * M1.M12) - (M1.M22 * M1.M13)));
                   
            return det;
        }

        /// <summary>
        /// set each row to the apropriate values 
        /// row1 = m11,m12,m13
        /// row2 = m21,m22,m23
        /// row3 = m31,m32,m33
        /// TIP:::: if a numnber larger than 3 is passed into 
        /// the method then by default it will be the last row
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vector3 Row( int i)
        { 
            // a method to return as Row as vector3 0 == first row, default == last row
            switch (i)
            {
                case 0:
                    return new Vector3(m11, m12, m13);                    
                case 1:
                    return new Vector3(m21, m22, m23);                    
                case 2:
                default:
                    return new Vector3(m31, m32, m33);
                    
            }
        }
        /// <summary>
        /// sets the comluns
        /// colum = m11,m21,m31
        /// column = m12,m22,m32
        /// column = m13,m23,m33
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vector3 Column(int i)
        {// a method to return as column as vector3 0 == first column, default == last column
            switch (i)
            {
                case 0:
                    return new Vector3(m11, m21, m31);                    
                case 1:
                    return new Vector3(m12, m22, m32);                    
                case 2:
                default:
                    return new Vector3(m13, m23, m33);
                    
            }
        }

        /// <summary>
        /// GETS THE INVERSE OF A MATRIX
        /// using following formula:
        ///               {a33.a22 - a32.a23, a32.a13 - a33.a12, a23.a13 - a32.a13}
        /// 1/determinant {a31.a23 - a33.a21, a33.a11 - a31.a13, a21.a11 - a32.a11}
        ///               {a32.a21 - a31.a22, a31.a12 - a32.a11, a22.a12 - a32.a12}
        /// </summary>
        /// <param name="M1"></param>
        /// <returns></returns>
        public static Matrix3 Inverse(Matrix3 M1)
        {
           
            if( Determinant(M1) == 0)
            {
                return new Matrix3();
            }
            else
            {
                 
                return new Matrix3 ( (1/ Determinant(M1)) * ((M1.M33 * M1.M22) - (M1.M32 * M1.M23)), (1/ Determinant(M1))*((M1.M32 * M1.M13) - (M1.M33 * M1.M12)),(1/ Determinant(M1)) * ((M1.M23 * M1.M12) - (M1.M22 * M1.M13)),
                                     (1/ Determinant(M1)) * ((M1.M31 * M1.M23) - (M1.M33 * M1.M21)), (1/ Determinant(M1))*((M1.M33 * M1.M11) - (M1.M31 * M1.M13)),(1/ Determinant(M1)) * ((M1.M21 * M1.M13) - (M1.M23 * M1.M11)),
                                     (1/ Determinant(M1)) * ((M1.M32 * M1.M21) - (M1.M31 * M1.M22)), (1/ Determinant(M1))*((M1.M31 * M1.M12) - (M1.M32 * M1.M11)),(1/ Determinant(M1)) * ((M1.M22 * M1.M11) - (M1.M21 * M1.M12))) ;
            }
            
           
           
            
        }
    }
}
