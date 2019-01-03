using AsciiMap.Core;
using AsciiMap.Core.Exceptions;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class MazeTests
    {
        [Fact]
        public void EmptyMazeMap()
        {
            Assert.Throws<EmptyMazeBoardException>(() => Maze.SolveMaze(""));
        }

        [Fact]
        public void CheckDirectionElement()
        {
            //var mazeBoard = new MazeBoard(null, 0, 0, 0, 0);
            //Maze maze = new Maze(mazeBoard);


        }

        //[Fact]
        //public void CheckDirectionElement()
        //{
        //    var mazeBoard = new MazeBoard(null, 0, 0, 0, 0);
        //    Maze maze = new Maze(mazeBoard);


        //}

        //[Fact]
        //public void CheckDirectionElement()
        //{
        //    var mazeBoard = new MazeBoard(null, 0, 0, 0, 0);
        //    Maze maze = new Maze(mazeBoard);


        //}

    }
}
