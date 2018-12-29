using System;
using AsciiMap.Core;
using AsciiMap.Core.Exceptions;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class MazeFactoryTests
    {
        [Fact]
        public void SimpleMazeCreated()
        {
            var maze = MazeFactory.CreateMaze("@-A-x");

            Assert.NotNull(maze);
        }

        [Fact]
        public void ComplexMazeCreated()
        {
            string input = string.Join(Environment.NewLine,
                " @--A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+");
            var maze = MazeFactory.CreateMaze(input);

            Assert.NotNull(maze);
        }

        [Fact]
        public void EmptyInput()
        {
            var ex = Assert.Throws<EmptyInputException>(() => MazeFactory.CreateMaze(""));
            Assert.NotNull(ex);
        }

        [Fact]
        public void NonAsciiCharacters()
        {
            var ex = Assert.Throws<NonAsciiCharacterException>(() => MazeFactory.CreateMaze("@-é-x"));
            Assert.Equal("Invalid character: é", ex.Message);
        }

        [Fact]
        public void NoStartingPosition()
        {
            var ex = Assert.Throws<NoStartingPosition>(() => MazeFactory.CreateMaze("-A-x"));
            Assert.NotNull(ex);
        }

        [Fact]
        public void NoEndingPosition()
        {
            var ex = Assert.Throws<NoEndingPositionException>(() => MazeFactory.CreateMaze("@-A-"));
            Assert.NotNull(ex);
        }

        [Fact]
        public void DuplicateStartingPosition()
        {
            var ex = Assert.Throws<DuplicateStartingPositionException>(() => MazeFactory.CreateMaze("@-A-@-x"));
            Assert.NotNull(ex);
        }

        [Fact]
        public void UnevenRowSize()
        {
            string input = string.Join(Environment.NewLine,
              "@--A---+",
              "        |",
              "x-B-+   C",
              "    |   |",
              "    +---+");

            var ex = Assert.Throws<UnevenRowSizeException>(() => MazeFactory.CreateMaze(input));
            Assert.NotNull(ex);
        }
    }
}
