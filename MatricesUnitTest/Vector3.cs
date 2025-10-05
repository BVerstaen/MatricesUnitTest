using System;

namespace Maths_Matrices.Tests
{
    public struct Vector3
    {
        public float x, y, z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public Vector3(MatrixFloat matrix)
        {
            if (matrix.NbColumns != 1 || matrix.NbLines != 3)
                throw new ArgumentException("Matrix must be 3x1");

            this.x = matrix[0, 0];
            this.y = matrix[1, 0];
            this.z = matrix[2, 0];
        }

        #region Operators

        public static Vector3 operator +(Vector3 v1, Vector3 v2) => new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        public static Vector3 operator -(Vector3 v1, Vector3 v2) => new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);

        public static Vector3 operator *(Quaternion q, Vector3 v)
        {
            Quaternion vectorQuaternion = Quaternion.Euler(v);
            return (q * vectorQuaternion * q.conjugated).EulerAngles;
        }

        #endregion

        public static Vector3 MultiplyAxis(Vector3 v1, Vector3 v2) => new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);

        public static Vector3 MultiplyPoint(MatrixFloat m, Vector3 v)
        {
            float x = m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z + m[0, 3];
            float y = m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z + m[1, 3];
            float z = m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z + m[2, 3];
            return new Vector3(x, y, z);
        }

        public Vector3 normalized
        {
            get
            {
                float length = (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2) + Math.Pow(z,2));
                return new Vector3(x/length, y/length, z/length);
            }
        }
    }
}