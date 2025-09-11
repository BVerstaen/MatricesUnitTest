// using System;
//
// namespace Maths_Matrices.Tests
// {
//     public class MatrixGeneric<T>
//     {
//         protected T[,] _matrix;
//
//         #region Abstract basic operations
//         
//         //These basic functions need to be implemented
//         //because they varied from type to type
//         //Straight T+T retruns an error. 
//         
//         protected virtual T Addition(T a, T b)
//         {
//             throw new NotImplementedException();
//         }
//         
//         protected virtual T Subtraction(T a, T b)
//         {
//             throw new NotImplementedException();
//         }
//         protected virtual T Multiplication(T a, T b)
//         {
//             throw new NotImplementedException();
//         }
//
//         #endregion
//         
//         #region Constructors
//         public MatrixGeneric(int lines, int columns)
//         {
//             _matrix = new T[lines, columns];
//         }
//
//         public MatrixGeneric(T[,] matrix)
//         {
//             this._matrix = matrix;
//         }
//
//         public MatrixGeneric(MatrixGeneric<T> matrixToCopy)
//         {
//             _matrix = new T[matrixToCopy.NbLines, matrixToCopy.NbColumns];
//             Array.Copy(matrixToCopy._matrix, matrixToCopy._matrix.GetLowerBound(0), _matrix, _matrix.GetLowerBound(0), _matrix.Length);
//         }
//         #endregion
//         
//         #region Fields & Getters / Setters
//         public int NbLines => _matrix.GetLength(0);
//         public int NbColumns => _matrix.GetLength(1);
//         
//         public T[,] ToArray2D() => _matrix;
//         
//         public T this[int i, int i1]
//         {
//             get => _matrix[i, i1];
//             set => _matrix[i, i1] = value;
//         }
//         #endregion
//
//         #region Additions
//
//         public void Add(MatrixGeneric<T> m2)
//         {
//             //Check if matrices have same number of lines / columns
//             if(NbLines != m2.NbLines || NbColumns != m2.NbColumns)
//                 throw new MatrixSumException($"Matrices must have the same number of lines and columns.");
//             
//             for (int i = 0; i < NbLines; i++)
//             {
//                 for (int j = 0; j < NbColumns; j++)
//                 {
//                     _matrix[i, j] = Addition(_matrix[i, j], m2[i, j]);
//                 }
//             }
//         }
//
//         public static MatrixGeneric<T> Add(MatrixGeneric<T> m1, MatrixGeneric<T> m2)
//         {
//             MatrixGeneric<T> newMatrix = new MatrixGeneric<T>(m1);
//             newMatrix.Add(m2);
//             return newMatrix;
//         }
//         
//         public static MatrixGeneric<T> operator +(MatrixGeneric<T> m1, MatrixGeneric<T> m2) => MatrixGeneric<T>.Add(m1, m2);
//         #endregion
//         
//         #region Substraction
//
//         public void Subtract(MatrixGeneric<T> m2)
//         {
//             //Check if matrices have same number of lines / columns
//             if(NbLines != m2.NbLines || NbColumns != m2.NbColumns)
//                 throw new MatrixSubstractException($"Matrices must have the same number of lines and columns.");
//             
//             for (int i = 0; i < NbLines; i++)
//             {
//                 for (int j = 0; j < NbColumns; j++)
//                 {
//                     _matrix[i, j] = Subtraction(_matrix[i, j], m2[i, j]);
//                 }
//             }
//         }
//
//         public static MatrixGeneric<T> Subtract(MatrixGeneric<T> m1, MatrixGeneric<T> m2)
//         {
//             MatrixGeneric<T> newMatrix = new MatrixGeneric<T>(m1);
//             newMatrix.Subtract(m2);
//             return newMatrix;
//         }
//         
//         public static MatrixGeneric<T> operator -(MatrixGeneric<T> m1, MatrixGeneric<T> m2) => MatrixGeneric<T>.Subtract(m1, m2);
//
//         #endregion
//         
//         #region MatricesMultiplication
//
//         public MatrixGeneric<T> Multiply(MatrixGeneric<T> m2)
//         {
//             if(NbColumns != m2.NbLines)
//                 throw new MatrixMultiplyException($"Number of columns of m1 must be equal to the number of lines of m2.");
//             
//             MatrixGeneric<T> newMatrix = new MatrixGeneric<T>(NbLines, m2.NbColumns);
//
//             for (int i = 0; i < NbLines; i++)
//             {
//                 for (int j = 0; j < m2.NbColumns; j++)
//                 {
//                     T result = default(T);
//                     for (int k = 0; k < NbColumns; k++)
//                     {
//                         T n1 = _matrix[i, k];
//                         T n2 = m2[k, j];
//                         result = Multiplication(n1, n2);
//                         result = Addition(result, result);
//                     }
//                     newMatrix[i,j] = result;
//                 }
//             }
//             
//             return newMatrix;
//         }
//         
//         public static MatrixGeneric<T> Multiply(MatrixGeneric<T> m1, MatrixGeneric<T> m2)
//         {
//             return m1.Multiply(m2);
//         }
//         
//         public static MatrixGeneric<T> operator *(MatrixGeneric<T> m1, MatrixGeneric<T> m2) => MatrixGeneric<T>.Multiply(m1, m2);
//         #endregion
//         
//         #region Transpose
//
//         public MatrixGeneric<T> Transpose()
//         {
//             MatrixGeneric<T> transposedMatrix = new MatrixGeneric<T>(NbColumns, NbLines);
//
//             for (int i = 0; i < NbLines; i++)
//             {
//                 for (int j = 0; j < NbColumns; j++)
//                 {
//                     transposedMatrix[j,i] = _matrix[i, j];
//                 }
//             }
//             
//             return transposedMatrix;
//         }
//         
//         public static MatrixGeneric<T> Transpose(MatrixGeneric<T> m1) => m1.Transpose();
//         
//         #endregion
//         
//         #region AugmentedMatrix and Split
//         public static MatrixGeneric<T> GenerateAugmentedMatrix(MatrixGeneric<T> m1, MatrixGeneric<T> m2)
//         {
//             MatrixGeneric<T> newMatrix = new MatrixGeneric<T>(m1.NbLines, m1.NbColumns + m2.NbColumns);
//
//             for (int i = 0; i < newMatrix.NbLines; i++)
//             {
//                 for (int j = 0; j < newMatrix.NbColumns; j++)
//                 {
//                     newMatrix[i, j] = j < m1.NbColumns ? m1[i, j] : m2[i, j - m1.NbColumns];
//                 }
//             }
//             return  newMatrix;
//         }
//         
//         public (MatrixGeneric<T>, MatrixGeneric<T>) Split(int i)
//         {
//             MatrixGeneric<T> m1 = new MatrixGeneric<T>(NbLines, i + 1);
//             MatrixGeneric<T> m2 = new MatrixGeneric<T>(NbLines, NbColumns - (i+1));
//
//             for (int j = 0; j < NbLines; j++)
//             {
//                 for (int k = 0; k < NbColumns; k++)
//                 {
//                     if(k <= i)
//                         m1[j, k] = _matrix[j, k];
//                     else
//                         m2[j, k - (i+1)] = _matrix[j, k];
//                 }
//             }
//             
//             return (m1, m2);
//         }
//         #endregion
//     }
// }