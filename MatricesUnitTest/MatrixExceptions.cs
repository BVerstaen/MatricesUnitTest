using System;

namespace Maths_Matrices.Tests
{
    public class MatrixSumException : Exception
    {
        public MatrixSumException(string message) : base(message){}
    }
    
    public class MatrixSubstractException : Exception
    {
        public MatrixSubstractException(string message) : base(message){}
    }
}