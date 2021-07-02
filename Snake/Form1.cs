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
        public int PlayFieldWidth;
        public int PlayFieldHeight;

        private PlayFieldData[,] PlayFieldData;

        private enum DrawItem
        {
            PlayField = 0,
            Apple, 
            Snake,
            HamiltonianCycle
        }

        private GameLogic CoreLogic;

        private bool IsAppRunning;
        private bool IsUserInterrupted;
        private bool IsHamiltonianCycleShown;

        private Color ColorPlayField;
        private Color ColorHamiltonianCycle;
        private Color ColorApple;
        private Color ColorSnakeHead;
        private Color ColorSnakeBody;
        private Color ColorSnakeTail;

        private SolidBrush CurrentSolidBrush;

        private int SquareWidth;
        private int SquareHeight;
        private int SquareDeltaX;
        private int SquareDeltaY;

        private int WaitFactor;

        private int DrawCounter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = Config.TitleAppNameAndVersion;
        }
        
        private bool GetPlayFieldDimension()
        {
            var width = (int)numUpDown_Width.Value;
            var height = (int)numUpDown_Height.Value;

            WaitFactor = (int)numUpDown_WaitFactor.Value;

            var errorMessage = string.Empty;
            if (width < 2)
            {
                errorMessage = 
                    "The 'PlayFieldWidth' value\r\n" + 
                    "must be equal or greater than 2!";
            }
            if (height < 2)
            {
                errorMessage = 
                    "The 'PlayFieldHeight' value\r\n" + 
                    "must be equal or greater than 2!";
            }
            if (width > 40)
            {
                errorMessage = 
                    "The 'PlayFieldWidth' value must be less than\r\n" +
                    "or equal to 40 for displaying reasons only!";
            }
            if (height > 40)
            {
                errorMessage = 
                    "The 'PlayFieldHeight' value must be less than\r\n" +
                    "or equal to 40 for displaying reasons only!";
            }
            if (!Common.IsValueEven(width) && !Common.IsValueEven(height))
            {
                errorMessage = 
                    "Both 'PlayFieldWidth' and 'PlayFieldHeight'\r\n" + 
                    "can not be odd at the same time!";
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, Config.TitleAppNameAndVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            PlayFieldWidth = width;
            PlayFieldHeight = height;
            return true;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            // ToDo: Uncomment the next line to call the test code.
            // new HamiltonianCycle().Test(); return;

            if (IsAppRunning)
            {
                numUpDown_Width.Enabled = numUpDown_Height.Enabled = chkBx_ShowHamiltonianCycle.Enabled = true;

                btn_Start.Text = "Start";
                IsAppRunning = false;

                IsUserInterrupted = true;
            }
            else
            {
                pictureBox1.Image = null;

                numUpDown_Width.Enabled = numUpDown_Height.Enabled = chkBx_ShowHamiltonianCycle.Enabled = false;

                if (!GetPlayFieldDimension())
                {
                    numUpDown_Width.Enabled = numUpDown_Height.Enabled = chkBx_ShowHamiltonianCycle.Enabled = true;
                    return;
                }

                btn_Start.Text = "Stop";
                IsAppRunning = true;

                IsHamiltonianCycleShown = false;
                IsUserInterrupted = false;

                CoreLogic = new GameLogic(PlayFieldWidth, PlayFieldHeight);

                // ToDo: Reminder to redesign the app: The Form, TextBox, Panel control dimensions, but also the following (Square) variables should be a function of the given playfield dimensions.
                SquareWidth = 20;
                SquareHeight = 20;
                SquareDeltaX = 2;
                SquareDeltaY = 2;

                CurrentSolidBrush = new SolidBrush(Color.Empty);

                ColorPlayField =Color.Black;
                ColorHamiltonianCycle = Color.DarkKhaki;
                ColorApple = Color.Red;
                ColorSnakeHead = Color.Blue;
                ColorSnakeBody = Color.Green;
                ColorSnakeTail = Color.Yellow;

                DrawCounter = 0;

                txtBox_Info.Text =
                    "Used colors:\r\n" +
                    "\tdark khaki = Hamiltonian Cyvle\r\n" +
                    "\r\n" +
                    "\tblack = Playfield\r\n" +
                    "\tred = Apple\r\n" +
                    "\tblue = Snake head\r\n" +
                    "\tgreen = Snake body\r\n" +
                    "\tyellow = Snake tail\r\n" +
                    "\r\n" +
                    "Possible playfield dimensions:\r\n" +
                    "\tMin. value is 2x2,\r\n" +
                    "\tmax. value is 40x40\r\n" +
                    "\t(40x40 for display reasons only)\r\n" +
                    "\r\n" +
                    "Current playfield dimensions:\r\n" +
                    $"\tWidth: {PlayFieldWidth},\r\n" +
                    $"\tHeight: {PlayFieldHeight}\r\n" +
                    "\r\n";

                InitPlayField(PlayFieldWidth, PlayFieldHeight);

                if (chkBx_ShowHamiltonianCycle.Checked)
                {
                    ShowHamiltonianCycle();
                }
                else
                {
                    IsHamiltonianCycleShown = true;
                }

                CoreLogicWrapper();
            }
        }

        private void ShowHamiltonianCycle() 
        {
            Log("The Hamiltonian Cycle will be displayed.");
            var taskA = Task.Factory.StartNew(() =>
            {
                var currentPosition = new Point(0, 0);
                do
                {
                    ChangePlayFieldData(new List<Point> { currentPosition }, DrawItem.HamiltonianCycle);
                    pictureBox1.Invalidate();
                    switch (CoreLogic.HamiltonianCycleData.Data.MoveDirections[currentPosition.X, currentPosition.Y])
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
                    Wait();
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
                InitPlayField(PlayFieldWidth, PlayFieldHeight);

                var coreLogicReturns = new GameLogic.ReturnData();
                do
                {
                    coreLogicReturns = CoreLogic.Main();
                    if (coreLogicReturns.ResetThesePositions != null && coreLogicReturns.ResetThesePositions.Count > 0)
                    {
                        ChangePlayFieldData(coreLogicReturns.ResetThesePositions, DrawItem.PlayField);
                    }

                    if (coreLogicReturns.SnakePositions != null && coreLogicReturns.SnakePositions.Count > 0)
                    {
                        ChangePlayFieldData(coreLogicReturns.SnakePositions, DrawItem.Snake);
                    }

                    if (coreLogicReturns.ApplePosition != null && !coreLogicReturns.IslogicalEndReached)
                    {
                        ChangePlayFieldData(new List<Point> { coreLogicReturns.ApplePosition }, DrawItem.Apple);
                    }
                    pictureBox1.Invalidate();
                    Wait();
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
                numUpDown_Width.Enabled = numUpDown_Height.Enabled = chkBx_ShowHamiltonianCycle.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void InitPlayField(int playFieldWidth, int playFieldHeight)
        {
            PlayFieldData = new PlayFieldData[PlayFieldWidth, PlayFieldHeight];
            for (int h = 0; h < playFieldHeight; h++)
            {
                for (int w = 0; w < playFieldWidth; w++)
                {
                    var drawPoint = ConvertArrayIndecesToGraphicalCoordiantes(new Point(w, h));
                    PlayFieldData[w, h] = new PlayFieldData
                    {
                        DrawDatas = new Rectangle {
                            X = drawPoint.X,
                            Y = drawPoint.Y,
                            Width = SquareWidth,
                            Height = SquareHeight
                        },
                        Color = ColorPlayField
                    };
                }
            }
            pictureBox1.Invalidate(); // Triggers "pictureBox1_Paint(...)" call.
        }

        private void ChangePlayFieldData(List<Point> pointList, DrawItem drawItem)
        {
            /* "pointList" contains a list of cartesian coordiantes that should be changed.
             * This can be one point for the apple or 100 points for snakes body.
             * 
             * The "drawItem" defines the change  kind.
             * This information defines finally the color to use.
             */
            for (int i = 0; i < pointList.Count; i++)
            {
                switch (drawItem)
                {
                    case DrawItem.PlayField:
                        PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorPlayField;
                        break;
                    case DrawItem.Apple:
                        PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorApple;
                        break;
                    case DrawItem.Snake:
                        if (i == 0)
                        {
                            PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorSnakeHead;
                        } else if (i == pointList.Count - 1)
                        {
                            PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorSnakeTail;
                        }
                        else
                        {
                            PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorSnakeBody;
                        }
                        break;
                    case DrawItem.HamiltonianCycle:
                        PlayFieldData[pointList[i].X, pointList[i].Y].Color = ColorHamiltonianCycle;
                        break;
                    default:
                        break;
                }
            } 
        }

        private Point ConvertArrayIndecesToGraphicalCoordiantes(Point cartesianCoordinate)
        {
            return new Point
            {
                X = SquareDeltaX + (SquareDeltaX + SquareWidth) * cartesianCoordinate.X,
                Y = SquareDeltaY + (SquareDeltaY + SquareHeight) * cartesianCoordinate.Y
            };
        }

        private void Log(string message)
        {
            txtBox_Info.Text += message + "\r\n";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentSolidBrush == null)
            { 
                return;
            }

            for (int h = 0; h < PlayFieldHeight; h++)
            {
                for (int w = 0; w < PlayFieldWidth; w++)
                {
                    CurrentSolidBrush.Color = PlayFieldData[w, h].Color;
                    e.Graphics.FillRectangle(CurrentSolidBrush, PlayFieldData[w, h].DrawDatas);
                }
            }
        }

        private void Wait()
        {
            if (WaitFactor == 0)
            { 
                return;
            }

            var index = 0;
            var maxLoop = 500000 * WaitFactor;
            do
            {
                index++;
            } while (index < maxLoop);
        }

        private void numUpDown_WaitFactor_ValueChanged(object sender, EventArgs e)
        {
            WaitFactor = (int)numUpDown_WaitFactor.Value;
        }
    }
}
