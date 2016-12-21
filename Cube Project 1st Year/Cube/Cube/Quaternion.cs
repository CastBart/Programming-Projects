using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab6Starter;

namespace QuaternionCalculator
{
/* Bartosz Zych
 * C00205464
 * Started on
 * 5.3.2016 at 19:00 -- 22:00
 *             3 hours
 * 7.3.2016 at 17:00 -- 19:00
 *             2 hours
 * 9.3.2016 at 9:00 -- 11:00
 *             2 hours
 * Total time: 7 hours
 * 
 * Known Bugs: Rotation Does not Work
 * 
 */
    class Quaternion
    {

        
        private float x;
        private float y;
        private float z;
        private float w;

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        public float W
        {
            get { return w; }
            set { w = value; }
        }
        


        
        /// <summary>
        /// default constructor
        /// </summary>
        public Quaternion()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }
        /// <summary>
        /// constructor wich sets all the values of the Quaternion
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        /// <param name="newW"></param>
        public Quaternion(float newW, float newX, float newY, float newZ)
        {
            w = newW;
            x = newX;
            y = newY;
            z = newZ;
        }

        public Quaternion(Vector3 v)
        {
            w = 0;
            x = v.X;
            y = v.Y;
            z = v.Z;
        }

        


        /// <summary>
        /// + operator adding 2 quaternion 
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        /// <returns></returns>
        public static Quaternion operator +(Quaternion leftSide, Quaternion rightSide)
        {
            return new Quaternion(
                leftSide.W + rightSide.W,
                leftSide.X + rightSide.X,
                leftSide.Y + rightSide.Y,
                leftSide.Z + rightSide.Z
                );
        }
        /// <summary>
        /// - operator subtracting 2 quaternion 
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        /// <returns></returns>
        public static Quaternion operator -(Quaternion leftSide, Quaternion rightSide)
        {
            return new Quaternion(
                leftSide.W - rightSide.W,
                leftSide.X - rightSide.X,
                leftSide.Y - rightSide.Y,
                leftSide.Z - rightSide.Z
                );
        }
        /// <summary>
        /// - operator used to negate a quaternion
        /// </summary>
        /// <param name="q1"></param>
        /// <returns></returns>
        public static Quaternion operator -(Quaternion q1)
        {
            return new Quaternion(-q1.w,-q1.x,-q1.y,-q1.z);
        }

        /// <summary>
        /// multiplication of 2 quaternions
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion leftSide, Quaternion rightSide)
        {
            return new Quaternion(
                (leftSide.W * rightSide.W) - (leftSide.X * rightSide.X) - (leftSide.Y * rightSide.Y) - (leftSide.Z * rightSide.Z),
                (leftSide.W * rightSide.X) + (leftSide.X * rightSide.W) + (leftSide.Y * rightSide.Z) - (leftSide.Z * rightSide.Y),
                (leftSide.W * rightSide.Y) + (leftSide.Y * rightSide.W) + (leftSide.Z * rightSide.X) - (leftSide.X * rightSide.Z),
                (leftSide.W * rightSide.Z) + (leftSide.Z * rightSide.W) + (leftSide.X * rightSide.Y) - (leftSide.Y * rightSide.X)
               );
        }
        /// <summary>
        /// scalar by a quaternion given that the scalar is a float
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion leftSide, float s)
        {
            return new Quaternion(
                leftSide.W * s,
                leftSide.X * s,
                leftSide.Y * s,
                leftSide.Z * s
               );
        }
        /// <summary>
        /// scalar by a quaternion given that the scalar is a double
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion leftSide, double s)
        {
            return leftSide * (float)s;
        }
        

        /// <summary>
        /// length of the quaternion 
        /// uses following formula
        /// the square root of each component sqaured:
        /// square root of(w * w + x * x + y * y + z * z )
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(w * w + x * x + y * y + z * z);
        }

        /// <summary>
        /// length squared is used to calculate the inverse of a quaternion
        /// uses following formula
        /// (w * w + x * x + y * y + z * z )
        /// </summary>
        /// <returns></returns>
        public double LengthSquared()
        {
            return (w * w + x * x + y * y + z * z);
        }
        /// <summary>
        /// the conjugate of a quaternion is multioplying each imaginery part by -1
        /// </summary>
        /// <returns></returns>
        public Quaternion Conjugate()
        {
            return new Quaternion(w, -x, -y, -z);
        }

        /// <summary>
        /// inverse of a quaternion uses the following formula
        /// q = quaternion
        /// conjuguate of q * (1/length squared of the quaternion)
        /// </summary>
        /// <returns></returns>
        public Quaternion Inverse()
        {
            return (this.Conjugate()) * (1 / this.LengthSquared());
        }
        /// <summary>
        /// here we need the quaternion to have a length of 1 therefor
        /// we need to multiplay the quaternion by 1/the legth
        /// </summary>
        /// <returns></returns>
        public Quaternion Normalize()
        {
            return this * (1 / this.Length());
        }
        /// <summary>
        /// Converts a Quaternion to a vector
        /// (ignores the constant) ;)
        /// </summary>
        /// <returns></returns>
        public Vector3 ConvertToVector3()
        {
            return new Vector3(this.x, this.y, this.z);
        }
       
        /// <summary>
        /// q = quaternion
        /// r = vector
        /// normalize q*(1/q.legth)
        /// then the following formula is used
        /// q * r * q inverse
        /// then we need to convertn the aswer to a vector
        /// </summary>
        /// <returns></returns>
        public Vector3 Rotate(Vector3 thisVector, float angle)
        {
            float angleRads = (float)(angle * Math.PI / 180);
            Quaternion Q1 = this.Normalize();
            Quaternion Q2 = new Quaternion((float)Math.Cos(angleRads / 2), (float)Math.Sin(angleRads / 2) * Q1.x, (float)Math.Sin(angleRads / 2) * Q1.y, (float)Math.Sin(angleRads / 2) * Q1.z);
            Quaternion Q3 = Q2.Conjugate();
            Quaternion Q4 = new Quaternion(thisVector);
            Q4 = Q2 * Q4 * Q3;
            
            return Q4.ConvertToVector3();
        }
     }
}
