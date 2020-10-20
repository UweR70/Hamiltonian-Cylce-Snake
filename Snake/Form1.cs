using Snake.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private enum DrawItem
        {
            Playground = 0,
            Apple, 
            Snake
        }

        private CoreLogic CoreLogic;

        private bool IsAppRunning;
        private bool IsUserInterrupted;
        private bool IsHamiltonianCycleShown;

        private Graphics Graphics;

        private Pen PenPlayground;
        private Pen PenHamiltonianCycle;
        private Pen PenApple;
        private Pen PenSnakeHead;
        private Pen PenSnakeBody;
        private Pen PenSnakeTail;

        private SolidBrush SolidBrush;

        private Rectangle Square;

        private int SquareWidth;
        private int SquareHeight;
        private int SquareDeltaX;
        private int SquareDeltaY;

        private int DrawCounter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = Config.TitleAppNameAndVersion;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (IsAppRunning)
            {
                btn_Start.Text = "Start";
                IsAppRunning = false;

                IsUserInterrupted = true;
            }
            else
            {
                txtBox_Info.Text = string.Empty;

                btn_Start.Text = "Stop";
                IsAppRunning = true;

                IsHamiltonianCycleShown = false;
                IsUserInterrupted = false;
                
                CoreLogic = new CoreLogic();

                // ToDo: Uwe: The next four parameter should be a function of "CoreLogic.PlaygroundWidth" and "CoreLogic.PlaygroundHeight".
                SquareWidth = 20;
                SquareHeight = 20;
                SquareDeltaX = 2;
                SquareDeltaY = 2;

                Square = new Rectangle
                {
                    Width = SquareWidth,
                    Height = SquareHeight
                };

                Graphics = panel1.CreateGraphics();
                
                SolidBrush = new SolidBrush(Color.Empty);

                PenPlayground = new Pen(Color.Black);
                PenHamiltonianCycle = new Pen(Color.DarkKhaki);
                PenApple = new Pen(Color.Red);
                PenSnakeHead = new Pen(Color.Blue);
                PenSnakeBody = new Pen(Color.Green);
                PenSnakeTail = new Pen(Color.Yellow);

                DrawCounter = 0;

                ShowHamiltonianCycle();
                CoreLogicWrapper();
            }
        }

        private void ShowHamiltonianCycle() 
        {
            Log("The Hamiltonian Cycle will be displayed.");
            var taskA = Task.Factory.StartNew(() =>
            {
                var hamiltonianCycle = CoreLogic.GetHamiltonianCycle();

                InitPlayground(CoreLogic.PlaygroundWidth, CoreLogic.PlaygroundHeight);

                // Draw all at once
                // DrawPositions(hamiltonianCycle, DrawItem.Apple);
                // Draw cell by cell
                for (int i = 0; i < hamiltonianCycle.Count; i++)
                {
                    if (IsUserInterrupted)
                    {
                        break;
                    }
                    var point = ConvertArrayIndecesToGraphicalCoordiantes(hamiltonianCycle[i]);
                    DrawOneRectangle(point, PenHamiltonianCycle);
                    Thread.Sleep(20);
                }
            }).ContinueWith(result =>
            {
                // Controls are handled here to avoid a "cross-thread" error.
                if (!IsUserInterrupted)
                {
                    Log("Start main logic");
                }

                IsHamiltonianCycleShown = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        
        public void CoreLogicWrapper()
        {
            var taskA = Task.Factory.StartNew(() =>
            {
                while (!IsHamiltonianCycleShown && !IsUserInterrupted)
                {
                    Thread.Sleep(500);
                }
                
                if (IsUserInterrupted)
                {
                    return;
                }
                InitPlayground(CoreLogic.PlaygroundWidth, CoreLogic.PlaygroundHeight);

                var coreLogicReturns = new CoreLogic.ReturnData();
                do
                {
                    coreLogicReturns = CoreLogic.Main();
                    if (coreLogicReturns.ResetThesePositions != null && coreLogicReturns.ResetThesePositions.Count > 0)
                    {
                        DrawPositions(coreLogicReturns.ResetThesePositions, DrawItem.Playground);
                    }

                    if (coreLogicReturns.SnakePositions != null && coreLogicReturns.SnakePositions.Count > 0)
                    {
                        DrawPositions(coreLogicReturns.SnakePositions, DrawItem.Snake);
                    }

                    if (coreLogicReturns.ApplePosition != null && !coreLogicReturns.IslogicalEndReached)

                    {
                        var point = ConvertArrayIndecesToGraphicalCoordiantes(coreLogicReturns.ApplePosition);
                        DrawOneRectangle(point, PenApple);
                    }
                    //Thread.Sleep(50);
                } while (!coreLogicReturns.IslogicalEndReached && IsAppRunning);
            }).ContinueWith(result =>
            {
                // Controls are handled here to avoid a "cross-thread" error.

                if (IsUserInterrupted)
                {
                    if (!IsHamiltonianCycleShown)
                    {
                        Log("User interrupted showing the Hamiltonian Cycle!");
                    } else
                    {
                        Log("User interrupted showing the snake logic!");
                    }
                }
                else
                {
                    Log("Logical end reached.");
                }
                
                btn_Start.Text = "Start";
                IsAppRunning = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void InitPlayground(int playgroundWidth, int playgroundHeight)
        {
            for (int h = 0; h < playgroundHeight; h++)
            {
                for (int w = 0; w < playgroundWidth; w++)
                {
                    var point = ConvertArrayIndecesToGraphicalCoordiantes(new Point { X = w, Y = h });
                    DrawOneRectangle(point, PenPlayground);
                }
            }
        }

        private Point ConvertArrayIndecesToGraphicalCoordiantes(Point point)
        {
            return new Point { 
                X = SquareDeltaX + (SquareDeltaX + SquareWidth) * point.X, 
                Y = SquareDeltaY + (SquareDeltaY + SquareHeight) * point.Y
            };
        }

        private void DrawOneRectangle(Point point, Pen pen)
        {
            Square.X = point.X;
            Square.Y = point.Y;
            Graphics.DrawRectangle(pen, Square);
            SolidBrush.Color = pen.Color;
            Graphics.FillRectangle(SolidBrush, Square);
        }

        private void DrawPositions(List<Point> pointList, DrawItem drawItem)
        {
            /* Drawing the snake in a simple loop ends in a flickering tail due its "movement".
             * Solution:
             * Draw snakes head, than the tail and than the body
             * in case the snake has a tail (means snakes length is greater than 1) .
             * 
             * Further optimizations: Make the decision which pen should be used outside the loop.
             */
            var loopFrom = 0;
            var loopTo = pointList.Count;
            
            var pen = PenPlayground;
            switch (drawItem)
            {
                case DrawItem.Playground:
                    break;
                case DrawItem.Apple:
                    pen = PenApple;
                    break;
                case DrawItem.Snake:
                    if (pointList.Count > 1)
                    {
                        // Head
                        var point = ConvertArrayIndecesToGraphicalCoordiantes(pointList[0]);
                        DrawOneRectangle(point, PenSnakeHead);

                        // Tail
                        point = ConvertArrayIndecesToGraphicalCoordiantes(pointList[pointList.Count - 1]);
                        DrawOneRectangle(point, PenSnakeTail);

                        if (pointList.Count == 2)
                        { 
                            // Everything is drawn.
                            return; 
                        }

                        //DrawCounter++;
                        //if(DrawCounter < 100)
                        //{ 
                        //    return; 
                        //}
                        //DrawCounter = 0;

                        loopFrom = 1;
                        loopTo = pointList.Count - 1;
                        pen = PenSnakeBody;
                    }
                    else
                    {
                        pen = PenSnakeBody;
                    }
                    break;
            }

           
            for (int i = loopFrom; i < loopTo; i++)
            {
                var point = ConvertArrayIndecesToGraphicalCoordiantes(pointList[i]);
                DrawOneRectangle(point, pen);
            }
        }

        private void Log(string message)
        {
            txtBox_Info.Text += message + "\r\n";
        }
    }
}
