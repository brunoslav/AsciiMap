using System;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.ConsoleApp
{
    public class AsciiMapSolver
    {
        private IFileSystem _fileSystem;

        public AsciiMapSolver(IFileSystem fileSystem)
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
                var result = Core.AsciiMap.FindPath(fileContent);
                Console.WriteLine(FormattableString.Invariant($"Letters: {result.Letters}"));
                Console.WriteLine(FormattableString.Invariant($"Path as characters: {result.CharacterPath}"));
            }
            catch (EmptyMapException)
            {
                Console.WriteLine("Input board is empty");
                return;
            }
            catch (DuplicateStartingPositionException)
            {
                Console.WriteLine("Multiple starting positions in map");
                return;
            }
            catch (InvalidMapPathException impe)
            {
                Console.WriteLine(FormattableString.Invariant($"Invalid map, can't determine next step. Current resolved path: {impe.CurrentPath}"));
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
