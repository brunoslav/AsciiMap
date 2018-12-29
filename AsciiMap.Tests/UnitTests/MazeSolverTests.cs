using AsciiMap.Core;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class MazeSolverTests
    {
        [Fact]
        public void EmptyMaze()
        {
            MazeSolver mazeSolver = new MazeSolver();

            var result = mazeSolver.SolveMaze(null);

            Assert.Null(result.Letters);
            Assert.Null(result.CharacterPath);
        }

        [Fact]
        public void NoEndingCharacterOnPath()
        {
            Assert.True(false);
        }

        [Fact]
        public void SimpleMazeSolved()
        {
            Assert.True(false);
        }

        [Fact]
        public void ComplexMazeSolved()
        {
            Assert.True(false);
        }

        [Fact]
        public void SuperComplexMazeSolved()
        {
            Assert.True(false);
        }
    }
}
