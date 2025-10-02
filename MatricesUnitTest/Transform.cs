using System;

namespace Maths_Matrices.Tests
{
    public class Transform
    {
        private const float Deg2Rad = (float)(Math.PI / 180f);

        public Vector3 LocalPosition;
        public Vector3 LocalRotation;
        public Vector3 LocalScale;

        private Transform _parentTransform = null;
        
        public Transform()
        {
            LocalPosition = Vector3.Zero;
            LocalRotation = Vector3.Zero;
            LocalScale = Vector3.One;
        }

        #region World Coordinates

        public Vector3 WorldPosition
        {
            get
            {
                if (_parentTransform != null)
                {
                    return _parentTransform.WorldPosition + (LocalPosition * WorldScale);
                }
                return LocalPosition;
            }
            set
            {
                if (_parentTransform != null)
                {
                    return;
                }
                LocalPosition = value;
            }
        }

        public Vector3 WorldRotation
        {
            get
            {
                if (_parentTransform != null)
                {
                    return _parentTransform.LocalRotation + LocalRotation;
                }
                return LocalRotation;
            }
        }
        
        public Vector3 WorldScale
        {
            get
            {
                if (_parentTransform != null)
                {
                    return LocalScale * _parentTransform.LocalScale;
                }
                return LocalScale;
            }
        }

        #endregion

        
        #region TranslationMatrices

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

        public MatrixFloat WorldTranslationMatrix
        {
            get
            {
                if (_parentTransform == null)
                    return LocalTranslationMatrix;
                
                MatrixFloat matrix = MatrixFloat.Identity(4);
                //Set position manually
                matrix[0, 3] = WorldPosition.x;
                matrix[1, 3] = WorldPosition.y;
                matrix[2, 3] = WorldPosition.z;

                return matrix;
            }
        }

        #endregion
        
        #region RotationMatrices

        private void CalculateTheta(float degree, out float cosTheta, out float sinTheta)
        {
            float radians = degree * Deg2Rad;
            cosTheta = (float)Math.Cos(radians);
            sinTheta = (float)Math.Sin(radians);
        }

        public MatrixFloat LocalRotationXMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(LocalRotation.x, out float cosTheta, out float sinTheta);

                //Set X rotations
                matrix[1, 1] = cosTheta;
                matrix[2, 2] = cosTheta;
                matrix[1, 2] = -sinTheta;
                matrix[2, 1] = sinTheta;
                return matrix;
            }
        }

        public MatrixFloat LocalRotationYMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(LocalRotation.y, out float cosTheta, out float sinTheta);

                //Set Y rotations
                matrix[0, 0] = cosTheta;
                matrix[2, 2] = cosTheta;
                matrix[0, 2] = sinTheta;
                matrix[2, 0] = -sinTheta;
                return matrix;
            }
        }

        public MatrixFloat LocalRotationZMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(LocalRotation.z, out float cosTheta, out float sinTheta);

                //Set Z rotations
                matrix[0, 0] = cosTheta;
                matrix[1, 1] = cosTheta;
                matrix[1, 0] = sinTheta;
                matrix[0, 1] = -sinTheta;

                return matrix;
            }
        }

        public MatrixFloat LocalRotationMatrix => LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;

        public MatrixFloat WorldRotationMatrix
        {
            get
            {
                if (_parentTransform == null)
                    return LocalRotationMatrix;

                return WorldRotationXMatrix * WorldRotationYMatrix * WorldlRotationZMatrix;
            }
        }
        private MatrixFloat WorldRotationXMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(WorldRotation.x, out float cosTheta, out float sinTheta);

                //Set X rotations
                matrix[1, 1] = cosTheta;
                matrix[2, 2] = cosTheta;
                matrix[1, 2] = -sinTheta;
                matrix[2, 1] = sinTheta;
                return matrix;
            }
        }

        private MatrixFloat WorldRotationYMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(WorldRotation.y, out float cosTheta, out float sinTheta);

                //Set Y rotations
                matrix[0, 0] = cosTheta;
                matrix[2, 2] = cosTheta;
                matrix[0, 2] = sinTheta;
                matrix[2, 0] = -sinTheta;
                return matrix;
            }
        }

        private MatrixFloat WorldlRotationZMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                CalculateTheta(WorldRotation.z, out float cosTheta, out float sinTheta);

                //Set Z rotations
                matrix[0, 0] = cosTheta;
                matrix[1, 1] = cosTheta;
                matrix[1, 0] = sinTheta;
                matrix[0, 1] = -sinTheta;

                return matrix;
            }
        }

        
        #endregion

        #region ScaleMatrices
        public MatrixFloat LocalScaleMatrix
        {
            get
            {
                MatrixFloat matrix = MatrixFloat.Identity(4);
                matrix[0, 0] = LocalScale.x;
                matrix[1, 1] = LocalScale.y;
                matrix[2, 2] = LocalScale.z;
                return matrix;
            }
        }

        public MatrixFloat WorldScaleMatrix
        {
            get
            {
                if (_parentTransform == null)
                    return LocalScaleMatrix;
                
                MatrixFloat matrix = MatrixFloat.Identity(4);

                matrix[0, 0] = WorldScale.x;
                matrix[1, 1] = WorldScale.y;
                matrix[2, 2] = WorldScale.z;
                return matrix;
            }
        }
        #endregion


        public MatrixFloat LocalToWorldMatrix => WorldTranslationMatrix * WorldRotationMatrix * WorldScaleMatrix;
        public MatrixFloat WorldToLocalMatrix => MatrixFloat.InvertByDeterminant(LocalToWorldMatrix);

        public void SetParent(Transform tParent)
        {
            _parentTransform = tParent;
        }
    }
}