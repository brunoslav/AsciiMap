using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AsciiMap.Tests")]
namespace AsciiMap.Core
{
    public class Maze
    {
        private readonly char[][] _mazeMap;
        private readonly int _rows;
        private readonly int _columns;

        private int _currentRowIndex;
        private int _currentColumnIndex;

        internal Maze(char[][] mazeMap, int rows, int columns, int startRowIndex, int startColumnIndex)
        {
            _mazeMap = mazeMap;
            _rows = rows;
            _columns = columns;
            _currentRowIndex = startRowIndex;
            _currentColumnIndex = startColumnIndex;
        }
    }
}
