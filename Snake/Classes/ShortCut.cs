using System;
using System.Collections.Generic;
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

            if (snakesHeadPosition.X > 0 && applePosition.X > 0 && snakesHeadPosition.Y > applePosition.Y)
            {
            
                returnDatas.ShotCutMoveDirections = new List<MoveDirection>();
                //var stepToApple = snakesHeadPosition.Y - applePosition.Y - 1; // init 
                for (int y = snakesHeadPosition.Y - 1; applePosition.Y < y; y--)
                {
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                // shortCutPath ends here exact the row with the apple.
                if (snakesHeadPosition.X == applePosition.X)
                {
                    // snake head is directly unter the apple.
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }
                else if (snakesHeadPosition.X < applePosition.X && HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Right)
                {
                    // Snake head is left from the apple
                    // AND
                    // snake head can be set in the row with the apple
                    // because the then following non-shortcut path (aka "normal" Hamiltonian Cylce) is going to "guids" the snake to the apple.
                    //
                    // Note that the movement direction is determind via 
                    // "... [snakesHeadPosition.X, applePosition.Y]" 
                    // because the apple postion can be a position with the move direction is equal ".Up"!
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                    //stepToApple++;
                }
                else if (snakesHeadPosition.X > applePosition.X && HamiltonianCycleData.MoveDirections[snakesHeadPosition.X, applePosition.Y] == MoveDirection.Left)
                {
                    // Snake head is left from the apple 
                    // otherwise as before ...
                    returnDatas.ShotCutMoveDirections.Add(MoveDirection.Up);
                }


                //if (snakesHeadPosition.Y > applePosition.Y)
                //{
                //    // snake head is mor e than one row und the row whicht includes the apple.
                //    nextMoveDirection = MoveDirection.Up;
                //}





                var xCount = returnDatas.SnakePositions.Count(x => x.X > 0 && x.Y >= applePosition.Y && x.Y < snakesHeadPosition.Y);
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
