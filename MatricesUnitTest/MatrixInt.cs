using System;

namespace Maths_Matrices.Tests
{
    public class MatrixInt
    {
        private int[,] _matrix;

        #region Constructors
        public MatrixInt(int lines, int columns)
        {
            _matrix = new int[lines, columns];
        }

        public MatrixInt(int[,] matrix)
        {
            this._matrix = matrix;
        }

        public MatrixInt(MatrixInt matrixToCopy)
        {
            _matrix = new int[matrixToCopy.NbLines, matrixToCopy.NbColumns];
            Array.Copy(matrixToCopy._matrix, matrixToCopy._matrix.GetLowerBound(0), _matrix, _matrix.GetLowerBound(0), _matrix.Length);
        }
        #endregion
        
        #region Fields & Getters / Setters
        public int NbLines => _matrix.GetLength(0);
        public int NbColumns => _matrix.GetLength(1);
        
        public int[,] ToArray2D() => _matrix;
        
        public int this[int i, int i1]
        {
            get => _matrix[i, i1];
            set => _matrix[i, i1] = value;
        }
        #endregion
        
        #region Identity
        public static MatrixInt Identity(int diagonal)
        {
            MatrixInt newMatrix = new MatrixInt(diagonal, diagonal);
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
                        if (_matrix[i, j] != 1 || i != j)
                            return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region Scalar Multiplication
        public void Multiply(int i)
        {
            for (int j = 0; j < NbLines; j++)
            {
                for (int k = 0; k < NbColumns; k++)
                {
                    _matrix[j, k] *= i;
                }
            }
        }

        public static MatrixInt Multiply(MatrixInt matrixInt, int i)
        {
            MatrixInt newMatrix = new MatrixInt(matrixInt);
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
        public static MatrixInt operator *(MatrixInt matrixInt, int i) => MatrixInt.Multiply(matrixInt, i);
        public static MatrixInt operator *(int i, MatrixInt matrixInt) => MatrixInt.Multiply(matrixInt, i);
        public static MatrixInt operator -(MatrixInt matrixInt) => MatrixInt.Multiply(matrixInt, -1);
        #endregion

        #region Additions

        public void Add(MatrixInt m2)
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

        public static MatrixInt Add(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt newMatrix = new MatrixInt(m1);
            newMatrix.Add(m2);
            return newMatrix;
        }
        
        public static MatrixInt operator +(MatrixInt m1, MatrixInt m2) => MatrixInt.Add(m1, m2);
        #endregion

        #region Substraction

        public void Subtract(MatrixInt m2)
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

        public static MatrixInt Subtract(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt newMatrix = new MatrixInt(m1);
            newMatrix.Subtract(m2);
            return newMatrix;
        }
        
        public static MatrixInt operator -(MatrixInt m1, MatrixInt m2) => MatrixInt.Subtract(m1, m2);

        #endregion

        #region MatricesMultiplication

        public MatrixInt Multiply(MatrixInt m2)
        {
            if(NbColumns != m2.NbLines)
                throw new MatrixMultiplyException($"Number of columns of m1 must be equal to the number of lines of m2.");
            
            MatrixInt newMatrix = new MatrixInt(NbLines, m2.NbColumns);

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    int result = 0;
                    for (int k = 0; k < NbColumns; k++)
                    {
                        int n1 = _matrix[i, k];
                        int n2 = m2[k, j];
                        result += n1 * n2; 
                    }
                    newMatrix[i,j] = result;
                }
            }
            
            return newMatrix;
        }
        
        public static MatrixInt Multiply(MatrixInt m1, MatrixInt m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixInt operator *(MatrixInt m1, MatrixInt m2) => MatrixInt.Multiply(m1, m2);
        #endregion

        #region Transpose

        public MatrixInt Transpose()
        {
            MatrixInt transposedMatrix = new MatrixInt(NbColumns, NbLines);

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    transposedMatrix[j,i] = _matrix[i, j];
                }
            }
            
            return transposedMatrix;
        }
        
        public static MatrixInt Transpose(MatrixInt m1) => m1.Transpose();
        
        #endregion

        #region AugmentedMatrix and Split
        public static MatrixInt GenerateAugmentedMatrix(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt newMatrix = new MatrixInt(m1.NbLines, m1.NbColumns + m2.NbColumns);

            for (int i = 0; i < newMatrix.NbLines; i++)
            {
                for (int j = 0; j < newMatrix.NbColumns; j++)
                {
                    newMatrix[i, j] = j < m1.NbColumns ? m1[i, j] : m2[i, j - m1.NbColumns];
                }
            }
            return  newMatrix;
        }
        
        public (MatrixInt, MatrixInt) Split(int i)
        {
            MatrixInt m1 = new MatrixInt(NbLines, i + 1);
            MatrixInt m2 = new MatrixInt(NbLines, NbColumns - (i+1));

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


    }
}