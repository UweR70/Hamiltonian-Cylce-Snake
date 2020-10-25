using System;
using System.Linq;
using static Snake.Classes.GameLogic;
using static Snake.Classes.HamiltonianCycle;

namespace Snake.Classes
{
    public class ShortCut
    {
        public void CalcShortCutForCase1And3(ReturnData returnDatas, HamiltonianCycleData HamiltonianCycleData)
        {
            var snakesHeadPosition = returnDatas.SnakePositions[0];
            var applePosition = returnDatas.ApplePosition;

            if (snakesHeadPosition.X < 1 || snakesHeadPosition.Y < 1 || snakesHeadPosition.Y == applePosition.Y)
            {
                //             No need to calculate a shortcut because snakes head is already in
                // .X == 0  -> ... the column 0
                // .Y == 0  -> ... a row that leads the snake to column 0
                // .Y == .Y -> ... the row that contains the apple
                return;
            }

            // Separated because Linq is relatively slow.
            var countInColumnZero = returnDatas.SnakePositions.Count(x => x.X == 0);
            if (countInColumnZero > 0)
            {
                return;
            }

            var countBelowApplesRow = returnDatas.SnakePositions.Count(x => x.Y < returnDatas.ApplePosition.Y);
            var countAboveApplesRow = returnDatas.SnakePositions.Count(x => x.Y > returnDatas.ApplePosition.Y);

            if (countBelowApplesRow == 0)
            {
                // The snake is completely above the apple.
                // The shortcut path to be calculated here should lead the snake one row below or in the row that contains the apple.
                
                for (int y = snakesHeadPosition.Y - 1; applePosition.Y < y; y--)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                // The short cut path ends here exact the row with the apple. Check now whether snakes head can be lead additional in the row with the apple.
                if (snakesHeadPosition.X == applePosition.X)
                {
                    // Snakes head is directly unter the apple.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                else if (snakesHeadPosition.X < applePosition.X && HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Right)
                {
                    // Snakes head is left from the apple
                    // AND
                    // snakes head can be lead in the row with the apple
                    // because the then following non-shortcut path (aka "normal" Hamiltonian Cylce) is going to lead the snake into the apple.
                    //
                    // Note that the movement direction is determind via a combination of snake and apple position
                    // "... [snakesHeadPosition.X, applePosition.Y] ..." 
                    // because the apple position can be a position with a movedirection that is equal to ".Up".
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                else if (snakesHeadPosition.X > applePosition.X && HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Left)
                {
                    // Snakes head is left from the apple ... -> See previous comment.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
            }
            else if (countAboveApplesRow == 0)
                // No need to generate a short cut in case snakesHeadPosition.Y is equal to zero because the snake is already on its way to column 0
            {
                // The snake is completely below the apple.
                // The snake should be leaded to column 0.
                // if (!Common.IsValueEven(snakesHeadPosition.Y))
                if( HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Right)
                {
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

            throw new Exception("Implement it!"); // ToDo: Impelent it!
        }
    }
}
