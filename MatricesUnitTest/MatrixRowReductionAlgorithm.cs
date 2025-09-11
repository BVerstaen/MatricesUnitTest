using System;

namespace Maths_Matrices.Tests
{
    public static class MatrixRowReductionAlgorithm
    {
        public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(m1, m2);
            
            for (int j = 0; j < augmentedMatrix.NbColumns; j++)
            {
                for (int i = 0; i < augmentedMatrix.NbLines; i++)
                {
                    //Find highest number
                    float highestNumber = augmentedMatrix[i, j];
                    int highestLine = i;
                    for (int k = i; k < augmentedMatrix.NbLines; k++)
                    {
                        if (augmentedMatrix[k, j] > highestNumber)
                        {
                            highestNumber = augmentedMatrix[k, j];
                            highestLine = k;
                        }
                    }
                    //Swap lines if necessary
                    if (highestLine != i)
                        MatrixElementaryOperations.SwapLines(augmentedMatrix, highestLine, i);
                    
                    //Multiply line
                    float lineScalarMultiplier = 1.0f / augmentedMatrix[i, j];
                    MatrixElementaryOperations.MultiplyLine(augmentedMatrix, i, lineScalarMultiplier);

                    //Add other lines
                    for (int r = 0; r < augmentedMatrix.NbLines; r++)
                    {
                        if (r != i)
                            continue;
                        
                        float factor = Math.Abs(-augmentedMatrix[r, j]);
                        MatrixElementaryOperations.AddLineToAnother(augmentedMatrix, i, r, factor);
                    }
                }
            }

            return augmentedMatrix.Split(augmentedMatrix.NbColumns - 2);
        }
    }
}