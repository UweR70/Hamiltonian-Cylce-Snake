using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.Classes
{
    public class HamiltonianCycle
    {
        public enum MoveDirection
        {
            Init = 0,   // Init is used to visualy see during developemnt whether all list items got a well assigned enum value or not.
            Up,
            Left,
            Down,
            Right
        }

        public enum Case
        {
            Case_1_And_3 = 0,
            Case_2
        }

        public class HamiltonianCycleData
        {
            public Data Data;
            public Case Case;
        }

        public class Data
        {
            // Example: width = 6, height = 6 -> even / even -> is case 1: 
            public MoveDirection[,] MoveDirections; //        MoveDirections[2, 3] = .Right
            public int[,] PointToSequenceNumber;    // PointToSequenceNumber[2, 3] = 19            (20th Element because zero base counted)    
            public Point[] SequenceNumberToPoint;   // SequenceNumberToPoint[19]   = Point(2, 3)
        }

        public HamiltonianCycleData GetHamiltonianCycleData(int playFieldWidth, int playFieldHeight)
        {
            if (playFieldWidth < 2)
            {
                throw new Exception("The 'PlayFieldWidth' value must be equal or greater than 2!");
            }
            if (playFieldHeight < 2)
            {
                throw new Exception("The 'PlayFieldHeight' value must be equal or greater than 2!");
            }
            if (playFieldWidth > 40)
            {
                throw new Exception("The 'PlayFieldWidth' value must be less than or equal to 40 for displaying reasons only!");
            }
            if (playFieldHeight > 40)
            {
                throw new Exception("The 'PlayFieldHeight' value must be less than or equal to 40 for displaying reasons only!");
            }

            // |------|------|------|
            // | Case | x    | y    |
            // |------|------|------|
            // | 1    | even | even |
            // | 2    | even | odd  |
            // |------|------|------|
            // | 3    | odd  | even |
            // | 4    | odd  | odd  | -> No Hamiltonian cycle possible 
            // |------|------|------|

            if (Common.IsValueEven(playFieldHeight))
            {
                return new HamiltonianCycleData { Data = Case_1_and_3(playFieldWidth, playFieldHeight), Case = Case.Case_1_And_3 };
            }
            else if (Common.IsValueEven(playFieldWidth))
            {
                return new HamiltonianCycleData { Data = Case_2(playFieldWidth, playFieldHeight), Case = Case.Case_2 };
            }

            // Case 4
            throw new Exception("Both 'PlayFieldWidth' and 'PlayFieldHeight' can not be odd at the same time!");
        }

        private Data Case_1_and_3(int width, int height)
        {
            var ret = new Data() {
                MoveDirections = new MoveDirection[width, height],
                PointToSequenceNumber = new int[width, height],
                SequenceNumberToPoint = new Point[width * height]
            };

            // 1) Column 0
            for (int y = 0; y < height - 1; y++)
            {
                ret.MoveDirections[0, y] = MoveDirection.Down;
            }
            ret.MoveDirections[0, height - 1] = MoveDirection.Right;

            // 2) Column 1
            for (int y = 1; y < height - 1; y++)
            {
                if (width == 2)
                {
                    ret.MoveDirections[1, y] = MoveDirection.Up;
                }
                else
                {
                    if (Common.IsValueEven(y))
                    {
                        ret.MoveDirections[1, y] = MoveDirection.Up;
                    }
                    else
                    {
                        ret.MoveDirections[1, y] = MoveDirection.Right;
                    }
                }
            }

            // 3) Last row
            for (int x = 1; x < width - 1; x++)
            {
                ret.MoveDirections[x, height - 1] = MoveDirection.Right;
            }
            ret.MoveDirections[width - 1, height - 1] = MoveDirection.Up;

            // 4) First row (special because of ret [1,0] which is ".Left" and not ".Up".
            for (int x = 1; x < width; x++)
            {
                ret.MoveDirections[x, 0] = MoveDirection.Left;
            }

            if (width > 2)
            {
                // 5) Last column 
                for (int y = 0; y < height - 1; y++)
                {
                    if (Common.IsValueEven(y))
                    {
                        // y = 2
                        ret.MoveDirections[width - 1, y] = MoveDirection.Left;
                    }
                    else
                    {
                        // y = 1
                        ret.MoveDirections[width - 1, y] = MoveDirection.Up;
                    }
                }

                // 6) Pattern
                for (int y = 1; y < height - 1; y++)    // width = 6, height = 4 ->  y:= 1, 2     (4-1 = 3 -> ... < 3 -> max y value is 2)       
                {
                    for (int x = 2; x < width - 1; x++) //                           x:= 2, 3, 4  (6-1 = 5 -> ... < 5 -> max x value is 4)
                    {
                        if (Common.IsValueEven(y))
                        {
                            // y = 2
                            ret.MoveDirections[x, y] = MoveDirection.Left;
                        }
                        else
                        {
                            // y = 1
                            ret.MoveDirections[x, y] = MoveDirection.Right;
                        }
                    }
                }
            }
            GenerateSequence(width, height, ret);
            return ret;
        }

        private Data Case_2(int width, int height)
        {
            var ret = new Data()
            {
                MoveDirections = new MoveDirection[width, height],
                PointToSequenceNumber = new int[width, height],
                SequenceNumberToPoint = new Point[width * height]
            };

            // 1) Column 0
            for (int y = 0; y < height - 1; y++)
            {
                ret.MoveDirections[0, y] = MoveDirection.Down;
            }
            ret.MoveDirections[0, height - 1] = MoveDirection.Right;

            // 2) Last row
            for (int x = 1; x < width - 1; x++)
            {
                if (Common.IsValueEven(x))
                {
                    ret.MoveDirections[x, height - 1] = MoveDirection.Right;
                }
                else
                {
                    ret.MoveDirections[x, height - 1] = MoveDirection.Up;
                }
            }

            // 3) Last column 
            ret.MoveDirections[width - 1, 0] = MoveDirection.Left;
            for (int y = 1; y < height; y++)
            {
                ret.MoveDirections[width - 1, y] = MoveDirection.Up;
            }

            // 4) first two rows (y =0  and y = 1)
            for (int x = 1; x < width - 1; x++)
            {
                ret.MoveDirections[x, 0] = MoveDirection.Left;
                if (Common.IsValueEven(x))
                {
                    ret.MoveDirections[x, 1] = MoveDirection.Down;
                }
                else
                {
                    ret.MoveDirections[x, 1] = MoveDirection.Right;
                }
            }

            // 5) Two "inner" rows
            //      height   (height - 2) / 2
            //        2         0
            //        4         1
            //        6         2
            //        8         3
            //              ...     ...
            var nTimes = (height - 2) / 2;
            for (int n = 0; n < nTimes; n++)
            {
                for (int x = 1; x < width - 1; x += 2)
                {
                    for (int y = 2; y < height - 1; y += 2)
                    {
                        ret.MoveDirections[x, y] = MoveDirection.Up;
                        ret.MoveDirections[x + 1, y] = MoveDirection.Down;

                        ret.MoveDirections[x, y + 1] = MoveDirection.Up;
                        ret.MoveDirections[x + 1, y + 1] = MoveDirection.Down;
                    }
                }
            }
            GenerateSequence(width, height, ret);
            
            return ret;
        }

        private void GenerateSequence(int width, int height, Data hamiltonianCycle)
        {
            var index = -1;
            var count = width * height;
            var currentPosition = new Point(0, 0);
            hamiltonianCycle.PointToSequenceNumber[0, 0] = ++index;
            hamiltonianCycle.SequenceNumberToPoint[0] = currentPosition;
            do
            {
                switch (hamiltonianCycle.MoveDirections[currentPosition.X, currentPosition.Y])
                {
                    case HamiltonianCycle.MoveDirection.Up:
                        currentPosition.Y--;
                        
                        break;
                    case HamiltonianCycle.MoveDirection.Left:
                        currentPosition.X--;
                        break;
                    case HamiltonianCycle.MoveDirection.Down:
                        currentPosition.Y++;
                        break;
                    case HamiltonianCycle.MoveDirection.Right:
                        currentPosition.X++;
                        break;
                }
                index++;
                hamiltonianCycle.PointToSequenceNumber[currentPosition.X, currentPosition.Y] = index;
                hamiltonianCycle.SequenceNumberToPoint[index] = new Point(currentPosition.X, currentPosition.Y);
            } while (index < count - 1);

            ShowHamiltonianCycle(width, height, hamiltonianCycle);
        }

        private void ShowHamiltonianCycle(int width, int height, Data hamiltonianCycle)
        {
            Console.WriteLine($"Testdata for ({width}, {height}):");

            Console.WriteLine("Part 1:");
            for (int hamHeight = 0; hamHeight < height; hamHeight++)
            {
                var oneRow = $"row {hamHeight}: ";
                for (int hamWidth = 0; hamWidth < width; hamWidth++)
                {
                    oneRow += $" | ({hamWidth}, {hamHeight}) = {hamiltonianCycle.PointToSequenceNumber[hamWidth, hamHeight],-4}";
                }
                Console.WriteLine(oneRow + " |");
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Part 2:");
            for (int hamHeight = 0; hamHeight < height; hamHeight++)
            {
                var oneRow = $"row {hamHeight}: ";
                for (int hamWidth = 0; hamWidth < width; hamWidth++)
                {
                    oneRow += $" | ({hamWidth}, {hamHeight}) = {hamiltonianCycle.MoveDirections[hamWidth, hamHeight], -5} ";
                }
                Console.WriteLine(oneRow + " |");
            }

            Console.WriteLine("------------------------------------------------------------------------------");
        } 

        public void Test()
        {
            var TestData = new List<Size>
            {
                //// Data for case 1 (= x is even and y is even) 
                ////      and case 3 (= x is odd  and y is even)
                //new Size(2, 2), new Size(2, 4), new Size(2, 6),
                //new Size(3, 2), new Size(3, 4), new Size(3, 6),
                //new Size(4, 2), new Size(4, 4), new Size(4, 6),

                // Data for case 2 ( = x is even and y is odd)
                new Size(2, 3), new Size(2, 5), new Size(2, 7),
                new Size(4, 3), new Size(4, 5), new Size(4, 7),
                new Size(6, 3), new Size(6, 5), new Size(6, 7),
            };

            foreach (var item in TestData)
            {
                var hamiltonianCycle = GetHamiltonianCycleData(item.Width, item.Height);
            }
        }
    }
}
