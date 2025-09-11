namespace Maths_Matrices.Tests
{
    public static class MatrixElementaryOperations
    {
        #region Swaps
        public static void SwapLines(MatrixInt m, int p1, int p2)
        {
            //swap values
            for (int i = 0; i < m.NbColumns; i++)
            {
                int p1Values = m[p1, i];
                int p2Values = m[p2, i];
                m[p1, i] = p2Values;
                m[p2, i] = p1Values;
            }
        }

        public static void SwapColumns(MatrixInt m, int line1, int line2)
        {
            //swap values
            for (int i = 0; i < m.NbLines; i++)
            {
                int p1Values = m[i, line1];
                int p2Values = m[i, line2];
                m[i, line1] = p2Values;
                m[i, line2] = p1Values;
            }
        }        
        #endregion

        #region Scalar Multiply

        public static void MultiplyLine(MatrixInt m, int line, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException("The scalar cannot be zero.");

            for (int i = 0; i < m.NbColumns; i++)
            {
                m[line, i] *= scalar;
            }
        }

        public static void MultiplyColumn(MatrixInt m, int column, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException("The scalar cannot be zero.");

            for (int i = 0; i < m.NbLines; i++)
            {
                m[i, column] *= scalar;
            }
        }
        
        #endregion

        #region AddToAnotherWithFactor
        public static void AddLineToAnother(MatrixInt m, int lineFrom, int lineTo, int factor = 1)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                m[lineTo, i] += m[lineFrom, i] * factor;
            }
        }

        public static void AddColumnToAnother(MatrixInt m, int columnFrom, int columnTo, int factor = 1)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                m[i, columnTo] += m[i, columnFrom] * factor;
            }
        }
        #endregion

    }
}