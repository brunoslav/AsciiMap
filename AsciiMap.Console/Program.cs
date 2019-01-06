namespace AsciiMap.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new AsciiMapSolver(new FileSystem()).Run(args);
        }
    }
}
