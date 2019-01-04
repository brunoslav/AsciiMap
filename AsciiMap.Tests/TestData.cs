using System;

namespace AsciiMap.Tests
{
    class TestData
    {
        public static string SimpleMaze = string.Join(Environment.NewLine,
                "@---A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+");
        public static string SimpleMazeLetters = "ACB";
        public static string SimpleMazeCharacterPath = "@---A---+|C|+---+|+-B-x";

        public static string ComplexMaze1 = string.Join(Environment.NewLine,
                "@         ",
                "| C----+  ",
                "A |    |  ",
                "+---B--+  ",
                "  |      x",
                "  |      |",
                "  +---D--+");
        public static string ComplexMaze1Letters = "ABCD";
        public static string ComplexMaze1CharacterPath = "@|A+---B--+|+----C|-||+---D--+|x";

        public static string ComplexMaze2 = string.Join(Environment.NewLine,
                "  @---+   ",
                "      B   ",
                "K-----|--A",
                "|     |  |",
                "|  +--E  |",
                "|  |     |",
                "+--E--Ex C",
                "   |     |",
                "   +--F--+");
        public static string ComplexMaze2Letters = "BEEFCAKE";
        public static string ComplexMaze2CharacterPath = "@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex";
    }
}
