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
            var maze = MazeBoardFactory.CreateMazeBoard("@-A-x");
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
            var maze = MazeBoardFactory.CreateMazeBoard(input);
            Assert.NotNull(maze);
        }



        [Fact]
        public void NonAsciiCharacters()
        {
            var ex = Assert.Throws<NonAsciiCharacterException>(() => MazeBoardFactory.CreateMazeBoard("@-é-x"));
            Assert.Equal('é', ex.NonAsciiChar);
        }

        [Fact]
        public void NoStartingPosition()
        {
            Assert.Throws<NoStartingPosition>(() => MazeBoardFactory.CreateMazeBoard("-A-x"));
        }

        [Fact]
        public void NoEndingPosition()
        {
            Assert.Throws<NoEndingPositionException>(() => MazeBoardFactory.CreateMazeBoard("@-A-"));
        }

        [Fact]
        public void DuplicateStartingPosition()
        {
            var ex = Assert.Throws<DuplicateStartingPositionException>(() => MazeBoardFactory.CreateMazeBoard("@-A-@-x"));
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

            var ex = Assert.Throws<UnevenRowSizeException>(() => MazeBoardFactory.CreateMazeBoard(input));
        }
    }
}
