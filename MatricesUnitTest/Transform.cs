namespace Maths_Matrices.Tests
{
    public class Transform
    {
        public Vector3 LocalPosition;

        public MatrixFloat LocalTranslationMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                //Set position manually
                matrix[0, 3] = LocalPosition.x;
                matrix[1, 3] = LocalPosition.y;
                matrix[2, 3] = LocalPosition.z;
                
                return matrix;
            }
        }

        public Transform()
        {
            LocalPosition = new Vector3(0f, 0f, 0f);
        }
    }
}