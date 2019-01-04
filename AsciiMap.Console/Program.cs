namespace AsciiMap.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new MazeSolver(new FileSystem()).Run(args);
        }
    }
}
