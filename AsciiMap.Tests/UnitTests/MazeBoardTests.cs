using AsciiMap.Core;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class MazeBoardTests
    {
        [Fact]
        public void MoveOutsideOfBounds()
        {
            MazeBoard mazeBoard = new MazeBoard(new char[1, 1] { { '@' } }, 1, 1, 0, 0);

            Assert.False(mazeBoard.Move(MoveDirection.Down));
            Assert.False(mazeBoard.Move(MoveDirection.Left));
            Assert.False(mazeBoard.Move(MoveDirection.Up));
            Assert.False(mazeBoard.Move(MoveDirection.Right));
        }

        [Fact]
        public void PeekOutsideOfBounds()
        {
            MazeBoard mazeBoard = new MazeBoard(new char[1, 1] { { '@' } }, 1, 1, 0, 0);

            Assert.Null(mazeBoard.PeekElement(MoveDirection.Down));
            Assert.Null(mazeBoard.PeekElement(MoveDirection.Left));
            Assert.Null(mazeBoard.PeekElement(MoveDirection.Up));
            Assert.Null(mazeBoard.PeekElement(MoveDirection.Right));
        }

        [Fact]
        public void PeekInsideBounds()
        {
            MazeBoard mazeBoard = new MazeBoard(new char[3, 3] { { ' ', '1', ' '},
            {' ', '@', '3' },
            {' ', 'a', ' ' } }, 3, 3, 1, 1);

            var element = mazeBoard.PeekElement(MoveDirection.Up);

            Assert.NotNull(element);
            Assert.Equal('1', element);

            element = mazeBoard.PeekElement(MoveDirection.Left);

            Assert.NotNull(element);
            Assert.Equal(' ', element);

            element = mazeBoard.PeekElement(MoveDirection.Right);

            Assert.NotNull(element);
            Assert.Equal('3', element);

            element = mazeBoard.PeekElement(MoveDirection.Down);

            Assert.NotNull(element);
            Assert.Equal('a', element);
        }

        [Fact]
        public void CanMoveToPosition()
        {
            MazeBoard mazeBoard = new MazeBoard(new char[2, 3] { { ' ', '@', '3' }, { ' ', 'a', ' ' } }, 2, 3, 0, 1);

            Assert.False(mazeBoard.CanMove(MoveDirection.Up));
            Assert.False(mazeBoard.CanMove(MoveDirection.Left));
            Assert.True(mazeBoard.CanMove(MoveDirection.Down));
            Assert.True(mazeBoard.CanMove(MoveDirection.Right));
        }

        [Fact]
        public void MazeMovement()
        {
            MazeBoard mazeBoard = new MazeBoard(new char[2, 2] { { '1', '2' }, { '4', '3' } }, 2, 2, 0, 0);

            Assert.True(mazeBoard.Move(MoveDirection.Right));
            Assert.Equal('2', mazeBoard.CurrentElement);
            Assert.False(mazeBoard.CurrentPositionVisited);

            Assert.True(mazeBoard.Move(MoveDirection.Down));
            Assert.Equal('3', mazeBoard.CurrentElement);
            Assert.False(mazeBoard.CurrentPositionVisited);

            Assert.True(mazeBoard.Move(MoveDirection.Left));
            Assert.Equal('4', mazeBoard.CurrentElement);
            Assert.False(mazeBoard.CurrentPositionVisited);

            Assert.True(mazeBoard.Move(MoveDirection.Up));
            Assert.Equal('1', mazeBoard.CurrentElement);
            Assert.True(mazeBoard.CurrentPositionVisited);
        }
    }
}
