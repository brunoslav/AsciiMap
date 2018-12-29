using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class EmptyInputException : Exception
    {
        public EmptyInputException()
        {
        }
    }
}