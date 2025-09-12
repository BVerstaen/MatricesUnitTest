using System;
using System.Collections.Generic;

namespace Maths_Matrices.Tests
{
    public class MatrixFloat
    {
        private float[,] _matrix;

        #region Constructors
        public MatrixFloat(int lines, int columns)
        {
            _matrix = new float[lines, columns];
        }

        public MatrixFloat(float[,] matrix)
        {
            this._matrix = matrix;
        }

        public MatrixFloat(MatrixFloat matrixToCopy)
        {
            _matrix = new float[matrixToCopy.NbLines, matrixToCopy.NbColumns];
            Array.Copy(matrixToCopy._matrix, matrixToCopy._matrix.GetLowerBound(0), _matrix, _matrix.GetLowerBound(0), _matrix.Length);
        }
        #endregion
        
        #region Fields & Getters / Setters
        public int NbLines => _matrix.GetLength(0);
        public int NbColumns => _matrix.GetLength(1);
        
        public float[,] ToArray2D() => _matrix;
        
        public float this[int i, int i1]
        {
            get => _matrix[i, i1];
            set => _matrix[i, i1] = value;
        }
        #endregion
        
        #region Identity
        public static MatrixFloat Identity(int diagonal)
        {
            MatrixFloat newMatrix = new MatrixFloat(diagonal, diagonal);
            for (int i = 0; i < diagonal; i++)
            {
                newMatrix[i, i] = 1;
            }
            return newMatrix;
        }
        
        public bool IsIdentity()
        {
            if (NbLines != NbColumns)
                return false;
                
            for (int i = 0; i < NbColumns; i++)
            {
                for (int j = 0; j < NbLines; j++)
                {
                    //Check if there's anything else than a 0
                    if (_matrix[j, i] != 0)
                    {
                        //Check if is a 1 in a diagonal
                        if (Math.Abs(_matrix[i, j] - 1.0f) > 0.0f || i != j)
                            return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region Scalar Multiplication
        public void Multiply(float i)
        {
            for (int j = 0; j < NbLines; j++)
            {
                for (int k = 0; k < NbColumns; k++)
                {
                    _matrix[j, k] *= i;
                }
            }
        }

        public static MatrixFloat Multiply(MatrixFloat matrixFloat, float i)
        {
            MatrixFloat newMatrix = new MatrixFloat(matrixFloat);
            for (int j = 0; j < newMatrix.NbLines; j++)
            {
                for (int k = 0; k < newMatrix.NbColumns; k++)
                {
                    newMatrix._matrix[j, k] *= i;
                }
            }
            
            return newMatrix;
        }
        #endregion

        #region Scalar Operatos
        public static MatrixFloat operator *(MatrixFloat matrixFloat, int i) => MatrixFloat.Multiply(matrixFloat, i);
        public static MatrixFloat operator *(int i, MatrixFloat matrixFloat) => MatrixFloat.Multiply(matrixFloat, i);
        public static MatrixFloat operator -(MatrixFloat matrixFloat) => MatrixFloat.Multiply(matrixFloat, -1);
        #endregion

        #region Additions

        public void Add(MatrixFloat m2)
        {
            //Check if matrices have same number of lines / columns
            if(NbLines != m2.NbLines || NbColumns != m2.NbColumns)
                throw new MatrixSumException($"Matrices must have the same number of lines and columns.");
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _matrix[i, j] += m2[i, j];
                }
            }
        }

        public static MatrixFloat Add(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat newMatrix = new MatrixFloat(m1);
            newMatrix.Add(m2);
            return newMatrix;
        }
        
        public static MatrixFloat operator +(MatrixFloat m1, MatrixFloat m2) => MatrixFloat.Add(m1, m2);
        #endregion

        #region Substraction

        public void Subtract(MatrixFloat m2)
        {
            //Check if matrices have same number of lines / columns
            if(NbLines != m2.NbLines || NbColumns != m2.NbColumns)
                throw new MatrixSubstractException($"Matrices must have the same number of lines and columns.");
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _matrix[i, j] -= m2[i, j];
                }
            }
        }

        public static MatrixFloat Subtract(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat newMatrix = new MatrixFloat(m1);
            newMatrix.Subtract(m2);
            return newMatrix;
        }
        
        public static MatrixFloat operator -(MatrixFloat m1, MatrixFloat m2) => MatrixFloat.Subtract(m1, m2);

        #endregion

        #region MatricesMultiplication

        public MatrixFloat Multiply(MatrixFloat m2)
        {
            if(NbColumns != m2.NbLines)
                throw new MatrixMultiplyException($"Number of columns of m1 must be equal to the number of lines of m2.");
            
            MatrixFloat newMatrix = new MatrixFloat(NbLines, m2.NbColumns);

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    float result = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        float n1 = _matrix[i, k];
                        float n2 = m2[k, j];
                        result += n1 * n2; 
                    }
                    newMatrix[i,j] = result;
                }
            }
            
            return newMatrix;
        }
        
        public static MatrixFloat Multiply(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixFloat operator *(MatrixFloat m1, MatrixFloat m2) => MatrixFloat.Multiply(m1, m2);
        #endregion

        #region Transpose

        public MatrixFloat Transpose()
        {
            MatrixFloat transposedMatrix = new MatrixFloat(NbColumns, NbLines);

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    transposedMatrix[j,i] = _matrix[i, j];
                }
            }
            
            return transposedMatrix;
        }
        
        public static MatrixFloat Transpose(MatrixFloat m1) => m1.Transpose();
        
        #endregion

        #region AugmentedMatrix and Split
        public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat newMatrix = new MatrixFloat(m1.NbLines, m1.NbColumns + m2.NbColumns);

            for (int i = 0; i < newMatrix.NbLines; i++)
            {
                for (int j = 0; j < newMatrix.NbColumns; j++)
                {
                    newMatrix[i, j] = j < m1.NbColumns ? m1[i, j] : m2[i, j - m1.NbColumns];
                }
            }
            return  newMatrix;
        }
        
        public (MatrixFloat, MatrixFloat) Split(int i)
        {
            MatrixFloat m1 = new MatrixFloat(NbLines, i + 1);
            MatrixFloat m2 = new MatrixFloat(NbLines, NbColumns - (i+1));

            for (int j = 0; j < NbLines; j++)
            {
                for (int k = 0; k < NbColumns; k++)
                {
                    if(k <= i)
                        m1[j, k] = _matrix[j, k];
                    else
                        m2[j, k - (i+1)] = _matrix[j, k];
                }
            }
            
            return (m1, m2);
        }
        #endregion

        #region InverMatricesByRowReduction
        public MatrixFloat InvertByRowReduction()
        {
            MatrixFloat identityMatrix = MatrixFloat.Identity(NbLines);
            MatrixFloat m1 = new MatrixFloat(this);
            MatrixFloat m2 = new MatrixFloat(identityMatrix);
            (m1, m2) = MatrixRowReductionAlgorithm.Apply(this, m2, true);

            return m2;
        }
        
        public static MatrixFloat InvertByRowReduction(MatrixFloat m) => new MatrixFloat(m.InvertByRowReduction());
        #endregion

        #region SubMatrices
        public MatrixFloat SubMatrix(int lineToSub, int columnToSub)
        {
            MatrixFloat newMatrix = new MatrixFloat(NbLines - 1, NbColumns -1);

            int line = 0;
            int column = 0;
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i != lineToSub && j != columnToSub)
                    {
                        newMatrix[line, column] = _matrix[i, j];
                        column++;
                        if (column == newMatrix.NbColumns)
                        {
                            column = 0;
                            line++;
                        }
                    }
                }
            }
            return newMatrix;
        }
        
        public static MatrixFloat SubMatrix(MatrixFloat m, int lineToSub, int columnToSub) => new MatrixFloat(m.SubMatrix(lineToSub, columnToSub));
        #endregion

        #region Determinant
        public static float Determinant(MatrixFloat m)
        {
            //Handle 1x1 matrix
            if (m.NbColumns < 2)
                return m[0, 0];
            
            //Handle 2x2 matrix
            if(m.NbColumns == 2)
                return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
            
            float finalDeterminant = 0.0f;
            for (int i = 0; i < m.NbColumns; i++)
            {
                //get submatrix
                MatrixFloat subMatrix = m.SubMatrix(0, i);
                                    //Get cofactor                     //Recursive determinant
                finalDeterminant += (float)Math.Pow(-1, i) * m[0, i] * Determinant(subMatrix);
            }
            return finalDeterminant;
        }
        #endregion

        #region Adjugate
        public MatrixFloat Adjugate()
        {
            MatrixFloat transposedMatrix = Transpose();
            MatrixFloat adjugateMatrix = new MatrixFloat(NbLines, NbColumns);
    
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    MatrixFloat subMatrix = transposedMatrix.SubMatrix(i, j);
                    adjugateMatrix[i, j] = (float)Math.Pow(-1, i + j) * Determinant(subMatrix);
                }
            }

            return adjugateMatrix;
        }
        
        public static MatrixFloat Adjugate(MatrixFloat m) => m.Adjugate();
        #endregion

        #region InvertByDeterminant
        public MatrixFloat InvertByDeterminant()
        {
            float determinant = Determinant(this);
            if (determinant == 0)
                throw new MatrixInvertException("Determinant is zero. Matrix can't be inverted.");
            
            return Multiply(Adjugate(), 1 / determinant);
        }

        public static MatrixFloat InvertByDeterminant(MatrixFloat m) => m.InvertByDeterminant();
        #endregion

    }
}