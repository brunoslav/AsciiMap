using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class InvalidMapPathException : Exception
    {
        public string CurrentPath { get; }

        public InvalidMapPathException(string currentPath)
        {
            CurrentPath = currentPath;
        }
    }
}