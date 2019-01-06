using System;

namespace AsciiMap.Tests
{
    class TestData
    {
        public static string SimpleMap = string.Join(Environment.NewLine,
                "@---A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+");
        public static string SimpleMapLetters = "ACB";
        public static string SimpleMapCharacterPath = "@---A---+|C|+---+|+-B-x";

        public static string ComplexMap1 = string.Join(Environment.NewLine,
                "@         ",
                "| C----+  ",
                "A |    |  ",
                "+---B--+  ",
                "  |      x",
                "  |      |",
                "  +---D--+");
        public static string ComplexMap1Letters = "ABCD";
        public static string ComplexMap1CharacterPath = "@|A+---B--+|+----C|-||+---D--+|x";

        public static string ComplexMap2 = string.Join(Environment.NewLine,
                "  @---+   ",
                "      B   ",
                "K-----|--A",
                "|     |  |",
                "|  +--E  |",
                "|  |     |",
                "+--E--Ex C",
                "   |     |",
                "   +--F--+");
        public static string ComplexMap2Letters = "BEEFCAKE";
        public static string ComplexMap2CharacterPath = "@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex";
    }
}
