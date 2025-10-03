using System;

namespace Maths_Matrices.Tests
{
    public struct Quaternion
    {
        public float w;
        public float x;
        public float y;
        public float z;
        
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
            Vector3 normalizedVector = axis.normalized;
            float vectorScalar = (float)Math.Sin(angle * MathUtils.Deg2Rad / 2);
            
            q.w = (float)Math.Cos(angle * MathUtils.Deg2Rad / 2);
            q.x = normalizedVector.x * vectorScalar;
            q.y = normalizedVector.y * vectorScalar;
            q.z = normalizedVector.z * vectorScalar;
        
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
        
        public Quaternion conjugated => new Quaternion(-x, -y, -z, w);

        
        
        public MatrixFloat Matrix
        {
            get
            {
                float squareX = (float)Math.Pow(x,2);
                float squareY = (float)Math.Pow(y,2);
                float squareZ = (float)Math.Pow(z,2);
                
                float[,] quaternionMatrix = new float[4,4]
                {
                    { 1 - 2 * squareY - 2 * squareZ, 2*x*y - 2*w*z, 2*x*z + 2*w*y, 0},
                    { 2*x*y + 2*w*z, 1 - 2*squareX - 2*squareZ, 2*y*z - 2*w*x, 0},
                    { 2*x*z - 2*w*y, 2*y*z + 2*w*x, 1 - 2*squareX - 2*squareY, 0},
                    { 0,0,0,1 },
                };
                
                return  new MatrixFloat(quaternionMatrix);
            }
        }
        
        #region EulerAngles
        public static Quaternion Euler(Vector3 eulerAngles) => Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
        public static Quaternion Euler(float x, float y, float z)
        {
            Quaternion qRY = Quaternion.AngleAxis(y, new Vector3(0,1,0));
            Quaternion qRX = Quaternion.AngleAxis(x, new Vector3(1,0,0));
            Quaternion qRZ = Quaternion.AngleAxis(z, new Vector3(0,0,1));
            
            return qRY * qRX * qRZ;
        }

        public Vector3 EulerAngles
        {
            get
            {
                float p = (float)Math.Asin(-Matrix[1,2]);
                float cosP = (float)Math.Cos(p);
                float h;
                float b;
                if (cosP != 0)
                {
                    h = (float)Math.Atan2(Matrix[0, 2], Matrix[2, 2]);
                    b = (float)Math.Atan2(Matrix[1, 0], Matrix[1, 1]);
                }
                else
                {
                    h = (float)Math.Atan2(-Matrix[2, 0], Matrix[0, 0]);
                    b = 0;
                }
                
                return new Vector3(p * MathUtils.Rad2Deg, h * MathUtils.Rad2Deg, b * MathUtils.Rad2Deg);
            }
        }
        #endregion
    }
}