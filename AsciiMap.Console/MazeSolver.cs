using System;
using AsciiMap.Core;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.ConsoleApp
{
    public class MazeSolver
    {
        private IFileSystem _fileSystem;

        public MazeSolver(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Run(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Empty file path");
                return;
            }

            //check for input file
            var filePath = args[0];

            if (!_fileSystem.Exists(filePath))
            {
                Console.WriteLine("Non-existing file path");
                return;
            }

            string fileContent = "";

            try
            {
                fileContent = _fileSystem.ReadAllText(filePath);
            }
            catch
            {
                Console.WriteLine("Error opening file");
                return;
            }

            try
            {
                var result = Maze.SolveMaze(fileContent);
                Console.WriteLine(FormattableString.Invariant($"Letters: {result.Letters}"));
                Console.WriteLine(FormattableString.Invariant($"Path as characters: {result.CharacterPath}"));
            }
            catch (EmptyMazeBoardException)
            {
                Console.WriteLine("Input board is empty");
                return;
            }
            catch (DuplicateStartingPositionException)
            {
                Console.WriteLine("Multiple starting positions in map");
                return;
            }
            catch (InvalidMazePathException impe)
            {
                Console.WriteLine(FormattableString.Invariant($"Invalid maze, can't determine next step. Current resolved path: {impe.CurrentPath}"));
                return;
            }
            catch (NoEndingPositionException)
            {
                Console.WriteLine("No ending position in map");
                return;
            }
            catch (NonAsciiCharacterException nace)
            {
                Console.WriteLine(FormattableString.Invariant($"Non-ASCII character in map: {nace.NonAsciiChar}"));
                return;
            }
            catch (NoStartingPositionException)
            {
                Console.WriteLine("No starting position in map");
                return;
            }
            catch (UnevenRowSizeException)
            {
                Console.WriteLine("Not all rows are same size");
                return;
            }
        }
    }
}
