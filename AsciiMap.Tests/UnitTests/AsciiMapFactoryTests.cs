using System;
using AsciiMap.Core;
using AsciiMap.Core.Exceptions;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class AsciiMapFactoryTests
    {
        [Fact]
        public void SimpleMapCreated()
        {
            var mapBoard = AsciiMapBoardFactory.CreateBoard("@-A-x");
            Assert.NotNull(mapBoard);
        }

        [Fact]
        public void ComplexMapCreated()
        {
            string input = string.Join(Environment.NewLine,
                " @--A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+");
            var mapBoard = AsciiMapBoardFactory.CreateBoard(input);
            Assert.NotNull(mapBoard);
        }

        [Fact]
        public void NonAsciiCharacters()
        {
            var ex = Assert.Throws<NonAsciiCharacterException>(() => AsciiMapBoardFactory.CreateBoard("@-é-x"));
            Assert.Equal('é', ex.NonAsciiChar);
        }

        [Fact]
        public void NoStartingPosition()
        {
            Assert.Throws<NoStartingPositionException>(() => AsciiMapBoardFactory.CreateBoard("-A-x"));
        }

        [Fact]
        public void NoEndingPosition()
        {
            Assert.Throws<NoEndingPositionException>(() => AsciiMapBoardFactory.CreateBoard("@-A-"));
        }

        [Fact]
        public void DuplicateStartingPosition()
        {
            var ex = Assert.Throws<DuplicateStartingPositionException>(() => AsciiMapBoardFactory.CreateBoard("@-A-@-x"));
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

            var ex = Assert.Throws<UnevenRowSizeException>(() => AsciiMapBoardFactory.CreateBoard(input));
        }
    }
}
