using System.Text;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.Core
{
    public class Maze
    {
        public static MazeSolution SolveMaze(string mazeMap)
        {
            return SolveMaze(MazeBoardFactory.CreateMazeBoard(mazeMap));
        }

        private static MazeSolution SolveMaze(MazeBoard mazeBoard)
        {
            var letters = new StringBuilder();
            var characterPath = new StringBuilder();

            var currentDirection = MoveDirection.None;

            while (true)
            {
                var currentElement = mazeBoard.CurrentElement;
                characterPath.Append(currentElement);

                if (currentElement == Constants.EndingPositionMark)
                    break;

                if (char.IsLetter(currentElement) && !mazeBoard.CurrentPositionVisited)
                    letters.Append(currentElement);

                currentDirection = NextMoveDirection(currentDirection, mazeBoard);

                if (currentDirection == MoveDirection.None)
                    throw new InvalidMazePathException(characterPath.ToString());

                mazeBoard.Move(currentDirection);
            }

            return new MazeSolution(letters.ToString(), characterPath.ToString());
        }

        private static MoveDirection NextMoveDirection(MoveDirection currentDirection, MazeBoard mazeBoard)
        {
            var direction = currentDirection;

            if (currentDirection == MoveDirection.None)
            {
                direction = DetermineDirection(MoveDirection.Up, MoveDirection.Right, mazeBoard);
                direction = DetermineDirection(direction, MoveDirection.Down, mazeBoard);
                direction = DetermineDirection(direction, MoveDirection.Left, mazeBoard);
            }
            else if (!mazeBoard.CanMove(currentDirection))
            {
                if (currentDirection == MoveDirection.Up || currentDirection == MoveDirection.Down)
                    direction = DetermineDirection(MoveDirection.Left, MoveDirection.Right, mazeBoard);
                else if (currentDirection == MoveDirection.Left || currentDirection == MoveDirection.Right)
                    direction = DetermineDirection(MoveDirection.Up, MoveDirection.Down, mazeBoard);
            }

            return direction;
        }

        private static MoveDirection DetermineDirection(MoveDirection first, MoveDirection second, MazeBoard mazeBoard)
        {
            var canGoFirst = first != MoveDirection.None && mazeBoard.CanMove(first);
            var canGoSecond = second != MoveDirection.None && mazeBoard.CanMove(second);

            var isFirstDirectionElement = InDirection(first, mazeBoard.PeekElement(first));
            var isSecondDirectionElement = InDirection(second, mazeBoard.PeekElement(second));

            //both directions shouldn't be valid
            if (canGoFirst && isFirstDirectionElement && canGoSecond && isSecondDirectionElement)
                return MoveDirection.None;
            else if (canGoFirst && isFirstDirectionElement)
                return first;
            else if (canGoSecond && isSecondDirectionElement)
                return second;
            //there is no direction element in any direction, in that case one direction should be empty
            else if (canGoFirst && canGoSecond)
                return MoveDirection.None;
            else if (canGoFirst)
                return first;
            else if (canGoSecond)
                return second;
            //we are not able to move in any of the offered directions
            else
                return MoveDirection.None;
        }

        private static bool InDirection(MoveDirection currentDirection, char? nextElement)
        {
            if (!nextElement.HasValue)
                return false;

            if (currentDirection == MoveDirection.Left || currentDirection == MoveDirection.Right)
                return nextElement == Constants.DirectionLeftRightMark;
            else if (currentDirection == MoveDirection.Up || currentDirection == MoveDirection.Down)
                return nextElement == Constants.DirectionUpDownMark;
            else
                return false;
        }
    }
}
