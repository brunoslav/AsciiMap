using AsciiMap.Core.Exceptions;

namespace AsciiMap.Core
{
    public class MazeSolver
    {

        public MazeSolution SolveMaze(Maze maze)
        {
            if (maze == null)
                throw new EmptyMazeException();

            

            return new MazeSolution(null, null);
        }
    }
}
