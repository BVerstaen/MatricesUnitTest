namespace Maths_Matrices.Tests
{
    public class Transform
    {
        public Vector3 LocalPosition;
        public Vector3 LocalRotation;

        public Transform()
        {
            LocalPosition = Vector3.Zero;
            LocalRotation = Vector3.Zero;
        }

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
        
        #region LocalRotationMatrices

        public MatrixFloat LocalRotationXMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                return matrix;
            }
        }
        
        public MatrixFloat LocalRotationYMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                return matrix;
            }
        }
        
        public MatrixFloat LocalRotationZMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                return matrix;
            }
        }
        
        public MatrixFloat LocalRotationMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                return matrix;
            }
        }
        #endregion
    }
}