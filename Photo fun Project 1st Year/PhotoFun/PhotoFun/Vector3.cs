using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab6Starter
{
    /* Bartosz Zych
     * C00205464
     * started on Saturday 14/11/2015 worked for 1h 32min
     * worked on Thursday 26/11/2015  worked for 45 min
     * worked on Monday 30/11/2015 worked for 2 hours
     * total time worked on this project: 4 hours and 17min
     * i have used my maths notes to get the formulas 
     */
    class Vector3
    {
        private float x;
        private float y;
        private float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// default constructor, makes null vector
        /// </summary>
        public Vector3()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        /// <summary>
        /// Constructor  taking values for x, y and z
        /// </summary
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        public Vector3(float x1, float y1, float z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }
        /// <summary>
        /// error message will pop up if either of x,y,z are not numbers
        /// </summary>
        /// <param name="textX"></param>
        /// <param name="textY"></param>
        /// <param name="textZ"></param>
        public Vector3(string textX, string textY, string textZ)
        {
            if (!float.TryParse(textX, out x))
            {
                MessageBox.Show("Bad Number X", "Input Error", MessageBoxButtons.OK);
                x = 0;
                y = 0;
                z = 0;
                return;
            }
            if (!float.TryParse(textY, out y))
            {
                MessageBox.Show("Bad Number Y", "Input Error", MessageBoxButtons.OK);
                x = 0;
                y = 0;
                z = 0;
                return;
            }
            if (!float.TryParse(textZ, out z))
            {
                MessageBox.Show("Bad Number Z", "Input Error", MessageBoxButtons.OK);
                x = 0;
                y = 0;
                z = 0;
                return;
            }

        }
        /// <summary>
        /// constructor taking a vector as the source
        /// </summary>
        /// <param name="v"></param>
        public Vector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        /// <summary>
        /// override for ToString method outputting brackets, comas and formatting the
        /// values as general 3 digits
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x.ToString("g3") + "," + y.ToString("g3") + "," + z.ToString("g3") + ")";
        }

        public double Length()
        {
            double length;
            length = (Math.Sqrt((x * x) + (y * y) + (z * z)));
            return length;
        }

        public bool Equals(Vector3 v2)
        {
            if (x == v2.x && y == v2.y && z == v2.z)
                return true;
            else return false;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Vector3 return false.
            Vector3 v2 = obj as Vector3;
            if ((System.Object)v2 == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == v2.x && y == v2.y && z == v2.z);
        }

        public override int GetHashCode()
        {
            return (int)(x * y * z);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(v1, v2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)v1 == null) || ((object)v2 == null))
            {
                return false;
            }
            return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }
        /// <summary>
        /// this method is used to calculat dot product
        /// it does so by multiplying x's, y's and z's of both vectors and adding them together
        /// making a new vector v2 and passing the values from Vector B to v2
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public float DotProduct(Vector3 v2)
        {
            return ((x * v2.x) + (y * v2.y) +(z * v2.z));
        }

       
        /// <summary>
        /// vector B's values are assignted to v2 
        /// v3 is created to calculate x, y and z
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 CrossProduct(Vector3 v2)
        {
            Vector3 v3 = new Vector3();
            v3.x = ((y)*(v2.z)-(z)*(v2.y));
            v3.y = ((z)*(v2.x)-(x)*(v2.z));
            v3.z = ((x)*(v2.y)-(y)*(v2.x));
            
            return v3;

        }
       

        /// <summary>
        /// here i am dividing dot product by (length vector1 * length vector2)
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double AngleBetween(Vector3 v2)
        {
            double lengthVec2;
            lengthVec2 = (Math.Sqrt((v2.x * v2.x) + (v2.y * v2.y) + (v2.z * v2.z)));
            double angle;
            double firstCalc;
            firstCalc= DotProduct(v2);
            
            double secondCalc;
            secondCalc = Length() * lengthVec2;

            angle = (Math.Acos(firstCalc / secondCalc) * 180 / Math.PI);
           
            return angle;
        }
        /// <summary>
        /// creating vector v3. dividing the x, y and z by the length of the marked vector.
        /// returing the v3
        /// </summary>
        /// <returns></returns>
        public Vector3 Unit()
        {
            Vector3 v3 = new Vector3();
            float length = (float)(Math.Sqrt((x * x) + (y * y) + (z * z)));
            
            v3.x =  (float)(x / length);
            
            v3.y = (float)(y / length);
            
            v3.z = (float)(z / length);
            return  v3;
            

            
        }
        /// <summary>
        /// x, y and z are equal to the calculation using x's, y's and z's from the 2 existing vectors (A and B),
        /// dot product and length
        /// returing a new vector v3
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 ParralelComponent(Vector3 v2)
        {
            Vector3 v3 = new Vector3();
            double firstCalc;
            double lenghtVec2;

            lenghtVec2 = Length();

            firstCalc = DotProduct(v2);
            double secondCalc = lenghtVec2 * lenghtVec2;
            if (secondCalc == 0)
            {
                return new Vector3();
            }
            else
            {
                v3.x = (float)(firstCalc / secondCalc) * x;
                v3.y = (float)(firstCalc / secondCalc) * y;
                v3.z = (float)(firstCalc / secondCalc) * z;
                return v3;
            }
        }
        /// <summary>
        /// Alos called normal but the result will not normally be a unit vector
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 PerpendicularComponent(Vector3 v2)
        {
            Vector3 v3 = new Vector3();
            double firstCalc;
            double lenghtVec2;


            lenghtVec2 = Length();

            firstCalc = DotProduct(v2);
            double secondCalc = lenghtVec2 * lenghtVec2;
            if (secondCalc == 0)
            {
                return new Vector3();
            }
            else
            {
                v3.x = v2.x - ((float)(x * firstCalc / secondCalc));
                v3.y = v2.y - ((float)(y * firstCalc / secondCalc));
                v3.z = v2.z - ((float)(z * firstCalc / secondCalc));
                return v3;
            }
        }
        /// <summary>
        /// number in the scale box is returned as a float
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Vector3 Scale(double scale)
        {
            return Scale((float)scale);
        }
        /// <summary>
        /// here the marked vector is scaled by the current number that is in the scaling box
        /// <param name="scale"></param>
        /// <returns></returns>
        public Vector3 Scale(float scale)
        {
            return new Vector3(x * scale, y * scale, z * scale);
        }
        /// <summary>
        /// here we add both vectors together
        /// it will return a new vector by adding two together
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// Subtract second vector from first
        /// return new vector passing difference between components
        /// as values for constructor
        /// </summary>
        /// <param name="v1">first operand</param>
        /// <param name="v2">second operand</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
        /// <summary>
        /// An overloaded operator - to return the negation of a single vector
        /// return a new vector negating each of the components
        /// </summary>
        /// <param name="v1">vector</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(-v1.x, -v1.y, -v1.z);
        }

        public static double operator *(Vector3 V1, Vector3 V2)
        {
            return (V1.x * V2.x + V1.y * V2.y + V1.z * V2.z);
        }
        public static Vector3 operator *(double k, Vector3 V1)
        {
            return new Vector3(V1.x * (float)k, V1.y * (float)k, V1.z * (float)k);
        }
        public static Vector3 operator *(float k, Vector3 V1)
        {
            return new Vector3(V1.x * k, V1.y * k, V1.z * k);
        }

        public static Vector3 operator /(Vector3 V1, float k)
        {
            return new Vector3(V1.x / k, V1.y / k, V1.z / k);
        }


        public static Vector3 operator *(int k, Vector3 V1)
        {
            return new Vector3(V1.x * k, V1.y * k, V1.z * k);
        }
        public static Vector3 operator ^(Vector3 V1, Vector3 V2)
        {
            return new Vector3(V1.y * V2.z - V1.z * V2.y, V1.z * V2.x - V1.x * V2.z, V1.x * V2.y - V1.y * V2.x);
        }
    }
}
