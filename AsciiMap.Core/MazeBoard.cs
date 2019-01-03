using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AsciiMap.Tests")]
namespace AsciiMap.Core
{
    public class MazeBoard
    {
        private const int PositionVisited = 1;

        private readonly char[,] _mazeMap;
        private readonly int[,] _mazeTraversal;
        private readonly int _rows;
        private readonly int _columns;

        private int _currentRowIndex;
        private int _currentColumnIndex;

        internal MazeBoard(char[,] mazeMap, int rows, int columns, int startRowIndex, int startColumnIndex)
        {
            _mazeMap = mazeMap;
            _mazeTraversal = new int[rows, columns];
            _currentRowIndex = startRowIndex;
            _currentColumnIndex = startColumnIndex;
            _rows = rows;
            _columns = columns;
        }

        public bool CurrentPositionVisited
        {
            get { return _mazeTraversal[_currentRowIndex, _currentColumnIndex] == PositionVisited; }
        }

        public char CurrentElement
        {
            get { return _mazeMap[_currentRowIndex, _currentColumnIndex]; }
        }

        public bool Move(MoveDirection direction)
        {
            if (!SimulateMove(direction, out int nextRow, out int nextColumn))
                return false;

            //mark current position as already visited before moving to next position
            _mazeTraversal[_currentRowIndex, _currentColumnIndex] = PositionVisited;

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
                element = _mazeMap[nextRow, nextColumn];

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
