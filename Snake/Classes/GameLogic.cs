using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Snake.Classes.HamiltonianCycle;

namespace Snake.Classes
{
    public class GameLogic
    {
        private int _PlayFieldWidth;
        private int _PlayFieldHeight;

        public readonly HamiltonianCycle HamiltonianCycle;
        public readonly HamiltonianCycleData HamiltonianCycleData;

        public ShortCut ShortCut;

        public class ReturnData
        {
            public List<Point> ResetThesePositions; // "old" apple and snake positions
            public List<MoveDirection> ShotCutMoveDirections;
            public List<Point> SnakePositions;
            public Point ApplePosition;
            public bool DrawApple;
            public bool IslogicalEndReached;
        }
        public ReturnData ReturnDatas;
        
        public GameLogic(int playFieldWidth, int playFieldHeight)
        {
            _PlayFieldWidth = playFieldWidth;
            _PlayFieldHeight = playFieldHeight;

            HamiltonianCycle = new HamiltonianCycle();
            HamiltonianCycleData = HamiltonianCycle.GetHamiltonianCycleData(_PlayFieldWidth, _PlayFieldHeight);

            ShortCut = new ShortCut();
            
            ReturnDatas = new ReturnData
            {
                ShotCutMoveDirections = new List<MoveDirection>(),
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

        private void SetTestData()
        {
            ReturnDatas.ResetThesePositions = new List<Point>();
            ReturnDatas.ApplePosition = new Point(5, 5);
            ReturnDatas.SnakePositions = new List<Point>() {
                new Point(4, 7),     // head
                new Point(4, 8),
                new Point(4, 9),

                new Point(3, 9),
                new Point(2, 9),

                //new Point(2, 8),
                //new Point(2, 7),

                //new Point(1, 7),
                //new Point(1, 8),

                new Point(1, 9),

                new Point(0, 9),
                new Point(0, 8),
                new Point(0, 7),
                new Point(0, 6),
                new Point(0, 5),
                new Point(0, 4),
                new Point(0, 3),
                new Point(0, 2),
                new Point(0, 1),
                new Point(0, 0),

                new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(7, 0), new Point(8, 0), new Point(9, 0),
                new Point(9, 1), new Point(8, 1), new Point(7, 1), new Point(6, 1), new Point(5, 1), new Point(4, 1), new Point(3, 1), new Point(2, 1), new Point(1, 1),
                new Point(1, 2), new Point(2, 2), new Point(3, 2), new Point(4, 2), new Point(5, 2), new Point(6, 2), new Point(7, 2), new Point(8, 2), new Point(9, 2),
                new Point(9, 3), new Point(8, 3), new Point(7, 3), new Point(6, 3), new Point(5, 3), new Point(4, 3), new Point(3, 3), new Point(2, 3), new Point(1, 3),
                new Point(1, 4), new Point(2, 4), new Point(3, 4), new Point(4, 4), new Point(5, 4), new Point(6, 4), new Point(7, 4), new Point(8, 4), 

                new Point(8, 5),
                new Point(8, 6),
                new Point(8, 7),
                new Point(8, 8),    // tail

                // new Point(7, 8),// tail
            };
        }

        public ReturnData Main()
        {
            if (ReturnDatas.ApplePosition.X == -1)
            {
                // Init situation: Set apple init position.
                ReturnDatas.ApplePosition = GetNewApplePosition();

                // SetTestData();    // ToDo: Remove this line of code afgter testing! 

                return ReturnDatas;
            }

            ReturnDatas.ResetThesePositions = new List<Point>();


            var nextMoveDirection = MoveDirection.Init;
            if (ReturnDatas.ShotCutMoveDirections == null || ReturnDatas.ShotCutMoveDirections.Count == 0)
            {
                switch (HamiltonianCycleData.Case)
                {
                    case Case.Case_1_And_3:
                        ShortCut.CalcShortCutForCase1And3(ReturnDatas, HamiltonianCycleData);
                        break;
                    case Case.Case_2:
                        ShortCut.CalcShortCutForCase2(ReturnDatas, HamiltonianCycleData);
                        break;
                    default:
                        throw new Exception("Unknown case!");
                }
            }
            // May be a shortcut path exists now.

            if (ReturnDatas.ShotCutMoveDirections.Count != 0)
            {
                // Minimum one short cut move direction exists. So it must be used!
                nextMoveDirection = ReturnDatas.ShotCutMoveDirections[0];
                ReturnDatas.ShotCutMoveDirections.RemoveAt(0);
            } else if (nextMoveDirection == MoveDirection.Init)
            {
                // Seems, that no short cut can be calculated, so the standard Hamiltonian Cycle must be used.
                var snakesHeadPosition = ReturnDatas.SnakePositions[0];
                nextMoveDirection = HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y];
            }
            MoveSnake(nextMoveDirection);

            return ReturnDatas;
        }
       
        private Point GetRandomPosition()
        {
            var r = new Random();
            return new Point
            { 
                X = r.Next(0, _PlayFieldWidth), 
                Y = r.Next(0, _PlayFieldHeight)
            };
        }

        private Point GetNewApplePosition()
        {
            var applePositionCandidate = new Point();
            int magicNumber = 70;

            if (ReturnDatas.SnakePositions.Count >= (_PlayFieldWidth * _PlayFieldHeight) * magicNumber / 100)
            {
                // The snake captures more than <magicNumber> percent of the playField.
                // In this case makes it sense to store all positions where an apple can be set
                // and get then by random a positon out of these positions.
                var freePositions = new List<Point>();
                for (int w = 0; w < _PlayFieldWidth; w++)
                {
                    for (int h = 0; h < _PlayFieldHeight; h++)
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
                // But this happens only in case there is in the playField minimum one positions free (to set a new apple). 
                // Otherwise is the game over because no free place at all.
                if (ReturnDatas.SnakePositions.Count < _PlayFieldWidth * _PlayFieldHeight)
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
