using System;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.Core
{
    public class AsciiMapBoardFactory
    {
        public static AsciiMapBoard CreateBoard(string asciiMap)
        {
            if (string.IsNullOrEmpty(asciiMap))
                throw new EmptyMapException();

            var inputRows = asciiMap.Split(Environment.NewLine);
            int columns = inputRows[0].Length;

            bool startingPositionFound = false;
            bool endingPositionFound = false;

            int startingRowIndex = -1;
            int startingColumIndex = -1;

            char[,] parsedElements = new char[inputRows.Length, columns];

            for (int currentRowIndex = 0; currentRowIndex < inputRows.Length; currentRowIndex++)
            {
                var inputRow = inputRows[currentRowIndex];

                if (columns != inputRow.Length)
                    throw new UnevenRowSizeException();

                for (int currentColumnIndex = 0; currentColumnIndex < inputRow.Length; currentColumnIndex++)
                {
                    char c = inputRow[currentColumnIndex];

                    if (!IsAcsii(c))
                        throw new NonAsciiCharacterException(c);

                    if (IsStartingChar(c))
                    {
                        if (startingPositionFound)
                            throw new DuplicateStartingPositionException();

                        startingPositionFound = true;
                        startingRowIndex = currentRowIndex;
                        startingColumIndex = currentColumnIndex;
                    }

                    if (IsEndingChar(c))
                        endingPositionFound = true;

                    parsedElements[currentRowIndex, currentColumnIndex] = c;
                }
            }

            if (!startingPositionFound)
                throw new NoStartingPositionException();

            if (!endingPositionFound)
                throw new NoEndingPositionException();

            return new AsciiMapBoard(parsedElements, inputRows.Length, columns, startingRowIndex, startingColumIndex);
        }

        private static bool IsAcsii(char c)
        {
            return c <= sbyte.MaxValue;
        }

        private static bool IsStartingChar(char c)
        {
            return c == Constants.StartingPositionMark;
        }

        private static bool IsEndingChar(char c)
        {
            return c == Constants.EndingPositionMark;
        }
    }
}
