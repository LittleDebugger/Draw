using System;

namespace Draw.Core.Exceptions
{
    public class CoreOperationException : Exception
    {
        internal CoreOperationException(string message)
            : base(message)
        {
        }
    }
}