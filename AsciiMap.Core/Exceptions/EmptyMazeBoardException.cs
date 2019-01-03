using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class EmptyMazeBoardException : Exception
    {
        public EmptyMazeBoardException()
        {
        }
    }
}