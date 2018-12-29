using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class UnevenRowSizeException : Exception
    {
        public UnevenRowSizeException()
        {
        }
    }
}