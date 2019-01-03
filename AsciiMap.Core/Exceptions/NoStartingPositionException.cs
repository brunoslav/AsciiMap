using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class NoStartingPositionException : Exception
    {
        public NoStartingPositionException()
        {
        }
    }
}