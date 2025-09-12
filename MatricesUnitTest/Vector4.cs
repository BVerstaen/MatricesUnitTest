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
        
        public static Vector4 operator*(MatrixFloat m, Vector4 v)
        {
            Vector4 result = new Vector4();
            for (int i = 0; i < m.NbColumns; i++)
            {
                result.x += m[0, i] * v.x;
                result.y += m[1, i] * v.y;
                result.z += m[2, i] * v.z;
                result.w += m[3, i] * v.w;
            }
            return result;
        }
    }
}