using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class DuplicateStartingPositionException : Exception
    {
        public DuplicateStartingPositionException()
        {
        }
    }
}