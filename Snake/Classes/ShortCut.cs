using static Snake.Classes.GameLogic;
using static Snake.Classes.HamiltonianCycle;

namespace Snake.Classes
{
    public class ShortCut
    {
        // NOTICE:
        // The here used terms "above", "left", "right" and "under/below" are "seen visually".
        // Example:
        //   Snakehead.Y should be equal to  5 
        // while apple.Y should be equal to 10
        // a) seen visually:
        //    The snakehaed is ABOVE the apple.
        // b) seen mathimatically:
        //    For sure snakehead.Y is less than apple.Y. So it is "under/ lower". 
        //
        // Keep this in mind when reading the following comments.

        public void CalcShortCutForCase1And3(ReturnData returnDatas, HamiltonianCycleData HamiltonianCycleData)
        {
            var snakesHeadPosition = returnDatas.SnakePositions[0];
            var applePosition = returnDatas.ApplePosition;

            if (snakesHeadPosition.X < 1 || snakesHeadPosition.Y < 1)
            {
                // No need to calculate a shortcut because snakes head is already in
                // .X == 0  -> ... the column 0
                // .Y == 0  -> ... the first row that leads the snake to column 0
                return;
            }

            if (snakesHeadPosition.Y == applePosition.Y
                && (
                        (snakesHeadPosition.X < applePosition.X && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Right)
                        ||
                        (snakesHeadPosition.X > applePosition.X && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Left)
                    )
                )
            {
                // No need to calculate a shortcut because snakes head is 
                // already in the row that contains the apple
                // and
                // (    snakes head is left  from the apple and the snake movedirection is right towards the apple
                //      or
                //      snakes head is right from the apple and the snake movedirection is left towards the apple
                // )
                return;
            }

            // Check whether all snake parts are in the Hamiltonia Cycle. Leave if not.
            if (!AreAllSnakePartsInTheHamiltonianCycle(returnDatas, HamiltonianCycleData))
            {
                return;
            }
            
            if (snakesHeadPosition.Y > applePosition.Y)
            {
                // The snake is below the apple.
                // The shortcut path to be calculated here should lead the snake one row below the row that contains the apple.
                for (int y = snakesHeadPosition.Y - 1; applePosition.Y < y; y--)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                // The short cut path ends here exact below the row with the apple. Check whether the apple is up next to the snakehead.
                if (snakesHeadPosition.X == applePosition.X)
                {
                    // The apple is up next to the snakehead.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                else if (snakesHeadPosition.X < applePosition.X && !Common.IsValueEven(applePosition.Y))
                {
                    // Snakes head is left from the apple
                    // AND
                    // the apple is in a row with an odd number like 1, 3, 5, 7, ... 
                    // The direction in "odd rows" in right, so we can additionally lead the snake into the row with the apple
                    // because the then following non-shortcut path (aka "normal" Hamiltonian Cylce) is going to lead the snake into the apple.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                else if (snakesHeadPosition.X > applePosition.X && Common.IsValueEven(applePosition.Y))
                {
                    // Snakes head is right from the apple ... -> See previous comment.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
            }
            else 
            {
                // The snake is above the apple.
                // The snake should be lead to column 0.
                if (!Common.IsValueEven(snakesHeadPosition.Y))
                {
                    // Do not use:
                    //      if (HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Right)
                    // because the snake head can be in an odd row while its move direction is ".Up"!
                    // Do also not use:
                    //      if (HamiltonianCycleData.Data.MoveDirections[2, snakesHeadPosition.Y] == MoveDirection.Right)
                    // becasue the minimum square size is 2-by-2.
                    // But this would work:
                    //      if (HamiltonianCycleData.Data.MoveDirections[1, snakesHeadPosition.Y] == MoveDirection.Right)
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }

                for (int x = snakesHeadPosition.X; x > 0; x--)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Left);
                }
            }
        }

        public void CalcShortCutForCase2(ReturnData returnDatas, HamiltonianCycleData HamiltonianCycleData)
        {
            var snakesHeadPosition = returnDatas.SnakePositions[0];
            var applePosition = returnDatas.ApplePosition;

            if (snakesHeadPosition.X < 1 || snakesHeadPosition.Y < 1)
            {
                // No need to calculate a shortcut because snakes head is already in
                // .X == 0  -> ... the column 0
                // .Y == 0  -> ... the first row that leads the snake to column 0
                return;
            }

            if (snakesHeadPosition.X == applePosition.X
                && (
                        (snakesHeadPosition.Y < applePosition.Y && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Down)
                        ||
                        (snakesHeadPosition.Y > applePosition.Y && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Up)
                    )
                )
            {
                // No need to calculate a shortcut because snakes head is 
                // already in the column that contains the apple
                // and
                // (    snakes head is above the apple and the snake movedirection is down towards the apple
                //      or
                //      snakes head is under the apple and the snake movedirection is up   towards the apple
                // )
                return;
            }

            if (!AreAllSnakePartsInTheHamiltonianCycle(returnDatas, HamiltonianCycleData))
            { 
                return;
            }

            if (snakesHeadPosition.X < applePosition.X)
            {
                // The snake is to the left of the apple.
                // The shortcut path to be calculated here should lead the snake one column before the column that contains the apple.
                for (int x = snakesHeadPosition.X; x < applePosition.X - 1; x++)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                }
                // The short cut path ends here exact one column before the row with the apple. Check whether the apple is right next to the snake head.
                if (snakesHeadPosition.Y == applePosition.Y)
                {
                    // The apple is right next to the snake head.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                }
                else if (snakesHeadPosition.Y < applePosition.Y && Common.IsValueEven(applePosition.X))
                {
                    // Snakes head is above the apple
                    // AND
                    // the apple is in a column with an even number like 2, 4, 6, 8, ... 
                    // The direction in "even columns" in down, so we can additionally lead the snake into this apple containing column
                    // because the then following non-shortcut path (aka "normal" Hamiltonian Cylce) is going to lead the snake into the apple.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                }
                else if (snakesHeadPosition.Y > applePosition.Y && !Common.IsValueEven(applePosition.X))
                {
                    // Snakes head is below the apple ... -> See previous comment.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                } else if(applePosition.Y == 0)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                    // snake head is no in the same column as the apple.
                    // Lead the snake up into the apple.
                    for (int y = snakesHeadPosition.Y; y > 0; y--)
                    {
                        returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                    }
                }
            }
            else
            {
                // The snake is to the right of the apple.
                // The snake should be lead to row 0.
                if (Common.IsValueEven(snakesHeadPosition.X))
                {
                    // Snake head is a "even column". 
                    // The move direction of "even columns" is down, so go one step to the right, to a "odd column.
                    // The move direction of "odd  columns" is up.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Right);
                }

                for (int y = snakesHeadPosition.Y; y > 0; y--)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
            }
        }
        
        private bool AreAllSnakePartsInTheHamiltonianCycle(ReturnData returnDatas, HamiltonianCycleData HamiltonianCycleData)
        {
            // Check whether all snake parts are in the Hamiltonia Cycle.
            for (int i = 0; i < returnDatas.SnakePositions.Count - 1; i++)
            {
                var pointA = returnDatas.SnakePositions[i];
                var pointB = returnDatas.SnakePositions[i + 1];

                if (HamiltonianCycleData.Data.PointToSequenceNumber[pointA.X, pointA.Y] == 0)
                {
                    if (HamiltonianCycleData.Data.PointToSequenceNumber[pointB.X, pointB.Y] != HamiltonianCycleData.Data.PointToSequenceNumber.Length - 1)
                    {
                        return false;
                    }

                }
                else if (HamiltonianCycleData.Data.PointToSequenceNumber[pointA.X, pointA.Y] - 1 != HamiltonianCycleData.Data.PointToSequenceNumber[pointB.X, pointB.Y])
                {
                    return false;
                }
            }
            // All snake parts are in the Hamiltonian Cycle.
            return true;
        }
    }
}
