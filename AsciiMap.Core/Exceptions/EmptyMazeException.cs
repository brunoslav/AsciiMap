using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    internal class EmptyMazeException : Exception
    {
        public EmptyMazeException()
        {
        }
    }
}