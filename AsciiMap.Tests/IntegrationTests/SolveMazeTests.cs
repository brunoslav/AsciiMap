using System;
using AsciiMap.Core;
using Xunit;

namespace AsciiMap.Tests.IntegrationTests
{
    public class SolveMazeTests
    {
        [Fact]
        public void SimpleMazeSolved()
        {
            string input = string.Join(Environment.NewLine,
                "@---A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+");

            var result = Maze.SolveMaze(input);

            Assert.Equal("ACB", result.Letters);
            Assert.Equal("@---A---+|C|+---+|+-B-x", result.CharacterPath);
        }

        [Fact]
        public void ComplexMaze1Solved()
        {
            string input = string.Join(Environment.NewLine,
                            "@---A---+",
                            "        |",
                            "x-B-+   C",
                            "    |   |",
                            "    +---+");

            var result = Maze.SolveMaze(input);

            Assert.Equal("ACB", result.Letters);
            Assert.Equal("@---A---+|C|+---+|+-B-x", result.CharacterPath);
        }

        [Fact]
        public void ComplexMaze2Solved()
        {
            string input = string.Join(Environment.NewLine,
                "  @---+   ",
                "      B   ",
                "K-----|--A",
                "|     |  |",
                "|  +--E  |",
                "|  |     |",
                "+--E--Ex C",
                "   |     |",
                "   +--F--+");

            var result = Maze.SolveMaze(input);

            Assert.Equal("BEEFCAKE", result.Letters);
            Assert.Equal("@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex", result.CharacterPath);
        }
    }
}
