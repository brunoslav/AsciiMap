using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class EmptyMapException : Exception
    {
        public EmptyMapException()
        {
        }
    }
}