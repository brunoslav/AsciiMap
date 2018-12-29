namespace AsciiMap.Core
{
    public class MazeSolution
    {
        public string Letters { get; }
        public string CharacterPath { get; }

        public MazeSolution(string letters, string characterPath)
        {
            Letters = letters;
            CharacterPath = characterPath;
        }
    }
}
