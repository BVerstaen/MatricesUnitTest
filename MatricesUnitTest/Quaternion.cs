using System;

namespace Maths_Matrices.Tests
{
    public struct Quaternion
    {
        private const float Deg2Rad = (float)(Math.PI / 180f);

        public float w;
        public float x;
        public float y;
        public float z;

        public MatrixFloat Matrix
        {
            get
            {
                float squareX = (float)Math.Pow(x,2);
                float squareY = (float)Math.Pow(y,2);
                float squareZ = (float)Math.Pow(z,2);
                
                float[,] quaternionMatrix = new float[3,3]
                {
                    { 1 - 2 * squareY - 2 * squareZ, 2*x*y - 2*w*z, 2*x*z + 2*w*y},
                    { this.w, this.x, this.y },
                    { this.w, this.x, this.y },
                };
                
                MatrixFloat m = new MatrixFloat(quaternionMatrix);
                return m;
            }
        }
        
        public Quaternion(float x, float y, float z, float w)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Quaternion Identity => new Quaternion(0, 0, 0, 1);

        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion q = Quaternion.Identity;
            
            float vectorPart = (float)Math.Sin(angle * Deg2Rad / 2);
            
            q.w = (float)Math.Cos(angle * Deg2Rad / 2);
            q.x = axis.x * vectorPart;
            q.y = axis.y * vectorPart;
            q.z = axis.z * vectorPart;
        
            return q;
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            Quaternion q = Quaternion.Identity;
            
            q.w = (q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z);
            q.x = (q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y); 
            q.y = (q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x);
            q.z = (q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w);
            
            return q;
        }
    }
}