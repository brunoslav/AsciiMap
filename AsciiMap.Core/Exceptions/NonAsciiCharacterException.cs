using System;

namespace AsciiMap.Core.Exceptions
{
    [Serializable]
    public class NonAsciiCharacterException : Exception
    {
        public NonAsciiCharacterException()
        {
        }

        public NonAsciiCharacterException(char c) : base(FormattableString.Invariant($"Non-ascii character: {c}"))
        {
        }
    }
}