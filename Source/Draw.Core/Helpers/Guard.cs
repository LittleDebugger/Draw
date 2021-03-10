using System;

namespace Draw.Core.Helpers
{
    public static class Guard
    {
        public static void ThrowIfNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}