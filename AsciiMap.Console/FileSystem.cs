using System.IO;

namespace AsciiMap.ConsoleApp
{
    class FileSystem : IFileSystem
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
