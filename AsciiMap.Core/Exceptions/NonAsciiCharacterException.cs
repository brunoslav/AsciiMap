using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class NonAsciiCharacterException : Exception
    {
        public char NonAsciiChar { get; }

        public NonAsciiCharacterException(char nonAsciiChar)
        {
            NonAsciiChar = nonAsciiChar;
        }
    }
}