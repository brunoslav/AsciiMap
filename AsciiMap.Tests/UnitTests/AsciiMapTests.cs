using System;
using AsciiMap.Core.Exceptions;
using Xunit;

namespace AsciiMap.Tests.UnitTests
{
    public class AsciiMapTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EmptyMap(string input)
        {
            Assert.Throws<EmptyMapException>(() => Core.AsciiMap.FindPath(input));
        }

        [Fact]
        public void MultipleDirectionsPossible()
        {
            var ex = Assert.Throws<InvalidMapPathException>(() => Core.AsciiMap.FindPath(string.Join(Environment.NewLine, " @ ", " | ", "-+-", "x  ")));
            Assert.Equal("@|+", ex.CurrentPath);
        }

        [Fact]
        public void SimpleMapSolved()
        {
            var result = Core.AsciiMap.FindPath(TestData.SimpleMap);

            Assert.Equal(TestData.SimpleMapLetters, result.Letters);
            Assert.Equal(TestData.SimpleMapCharacterPath, result.CharacterPath);
        }

        [Fact]
        public void ComplexMap1Solved()
        {
            var result = Core.AsciiMap.FindPath(TestData.ComplexMap1);

            Assert.Equal(TestData.ComplexMap1Letters, result.Letters);
            Assert.Equal(TestData.ComplexMap1CharacterPath, result.CharacterPath);
        }

        [Fact]
        public void ComplexMap2Solved()
        {
            var result = Core.AsciiMap.FindPath(TestData.ComplexMap2);

            Assert.Equal(TestData.ComplexMap2Letters, result.Letters);
            Assert.Equal(TestData.ComplexMap2CharacterPath, result.CharacterPath);
        }
    }
}
