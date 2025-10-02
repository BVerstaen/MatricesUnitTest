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

        public static readonly Vector3 Zero = new Vector3(0,0,0);
        public static readonly Vector3 One = new Vector3(1,1,1);
        
        public Vector3(MatrixFloat matrix)
        {
            if (matrix.NbColumns != 1 || matrix.NbLines != 4)
                throw new ArgumentException("Matrix must be 4x1");

            this.x = matrix[0, 0];
            this.y = matrix[1, 0];
            this.z = matrix[2, 0];
        }
        
        public static Vector3 operator+(Vector3 v1, Vector3 v2) => new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        public static Vector3 operator-(Vector3 v1, Vector3 v2) => new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        public static Vector3 MultiplyAxis(Vector3 v1, Vector3 v2) => new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        public static Vector3 DivideAxis(Vector3 v1, Vector3 v2) => new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
    }
}