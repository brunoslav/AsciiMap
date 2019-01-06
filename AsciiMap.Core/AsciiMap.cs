using System.Text;
using AsciiMap.Core.Exceptions;

namespace AsciiMap.Core
{
    public class AsciiMap
    {
        public static AsciiMapSolution FindPath(string asciiMap)
        {
            return FindPath(AsciiMapBoardFactory.CreateBoard(asciiMap));
        }

        private static AsciiMapSolution FindPath(AsciiMapBoard mapBoard)
        {
            var letters = new StringBuilder();
            var characterPath = new StringBuilder();

            var currentDirection = MoveDirection.None;

            while (true)
            {
                var currentElement = mapBoard.CurrentElement;
                characterPath.Append(currentElement);

                if (currentElement == Constants.EndingPositionMark)
                    break;

                if (char.IsLetter(currentElement) && !mapBoard.CurrentPositionVisited)
                    letters.Append(currentElement);

                currentDirection = NextMoveDirection(currentDirection, mapBoard, characterPath);

                if (currentDirection == MoveDirection.None)
                    throw new InvalidMapPathException(characterPath.ToString());

                mapBoard.Move(currentDirection);
            }

            return new AsciiMapSolution(letters.ToString(), characterPath.ToString());
        }

        private static MoveDirection NextMoveDirection(MoveDirection currentDirection, AsciiMapBoard mapBoard, StringBuilder characterPath)
        {
            var direction = currentDirection;

            if (currentDirection == MoveDirection.None)
            {
                direction = DetermineDirection(MoveDirection.Up, MoveDirection.Right, mapBoard, characterPath);
                direction = DetermineDirection(direction, MoveDirection.Down, mapBoard, characterPath);
                direction = DetermineDirection(direction, MoveDirection.Left, mapBoard, characterPath);
            }
            else if (!mapBoard.CanMove(currentDirection))
            {
                if (currentDirection == MoveDirection.Up || currentDirection == MoveDirection.Down)
                    direction = DetermineDirection(MoveDirection.Left, MoveDirection.Right, mapBoard, characterPath);
                else if (currentDirection == MoveDirection.Left || currentDirection == MoveDirection.Right)
                    direction = DetermineDirection(MoveDirection.Up, MoveDirection.Down, mapBoard, characterPath);
            }

            return direction;
        }

        private static MoveDirection DetermineDirection(MoveDirection first, MoveDirection second, AsciiMapBoard mapBoard, StringBuilder characterPath)
        {
            var canGoFirst = first != MoveDirection.None && mapBoard.CanMove(first);
            var canGoSecond = second != MoveDirection.None && mapBoard.CanMove(second);

            var isFirstDirectionElement = InDirection(first, mapBoard.PeekElement(first));
            var isSecondDirectionElement = InDirection(second, mapBoard.PeekElement(second));

            //both directions shouldn't be valid
            if (canGoFirst && isFirstDirectionElement && canGoSecond && isSecondDirectionElement)
                throw new InvalidMapPathException(characterPath.ToString());
            else if (canGoFirst && isFirstDirectionElement)
                return first;
            else if (canGoSecond && isSecondDirectionElement)
                return second;
            //there is no direction element in any direction, in that case one direction should be empty
            else if (canGoFirst && canGoSecond)
                throw new InvalidMapPathException(characterPath.ToString());
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
