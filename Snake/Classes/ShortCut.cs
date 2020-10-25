using System;
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

            if (snakesHeadPosition.X < 1 || snakesHeadPosition.Y < 1)
            {
                // No need to calculate a shortcut because snakes head is already in
                // .X == 0  -> ... the column 0
                // .Y == 0  -> ... the first row that leads the snake to column 0
                return;
            }

            if (snakesHeadPosition.Y == applePosition.Y
                && (
                        // 
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


            // Check whether all snake parts in the Hamiltonia Cycle. Leave if not.
            for (int i = 0; i < returnDatas.SnakePositions.Count - 1; i++)
            {
                var pointA = returnDatas.SnakePositions[i];
                var pointB = returnDatas.SnakePositions[i + 1];

                if (HamiltonianCycleData.Data.PointSequence[pointA.X, pointA.Y] == 0)
                {
                    if (HamiltonianCycleData.Data.PointSequence[pointB.X, pointB.Y] != HamiltonianCycleData.Data.PointSequence.Length - 1)
                    {
                        return;
                    }

                }
                else if (HamiltonianCycleData.Data.PointSequence[pointA.X, pointA.Y] - 1 != HamiltonianCycleData.Data.PointSequence[pointB.X, pointB.Y])
                {
                    return;
                }
            }
            // All snake parts are in the Hamiltonian Cycle.
            
            if (snakesHeadPosition.Y > applePosition.Y)
            {
                // The snake is above the apple.
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
                else if (snakesHeadPosition.X < applePosition.X && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Right)
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
                else if (snakesHeadPosition.X > applePosition.X && HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Left)
                {
                    // Snakes head is left from the apple ... -> See previous comment.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
            }
            else 
            {
                // The snake is below the apple.
                // The snake should be leaded to column 0.
                if (!Common.IsValueEven(snakesHeadPosition.Y))
                {
                    // Do not use:
                    //      if (HamiltonianCycleData.Data.MoveDirections[snakesHeadPosition.X, snakesHeadPosition.Y] == MoveDirection.Right)
                    // because the snake head can be in an odd row while it move direction is ".Up"!
                    // Do also not use:
                    //      if (HamiltonianCycleData.Data.MoveDirections[2, snakesHeadPosition.Y] == MoveDirection.Right)
                    // becasue the min square size is 2-by-2
                    // But this wpuld work:
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

            throw new Exception("Implement it!"); // ToDo: Impelent it!
        }
    }
}
