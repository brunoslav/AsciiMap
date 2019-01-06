using AsciiMap.Core;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class AsciiMapBoardTests
    {
        [Fact]
        public void MoveOutsideOfBounds()
        {
            AsciiMapBoard mapBoard = new AsciiMapBoard(new char[1, 1] { { '@' } }, 1, 1, 0, 0);

            Assert.False(mapBoard.Move(MoveDirection.Down));
            Assert.False(mapBoard.Move(MoveDirection.Left));
            Assert.False(mapBoard.Move(MoveDirection.Up));
            Assert.False(mapBoard.Move(MoveDirection.Right));
        }

        [Fact]
        public void PeekOutsideOfBounds()
        {
            AsciiMapBoard mapBoard = new AsciiMapBoard(new char[1, 1] { { '@' } }, 1, 1, 0, 0);

            Assert.Null(mapBoard.PeekElement(MoveDirection.Down));
            Assert.Null(mapBoard.PeekElement(MoveDirection.Left));
            Assert.Null(mapBoard.PeekElement(MoveDirection.Up));
            Assert.Null(mapBoard.PeekElement(MoveDirection.Right));
        }

        [Fact]
        public void PeekInsideBounds()
        {
            AsciiMapBoard mapBoard = new AsciiMapBoard(new char[3, 3] { { ' ', '1', ' '},
            {' ', '@', '3' },
            {' ', 'a', ' ' } }, 3, 3, 1, 1);

            var element = mapBoard.PeekElement(MoveDirection.Up);

            Assert.NotNull(element);
            Assert.Equal('1', element);

            element = mapBoard.PeekElement(MoveDirection.Left);

            Assert.NotNull(element);
            Assert.Equal(' ', element);

            element = mapBoard.PeekElement(MoveDirection.Right);

            Assert.NotNull(element);
            Assert.Equal('3', element);

            element = mapBoard.PeekElement(MoveDirection.Down);

            Assert.NotNull(element);
            Assert.Equal('a', element);
        }

        [Fact]
        public void CanMoveToPosition()
        {
            AsciiMapBoard mapBoard = new AsciiMapBoard(new char[2, 3] { { ' ', '@', '3' }, { ' ', 'a', ' ' } }, 2, 3, 0, 1);

            Assert.False(mapBoard.CanMove(MoveDirection.Up));
            Assert.False(mapBoard.CanMove(MoveDirection.Left));
            Assert.True(mapBoard.CanMove(MoveDirection.Down));
            Assert.True(mapBoard.CanMove(MoveDirection.Right));
        }

        [Fact]
        public void MapMovement()
        {
            AsciiMapBoard mapBoard = new AsciiMapBoard(new char[2, 2] { { '1', '2' }, { '4', '3' } }, 2, 2, 0, 0);

            Assert.True(mapBoard.Move(MoveDirection.Right));
            Assert.Equal('2', mapBoard.CurrentElement);
            Assert.False(mapBoard.CurrentPositionVisited);

            Assert.True(mapBoard.Move(MoveDirection.Down));
            Assert.Equal('3', mapBoard.CurrentElement);
            Assert.False(mapBoard.CurrentPositionVisited);

            Assert.True(mapBoard.Move(MoveDirection.Left));
            Assert.Equal('4', mapBoard.CurrentElement);
            Assert.False(mapBoard.CurrentPositionVisited);

            Assert.True(mapBoard.Move(MoveDirection.Up));
            Assert.Equal('1', mapBoard.CurrentElement);
            Assert.True(mapBoard.CurrentPositionVisited);
        }
    }
}
