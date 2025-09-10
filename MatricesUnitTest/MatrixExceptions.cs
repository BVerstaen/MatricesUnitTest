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

    public class MatrixMultiplyException : Exception
    {
        public MatrixMultiplyException(string message) : base(message){}
    }

    public class MatrixScalarZeroException : Exception
    {
        public MatrixScalarZeroException(string message) : base(message){}
    }
}