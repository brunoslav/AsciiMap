using System;
using System.IO;
using AsciiMap.ConsoleApp;
using Moq;
using Xunit;

namespace AsciiMap.Tests.IntegrationTests
{
    public class SolveMazeTests
    {
        private readonly string[] MockFilePath = new string[] { string.Empty };

        [Fact]
        public void EmptyInput()
        {
            var output = RedirectOutput(() =>
            {
                MazeSolver mazeSolver = new MazeSolver(null);
                mazeSolver.Run(null);
            });

            Assert.Equal(FormattableString.Invariant($"Empty file path{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void FileDoesntExist()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(false);
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Non-existing file path{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void DuplicateStartingPosition()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("@-@");
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Multiple starting positions in map{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void EmptyMazeBoard()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("");
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"Input board is empty{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void InvalidMazePath()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(string.Join(Environment.NewLine, "@-+ ", "  | ", "x-+-"));
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Invalid maze, can't determine next step. Current resolved path: @-+|+{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void NoEndingPosition()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("@-a");
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"No ending position in map{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void NonAsciiCharacter()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("@-é-as--x");
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"Non-ASCII character in map: é{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void NoStartingPosition()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("-sa-x");
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"No starting position in map{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void UnevenRowSize()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(string.Join(Environment.NewLine, "@--+", "--x"));
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"Not all rows are same size{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void FileError()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Throws<Exception>();
                string[] args = new string[] { "test" };
                new MazeSolver(mockFileSystem.Object).Run(args);

            });

            Assert.Equal(FormattableString.Invariant($"Error opening file{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void SimpleMazeSolved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.SimpleMaze);
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.SimpleMazeLetters}{Environment.NewLine}Path as characters: {TestData.SimpleMazeCharacterPath}{Environment.NewLine}"), output);
        }

        [Fact]
        public void ComplexMaze1Solved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.ComplexMaze1);
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.ComplexMaze1Letters}{Environment.NewLine}Path as characters: {TestData.ComplexMaze1CharacterPath}{Environment.NewLine}"), output);
        }

        [Fact]
        public void ComplexMaze2Solved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.ComplexMaze2);
                new MazeSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.ComplexMaze2Letters}{Environment.NewLine}Path as characters: {TestData.ComplexMaze2CharacterPath}{Environment.NewLine}"), output);
        }


        private string RedirectOutput(Action action)
        {
            using (var output = new StringWriter())
            {
                Console.SetOut(output);
                action();
                return output.ToString();
            }
        }
    }
}
