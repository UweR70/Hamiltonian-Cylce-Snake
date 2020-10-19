using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake.Classes
{
    public class CoreLogic
    {
        public const int PlaygroundWidth = 10;
        public const int PlaygroundHeight = 10;

        public enum MoveDirection
        {
            Up = 0,
            Left, 
            Down, 
            Right
        }

        public class ReturnData
        {
            public List<Point> ResetThesePositions; // "old" apple and snake positions
            public List<Point> SnakePositions;
            public Point ApplePosition;
            public bool DrawApple;
            public bool IslogicalEndReached;
        }
        public ReturnData ReturnDatas;
        
        public CoreLogic()
        {
            ReturnDatas = new ReturnData
            {
                ResetThesePositions = new List<Point>(),
                SnakePositions = new List<Point>
                {
                    // new Point(0,0)  
                    GetRandomPosition()
                },
                ApplePosition = new Point(-1, -1),   // Init values
                DrawApple = true,
                IslogicalEndReached = false
            };
        }

        public ReturnData Main()
        {
            if (ReturnDatas.ApplePosition.X == -1)
            {
                // Init situation: Set init positions for the apple.
                ReturnDatas.ApplePosition = GetNewApplePosition();
                return ReturnDatas;
            }

            ReturnDatas.ResetThesePositions = new List<Point>();

            if (PlaygroundWidth >= PlaygroundHeight)
            { 
                // Case 1:
                if (ReturnDatas.SnakePositions[0].X == 0)
                {
                    if (ReturnDatas.SnakePositions[0].Y < PlaygroundHeight - 1)
                    { 
                        MoveSnake(MoveDirection.Down);
                    } else
                    { 
                        MoveSnake(MoveDirection.Right); 
                    }
                }
                else
                {
                    if (IsValueEven(ReturnDatas.SnakePositions[0].Y))
                    {
                        // row = 0, 2, 4, 6, ...
                        if (ReturnDatas.SnakePositions[0].X > 1 ) 
                        {
                            MoveSnake(MoveDirection.Left);
                        } 
                        else
                        {
                            if (ReturnDatas.SnakePositions[0].Y > 0)
                            {
                                MoveSnake(MoveDirection.Up);
                            }
                            else
                            {
                                MoveSnake(MoveDirection.Left);
                            }
                        }
                    }
                    else
                    {
                        // row = 1, 3, 5, 7, ...
                        if (ReturnDatas.SnakePositions[0].X < PlaygroundWidth - 1)   // "... - 1 " becasue its zero base counted!
                        {
                            MoveSnake(MoveDirection.Right);
                        } else
                        {
                            MoveSnake(MoveDirection.Up);
                        }
                    }
                }
            }
            else
            {

            }
            return ReturnDatas;
        }

        public static bool IsValueEven(int value)
        {
            // "true" for 0, 2, 4 ,6, ...
            return value % 2 == 0;
        }

        private Point GetRandomPosition()
        {
            var r = new Random();
            return new Point
            { 
                X = r.Next(0, PlaygroundWidth), 
                Y = r.Next(0, PlaygroundHeight)
            };
        }

        private Point GetNewApplePosition()
        {
            var applePositionCandidate = new Point();
            int magicNumber = 70;

            // TEST_FillSnake(magicNumber);    // #########################################################################################

            if (ReturnDatas.SnakePositions.Count >= (PlaygroundWidth * PlaygroundHeight) * magicNumber / 100)
            {
                // The snake captures more than <magicNumber> percent of the playground.
                // In this case makes it sense to store all positions where an apple can be set
                // and get then by random a positon out of these positions.
                var freePositions = new List<Point>();
                for (int w = 0; w < PlaygroundWidth; w++)
                {
                    for (int h = 0; h < PlaygroundHeight; h++)
                    {
                        if (!ReturnDatas.SnakePositions.Any(x => x.X == w && x.Y == h))
                        {
                            freePositions.Add(new Point { X = w, Y = h });
                        }
                    }
                }

                var r = new Random();
                var index = r.Next(0, freePositions.Count);
                applePositionCandidate = freePositions[index];
            }
            else
            {
                do
                {
                    applePositionCandidate = GetRandomPosition();

                } while (ReturnDatas.SnakePositions.Any(x => x.X == applePositionCandidate.X && x.Y == applePositionCandidate.Y));
            }
            return applePositionCandidate;
        }

        private void TEST_FillSnake(int fillPercentage)
        {
            ReturnDatas.SnakePositions = new List<Point>();
            long neededItems = PlaygroundWidth * PlaygroundHeight * fillPercentage / 100;
            neededItems++; // To be sure!

            var counter = 0;
            for (int w = 0; w < PlaygroundWidth; w++)
            {
                for (int h = 0; h < PlaygroundHeight; h++)
                {
                    ReturnDatas.SnakePositions.Add(new Point { X = w, Y = h });
                    if (++counter == neededItems)
                    { 
                        return; 
                    }
                }
            }
        }

        private void MoveSnake(MoveDirection moveDirection)
        {
            ReturnDatas.DrawApple = false;

            var headDelta = new Point(); 
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    headDelta.X = 0; headDelta.Y = -1;
                    break;
                case MoveDirection.Left:
                    headDelta.X = -1; headDelta.Y = 0;
                    break;
                case MoveDirection.Down:
                    headDelta.X = 0; headDelta.Y = 1;
                    break;
                case MoveDirection.Right:
                    headDelta.X = 1; headDelta.Y = 0;
                    break;
            }

            var newHeadPosition = new Point
            { 
                X = ReturnDatas.SnakePositions[0].X + headDelta.X, 
                Y = ReturnDatas.SnakePositions[0].Y + headDelta.Y 
            };
            ReturnDatas.SnakePositions.Insert(0, newHeadPosition);

            if (newHeadPosition.X != ReturnDatas.ApplePosition.X || newHeadPosition.Y != ReturnDatas.ApplePosition.Y)
            {
                // Snakes new head position is not equal with the apple position.
                // -> The snake moves forward. The apple was not hit. 
                // The tail of the snakes must also move. Snake length will not be increased 
                // First: One item added to the snake, then here: One item removed from the snake. Result: Snake length stays finally the same.
                var lastSnakePosition = ReturnDatas.SnakePositions[ReturnDatas.SnakePositions.Count - 1];
                ReturnDatas.ResetThesePositions.Add(lastSnakePosition);
                ReturnDatas.SnakePositions.RemoveAt(ReturnDatas.SnakePositions.Count - 1);
            }
            else
            {
                // The head/snake moves into the apple.
                // -> The tail will not be removed. 
                // First: One item added to the snake, then here: No item removed from the snake. Result: Snake length will increase by one.
                // But this happens only in case there is in the playground minimum one positions free (to set a new apple). 
                // Otherwise is the game over because no free place at all.
                if (ReturnDatas.SnakePositions.Count < PlaygroundWidth * PlaygroundHeight)
                {
                    ReturnDatas.ApplePosition = GetNewApplePosition();
                    ReturnDatas.DrawApple = true;
                }
                else
                {
                    ReturnDatas.IslogicalEndReached = true;
                }
            }
        }
    }
}
