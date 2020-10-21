using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.Classes
{
    public class HamiltonianCycle
    {
        public enum MoveDirection
        {
            AAAAA = 0,          // ToDo: Uwe: "AAAAA" and "CCCCC" used to visual see during developemnt whether all list items got a well assigned enum value or not.
            Up,
            Left,
            Down,
            Right,
            CCCCC
        }

        public MoveDirection[,] GetHamiltonianCycle(int playgroundWidth, int playgroundHeight)
        {
            if (playgroundWidth < 2)
            {
                throw new Exception("The 'PlaygroundWidth' value must be equal or greater than 2!");
            }
            if (playgroundHeight < 2)
            {
                throw new Exception("The 'PlaygroundHeight' value must be equal or greater than 2!");
            }
            if (playgroundWidth > 40)
            {
                throw new Exception("The 'PlaygroundWidth' value must be less than or equal to 40 for displaying reasons only!");
            }
            if (playgroundHeight > 40)
            {
                throw new Exception("The 'PlaygroundHeight' value must be less than or equal to 40 for displaying reasons only!");
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

            if (Common.IsValueEven(playgroundHeight))
            {
                return Case_1_and_3(playgroundWidth, playgroundHeight);
            }
            else if (Common.IsValueEven(playgroundWidth))
            {
                return Case_2(playgroundWidth, playgroundHeight);
            }
            
            // Case 4
            throw new Exception("Both 'PlaygroundWidth' and 'PlaygroundHeight' can not be odd at the same time!");
        }

        private MoveDirection[,] Case_1_and_3(int width, int height)
        {
            var ret = new MoveDirection[width, height];

            // 1) Column 0
            for (int y = 0; y < height - 1; y++)
            {
                ret[0, y] = MoveDirection.Down;
            }
            ret[0, height - 1] = MoveDirection.Right;

            // 2) Column 1
            for (int y = 1; y < height - 1; y++)
            {
                if (width == 2)
                {
                    ret[1, y] = MoveDirection.Up;
                }
                else
                {
                    if (Common.IsValueEven(y))
                    {
                        ret[1, y] = MoveDirection.Up;
                    }
                    else
                    {
                        ret[1, y] = MoveDirection.Right;
                    }
                }
            }

            // 3) Last row
            for (int x = 1; x < width - 1; x++)
            {
                ret[x, height - 1] = MoveDirection.Right;
            }
            ret[width - 1, height - 1] = MoveDirection.Up;

            // 4) First row (special because of ret [1,0] which is ".Left" and not ".Up".
            for (int x = 1; x < width; x++)
            {
                ret[x, 0] = MoveDirection.Left;
            }

            if (width > 2)
            {
                // 5) Last column 
                for (int y = 0; y < height - 1; y++)
                {
                    if (Common.IsValueEven(y))
                    {
                        // y = 2
                        ret[width - 1, y] = MoveDirection.Left;
                    }
                    else
                    {
                        // y = 1
                        ret[width - 1, y] = MoveDirection.Up;
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
                            ret[x, y] = MoveDirection.Left;
                        }
                        else
                        {
                            // y = 1
                            ret[x, y] = MoveDirection.Right;
                        }
                    }
                }
            }
            ShowHamiltonianCycle(width, height, ret);
            return ret;
        }

        private MoveDirection[,] Case_2(int width, int height)
        {
            var ret = new MoveDirection[width, height];
            // ToDo: Uwe: Implement it!
            return ret;
        }

        private void ShowHamiltonianCycle(int width, int height, MoveDirection[,] hamiltonianCycle)
        {
            Console.WriteLine($"Testdata for ({width}, {height}):");
            for (int hamHeight = 0; hamHeight < height; hamHeight++)
            {
                var oneRow = $"row {hamHeight}: ";
                for (int hamWidth = 0; hamWidth < width; hamWidth++)
                {
                    oneRow += $" | ({hamWidth}, {hamHeight}) = {hamiltonianCycle[hamWidth, hamHeight],-5} ";
                }
                Console.WriteLine(oneRow);
            }
            Console.WriteLine("------------------------------------------------------------------------------");
        }

        public void Test()
        {
            var TestData = new List<Size>
            {
                // Data for case 1 (= x is even and y is even) 
                //      and case 4 (= x is odd  and y is even)
                new Size(2, 2), new Size(2, 4), new Size(2, 6),
                new Size(3, 2), new Size(3, 4), new Size(3, 6),
                new Size(4, 2), new Size(4, 4), new Size(4, 6),

                //// Data for case 2 ( = x is even and y is odd)
                //new Size(2, 3), new Size(2, 5), new Size(2, 7),
                //new Size(4, 3), new Size(4, 5), new Size(4, 7),
                //new Size(6, 3), new Size(6, 5), new Size(6, 7),
            };

            foreach (var item in TestData)
            {
                var hamiltonianCycle = GetHamiltonianCycle(item.Width, item.Height);
                ShowHamiltonianCycle(item.Width, item.Height, hamiltonianCycle);
            }
        }
    }
}
