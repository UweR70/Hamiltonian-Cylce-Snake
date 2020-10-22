using Snake.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public int PlaygroundWidth;
        public int PlaygroundHeight;

        private enum DrawItem
        {
            Playground = 0,
            Apple, 
            Snake
        }

        private GameLogic CoreLogic;

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

        private Color[,] CurrentColors;

        private int DrawCounter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = Config.TitleAppNameAndVersion;
        }
        
        private bool GetPlaygroundDimension()
        {
            var width = (int)numUpDown_Width.Value;
            var height = (int)numUpDown_Height.Value;

            var errorMessage = string.Empty;
            if (width < 2)
            {
                errorMessage = 
                    "The 'PlaygroundWidth' value\r\n" + 
                    "must be equal or greater than 2!";
            }
            if (height < 2)
            {
                errorMessage = 
                    "The 'PlaygroundHeight' value\r\n" + 
                    "must be equal or greater than 2!";
            }
            if (width > 40)
            {
                errorMessage = 
                    "The 'PlaygroundWidth' value must be less than\r\n" +
                    "or equal to 40 for displaying reasons only!";
            }
            if (height > 40)
            {
                errorMessage = 
                    "The 'PlaygroundHeight' value must be less than\r\n" +
                    "or equal to 40 for displaying reasons only!";
            }
            if (!Common.IsValueEven(width) && !Common.IsValueEven(height))
            {
                errorMessage = 
                    "Both 'PlaygroundWidth' and 'PlaygroundHeight'\r\n" + 
                    "can not be odd at the same time!";
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, Config.TitleAppNameAndVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            PlaygroundWidth = width;
            PlaygroundHeight = height;
            return true;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            // ToDo: Remove this after testing!
            // new HamiltonianCycle().Test(); 
            // return;

            if (IsAppRunning)
            {
                numUpDown_Width.Enabled = numUpDown_Height.Enabled = true;

                btn_Start.Text = "Start";
                IsAppRunning = false;

                IsUserInterrupted = true;

                CurrentColors = null;
            }
            else
            {
                panel1.Refresh();
                numUpDown_Width.Enabled = numUpDown_Height.Enabled = false;

                if (!GetPlaygroundDimension())
                {
                    numUpDown_Width.Enabled = numUpDown_Height.Enabled = true;
                    return;
                }

                btn_Start.Text = "Stop";
                IsAppRunning = true;

                IsHamiltonianCycleShown = false;
                IsUserInterrupted = false;

                CoreLogic = new GameLogic(PlaygroundWidth, PlaygroundHeight);

                // ToDo: The next four parameter should be a function of the playground width and height.
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

                txtBox_Info.Text =
                    "Used colors:\r\n" +
                    "\tdark khaki = Hamiltonian Cyvle\r\n" +
                    "\r\n" +
                    "\tblack = Playground\r\n" +
                    "\tred = Apple\r\n" +
                    "\tblue = Snake head\r\n" +
                    "\tgreen = Snake body\r\n" +
                    "\tyellow = Snake tail\r\n" +
                    "\r\n" +
                    "Possible playground dimensions:\r\n" +
                    "\tMin. value is 2x2,\r\n" +
                    "\tmax. value is 40x40\r\n" +
                    "\t(40x40 for display reasons only)\r\n" +
                    "\r\n" +
                    "Current playground dimensions:\r\n" +
                    $"\tWidth: {PlaygroundWidth},\r\n" +
                    $"\tHeight: {PlaygroundHeight}\r\n" +
                    "\r\n";                   

                ShowHamiltonianCycle();
                CoreLogicWrapper();
            }
        }

        private void ShowHamiltonianCycle() 
        {
            Log("The Hamiltonian Cycle will be displayed.");
            var taskA = Task.Factory.StartNew(() =>
            {
                InitPlayground(PlaygroundWidth, PlaygroundHeight);

                var currentPosition = new Point(0, 0);
                do
                {
                    DrawOneRectangle(currentPosition, PenHamiltonianCycle);
                    switch (CoreLogic.HamiltonianCycleMoveDirections[currentPosition.X, currentPosition.Y])
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
                    Thread.Sleep(15);
                } while (!(currentPosition.X == 0 && currentPosition.Y == 0) && !IsUserInterrupted);
            }).ContinueWith(result =>
            {
                // Controls are handled here to avoid a "cross-thread" error.
                if (!IsUserInterrupted)
                {
                    Log("Done.");
                    Log("Start main logic.");
                }

                IsHamiltonianCycleShown = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        
        private void CoreLogicWrapper()
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
                InitPlayground(PlaygroundWidth, PlaygroundHeight);

                var coreLogicReturns = new GameLogic.ReturnData();
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
                        DrawOneRectangle(coreLogicReturns.ApplePosition, PenApple);
                    }
                    Thread.Sleep(10);
                } while (!coreLogicReturns.IslogicalEndReached && IsAppRunning && !IsUserInterrupted);
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
                numUpDown_Width.Enabled = numUpDown_Height.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void InitPlayground(int playgroundWidth, int playgroundHeight)
        {
            CurrentColors = new Color[PlaygroundWidth, PlaygroundHeight];   // Init
            for (int h = 0; h < playgroundHeight; h++)
            {
                for (int w = 0; w < playgroundWidth; w++)
                {
                    DrawOneRectangle(new Point { X = w, Y = h }, PenPlayground);
                }
            }
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
                        DrawOneRectangle(pointList[0], PenSnakeHead);

                        // Tail
                        DrawOneRectangle(pointList[pointList.Count - 1], PenSnakeTail);

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
                DrawOneRectangle(pointList[i], pen);
            }
        }

        private void DrawOneRectangle(Point arrayIndex, Pen pen)
        {
            if (CurrentColors[arrayIndex.X, arrayIndex.Y] == pen.Color)
            {
                return;
            }
            CurrentColors[arrayIndex.X, arrayIndex.Y] = pen.Color;

            var graphicalPoint = ConvertArrayIndecesToGraphicalCoordiantes(arrayIndex);
            Square.X = graphicalPoint.X;
            Square.Y = graphicalPoint.Y;
            Graphics.DrawRectangle(pen, Square);
            SolidBrush.Color = pen.Color;
            Graphics.FillRectangle(SolidBrush, Square);
        }

        private Point ConvertArrayIndecesToGraphicalCoordiantes(Point graphicalPoint)
        {
            return new Point
            {
                X = SquareDeltaX + (SquareDeltaX + SquareWidth) * graphicalPoint.X,
                Y = SquareDeltaY + (SquareDeltaY + SquareHeight) * graphicalPoint.Y
            };
        }

        private void Log(string message)
        {
            txtBox_Info.Text += message + "\r\n";
        }
    }
}
