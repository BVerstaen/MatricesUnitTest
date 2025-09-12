using System;

namespace Maths_Matrices.Tests
{
    public struct Vector4
    {
        public float x, y, z, w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(MatrixFloat matrix)
        {
            if(matrix.NbColumns != 1 || matrix.NbLines != 4)
                throw new ArgumentException("Matrix must be 4x1");
            
            this.x = matrix[0,0];
            this.y = matrix[1,0];
            this.z = matrix[2,0];
            this.w = matrix[3,0];
        }
        
        public static Vector4 operator*(MatrixFloat m, Vector4 v)
        {
            MatrixFloat vectorMatrix = new MatrixFloat(v);
            vectorMatrix = m * vectorMatrix;
            return  new Vector4(vectorMatrix);
        }
    }
}