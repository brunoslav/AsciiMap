using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AsciiMap.Tests")]
namespace AsciiMap.Core
{
    public class AsciiMapBoard
    {
        private const int PositionVisited = 1;

        private readonly char[,] _asciiMap;
        private readonly int[,] _mapTraversal;
        private readonly int _rows;
        private readonly int _columns;

        private int _currentRowIndex;
        private int _currentColumnIndex;

        internal AsciiMapBoard(char[,] asciiMap, int rows, int columns, int startRowIndex, int startColumnIndex)
        {
            _asciiMap = asciiMap;
            _mapTraversal = new int[rows, columns];
            _currentRowIndex = startRowIndex;
            _currentColumnIndex = startColumnIndex;
            _rows = rows;
            _columns = columns;
        }

        public bool CurrentPositionVisited
        {
            get { return _mapTraversal[_currentRowIndex, _currentColumnIndex] == PositionVisited; }
        }

        public char CurrentElement
        {
            get { return _asciiMap[_currentRowIndex, _currentColumnIndex]; }
        }

        public bool Move(MoveDirection direction)
        {
            if (!SimulateMove(direction, out int nextRow, out int nextColumn))
                return false;

            //mark current position as already visited before moving to next position
            _mapTraversal[_currentRowIndex, _currentColumnIndex] = PositionVisited;

            _currentRowIndex = nextRow;
            _currentColumnIndex = nextColumn;

            return true;
        }

        public bool CanMove(MoveDirection direction)
        {
            var element = PeekElement(direction);
            return element.HasValue && element != Constants.EmptyPositionMark;
        }

        public char? PeekElement(MoveDirection direction)
        {
            char? element = null;

            if (SimulateMove(direction, out int nextRow, out int nextColumn))
                element = _asciiMap[nextRow, nextColumn];

            return element;
        }

        private bool SimulateMove(MoveDirection direction, out int nextRow, out int nextColumn)
        {
            nextRow = _currentRowIndex;
            nextColumn = _currentColumnIndex;

            switch (direction)
            {
                case MoveDirection.Up:
                    nextRow--;
                    break;
                case MoveDirection.Down:
                    nextRow++;
                    break;
                case MoveDirection.Left:
                    nextColumn--;
                    break;
                case MoveDirection.Right:
                    nextColumn++;
                    break;
                case MoveDirection.None:
                default:
                    break;
            }

            if (nextRow < 0 || nextRow >= _rows || nextColumn < 0 || nextColumn >= _columns)
                return false;

            return true;
        }
    }
}
