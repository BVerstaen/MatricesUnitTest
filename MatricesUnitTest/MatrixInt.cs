using System;

namespace Maths_Matrices.Tests
{
    public class MatrixInt
    {
        private int[,] matrice;

        #region Constructors
        public MatrixInt(int columns, int lines)
        {
            matrice = new int[columns, lines];
        }

        public MatrixInt(int[,] matrice)
        {
            this.matrice = matrice;
        }

        public MatrixInt(MatrixInt matriceToCopy)
        {
            matrice = new int[matriceToCopy.NbColumns, matriceToCopy.NbLines];
            Array.Copy(matriceToCopy.matrice, matriceToCopy.matrice.GetLowerBound(0), matrice, matrice.GetLowerBound(0), matrice.Length);
        }
        #endregion
        
        #region Fields & Getters / Setters
        public int NbLines => matrice.GetLength(0);
        public int NbColumns => matrice.GetLength(1);
        
        public int[,] ToArray2D() => matrice;
        
        public int this[int i, int i1]
        {
            get => matrice[i, i1];
            set => matrice[i, i1] = value;
        }
        #endregion
        
        #region Identity
        public static MatrixInt Identity(int diagonal)
        {
            MatrixInt newMatrice = new MatrixInt(diagonal, diagonal);
            for (int i = 0; i < diagonal; i++)
            {
                newMatrice[i, i] = 1;
            }
            return newMatrice;
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
                    if (matrice[j, i] != 0)
                    {
                        //Check if is a 1 in a diagonal
                        if (matrice[i, j] != 1 || i != j)
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
                    matrice[j, k] *= i;
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
                    newMatrix.matrice[j, k] *= i;
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

        public void Add(MatrixInt m2) => matrice = Addition(this, m2).matrice;
        
        private static MatrixInt Addition(MatrixInt m1, MatrixInt m2)
        {
            //Check if matrices have same number of lines / columns
            if(m1.NbLines != m2.NbLines || m1.NbColumns != m2.NbColumns)
                throw new ArgumentException("Matrices must have the same number of lines and columns.");

            MatrixInt newMatrice = new MatrixInt(m1.NbColumns, m1.NbLines);
            
            for (int i = 0; i < m1.NbLines; i++)
            {
                for (int j = 0; j < m1.NbColumns; j++)
                {
                    newMatrice[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return newMatrice;
        }

        public static MatrixInt Add(MatrixInt m1, MatrixInt m2)
        {
            return Addition(m1, m2);
        }
        
        #endregion
    }
}