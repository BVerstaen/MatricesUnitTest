using System;

namespace Maths_Matrices.Tests
{
    public struct MatrixInt
    {
        private int[,] matrice;

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
            matrice = new int[0,0];
            
            Array.Copy(matriceToCopy, 0, matrice, 0,);
        }

        public int NbLines => matrice.GetLength(0);
        public int NbColumns => matrice.GetLength(1);
        public int[,] ToArray2D() => matrice;
        public int this[int i, int i1]
        {
            get => matrice[i, i1];
            set => matrice[i, i1] = value;
        }
    }
}