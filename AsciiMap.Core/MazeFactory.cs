using System;
using System.Collections.Generic;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.Core
{
    public class MazeFactory
    {
        public static Maze CreateMaze(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new EmptyInputException();

            var inputRows = input.Split(Environment.NewLine);

            bool startingPositionFound = false;
            bool endingPositionFound = false;

            int startingRowIndex = -1;
            int startingColumIndex = -1;
            int columns = -1;

            List<char[]> passedRows = new List<char[]>();

            for (int currentRowIndex = 0; currentRowIndex < inputRows.Length; currentRowIndex++)
            {
                var inputRow = inputRows[currentRowIndex];

                if (columns == -1)
                    columns = inputRow.Length;
                else if (columns != inputRow.Length)
                    throw new UnevenRowSizeException();

                char[] rowChars = new char[columns];

                int currentColumnIndex = 0;

                foreach (char c in inputRow)
                {
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

                    rowChars[currentColumnIndex++] = c;
                }

                passedRows.Add(rowChars);
            }

            if (!startingPositionFound)
                throw new NoStartingPosition();

            if (!endingPositionFound)
                throw new NoEndingPositionException();

            return new Maze(passedRows.ToArray(), passedRows.Count, columns, startingRowIndex, startingColumIndex);
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
