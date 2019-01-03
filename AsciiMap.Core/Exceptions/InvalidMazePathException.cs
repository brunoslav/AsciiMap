using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class InvalidMazePathException : Exception
    {
        public string CurrentPath { get; }

        public InvalidMazePathException(string currentPath)
        {
            CurrentPath = currentPath;
        }
    }
}