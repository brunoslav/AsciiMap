namespace AsciiMap.Core
{
    public class AsciiMapSolution
    {
        public string Letters { get; }
        public string CharacterPath { get; }

        public AsciiMapSolution(string letters, string characterPath)
        {
            Letters = letters;
            CharacterPath = characterPath;
        }
    }
}
