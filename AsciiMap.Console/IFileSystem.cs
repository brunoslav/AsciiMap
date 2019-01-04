namespace AsciiMap.ConsoleApp
{
    public interface IFileSystem
    {
        bool Exists(string filePath);
        string ReadAllText(string filePath);
    }
}
