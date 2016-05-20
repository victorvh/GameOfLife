using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace GameOfLife
{
    public partial class GameRenderer : Form
    {
        // Alter game variables start //
        private const int WindowWidth  = 600;
        private const int WindowHeight = 600;

        private const int NumCellColumns = 60;
        private const int NumCellRows    = 60;

        private const int GameSpeedInMS = 100;

        private Brush DeadBrush = Brushes.White;
        private Brush AliveBrush = Brushes.LawnGreen;
        // Alter game variables end   //

        private const int CellWidth  = (int)(WindowWidth  / NumCellColumns);
        private const int CellHeight = (int)(WindowHeight / NumCellRows);

        private GameSimulator GameSimulation;
        private static System.Timers.Timer GameAutoRun;
        
        private Image Bitmap;
        private Graphics GForm;
        private Graphics g;

        public GameRenderer()
        {
            InitializeComponent();

            GameSimulation = new GameSimulator(NumCellRows, NumCellColumns);

            GameAutoRun = new System.Timers.Timer(GameSpeedInMS);
            GameAutoRun.Elapsed += GameTick;

            this.ClientSize = new Size(WindowWidth, WindowHeight);

            Bitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            GForm = this.CreateGraphics();
            g = Graphics.FromImage(Bitmap);
        }

        private void PaintCell(bool isAlive, int xCoord, int yCoord)
        {
            Brush brush = (isAlive) ? AliveBrush : DeadBrush;

            // If the program was running too fast it would crash while trying to access the g object
            // Now it just calls itself until it no longer gives an exception
            try
            {
                g.FillRectangle(brush, xCoord * CellWidth, yCoord * CellHeight, CellWidth - 1, CellHeight - 1);
            }
            catch(Exception InvalidOperationException)
            {
                PaintCell(isAlive, xCoord, yCoord);
            }
        }

        private void PaintCanvas()
        {
            for (int y = 0; y < GameSimulation.Rows; y++)
            {
                for (int x = 0; x < GameSimulation.Columns; x++)
                {
                    PaintCell(GameSimulation.CellGrid[y, x].IsAlive, x, y);
                }
            }
            GForm.DrawImage(Bitmap, 0, 0);
        }

        private void ReviveCell(int xCoord, int yCoord)
        {
            int cellX = (int)(xCoord / CellWidth);
            int cellY = (int)(yCoord / CellHeight);

            GameSimulation.CellGrid[cellY, cellX].Revive();

            PaintCell(true, cellX, cellY);
            GForm.DrawImage(Bitmap, 0, 0);
        }

        private void RunCycleStep()
        {
            GameSimulation.RunCycleStep();
            PaintCanvas();
        }


        // Input, autorun and setup handling
        private void GameRenderer_MouseClick(object sender, MouseEventArgs e)
        {
            GameAutoRun.Enabled = false;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    RunCycleStep();
                    break;
                case MouseButtons.Right:
                    ReviveCell(e.X, e.Y);
                    break;
            }
        }

        private void GameRenderer_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Char.ToLower(e.KeyChar))
            {
                case 's':
                    GameAutoRun.Enabled = !GameAutoRun.Enabled;
                    break;
            }
        }

        private void GameTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            RunCycleStep();
        }

        private void GameRenderer_Paint(object sender, PaintEventArgs e)
        {
            PaintCanvas();
        }
    }
}
