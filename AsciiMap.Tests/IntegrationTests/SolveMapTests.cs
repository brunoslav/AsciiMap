using System;
using System.IO;
using AsciiMap.ConsoleApp;
using Moq;
using Xunit;

namespace AsciiMap.Tests.IntegrationTests
{
    public class SolveMapTests
    {
        private readonly string[] MockFilePath = new string[] { string.Empty };

        [Fact]
        public void EmptyInput()
        {
            var output = RedirectOutput(() =>
            {
                AsciiMapSolver mapSolver = new AsciiMapSolver(null);
                mapSolver.Run(null);
            });

            Assert.Equal(FormattableString.Invariant($"Empty file path{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void MapFileDoesntExist()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(false);
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
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
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Multiple starting positions in map{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void EmptyMap()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("");
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);

            });

            Assert.Equal(FormattableString.Invariant($"Input board is empty{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void InvalidMapPath()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(string.Join(Environment.NewLine, "@-+ ", "  | ", "x-+-"));
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Invalid map, can't determine next step. Current resolved path: @-+|+{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void NoEndingPosition()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("@-a");
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);

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
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);

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
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);

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
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);

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
                new AsciiMapSolver(mockFileSystem.Object).Run(args);

            });

            Assert.Equal(FormattableString.Invariant($"Error opening file{ Environment.NewLine}"), output.ToString());
        }

        [Fact]
        public void SimpleMapSolved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.SimpleMap);
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.SimpleMapLetters}{Environment.NewLine}Path as characters: {TestData.SimpleMapCharacterPath}{Environment.NewLine}"), output);
        }

        [Fact]
        public void ComplexMap1Solved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.ComplexMap1);
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.ComplexMap1Letters}{Environment.NewLine}Path as characters: {TestData.ComplexMap1CharacterPath}{Environment.NewLine}"), output);
        }

        [Fact]
        public void ComplexMap2Solved()
        {
            var output = RedirectOutput(() =>
            {
                var mockFileSystem = new Mock<IFileSystem>();
                mockFileSystem.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
                mockFileSystem.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(TestData.ComplexMap2);
                new AsciiMapSolver(mockFileSystem.Object).Run(MockFilePath);
            });

            Assert.Equal(FormattableString.Invariant($"Letters: {TestData.ComplexMap2Letters}{Environment.NewLine}Path as characters: {TestData.ComplexMap2CharacterPath}{Environment.NewLine}"), output);
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
