using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class NoEndingPositionException : Exception
    {
        public NoEndingPositionException()
        {
        }
    }
}