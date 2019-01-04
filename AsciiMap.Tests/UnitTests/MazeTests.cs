using System;
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
        public void MultipleDirectionsPossible()
        {
            var ex = Assert.Throws<InvalidMazePathException>(() => Maze.SolveMaze(string.Join(Environment.NewLine, " @ ", " | ", "-+-", "x  ")));
            Assert.Equal("@|+", ex.CurrentPath);
        }

        [Fact]
        public void SimpleMazeSolved()
        {
            var result = Maze.SolveMaze(TestData.SimpleMaze);

            Assert.Equal(TestData.SimpleMazeLetters, result.Letters);
            Assert.Equal(TestData.SimpleMazeCharacterPath, result.CharacterPath);
        }

        [Fact]
        public void ComplexMaze1Solved()
        {
            var result = Maze.SolveMaze(TestData.ComplexMaze1);

            Assert.Equal(TestData.ComplexMaze1Letters, result.Letters);
            Assert.Equal(TestData.ComplexMaze1CharacterPath, result.CharacterPath);
        }

        [Fact]
        public void ComplexMaze2Solved()
        {
            var result = Maze.SolveMaze(TestData.ComplexMaze2);

            Assert.Equal(TestData.ComplexMaze2Letters, result.Letters);
            Assert.Equal(TestData.ComplexMaze2CharacterPath, result.CharacterPath);
        }
    }
}
