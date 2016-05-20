using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameSimulator
    {
        public Cell[,] CellGrid;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public GameSimulator(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            CellGrid = new Cell[Rows, Columns];
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    CellGrid[y, x] = new Cell();
                }
            }
        }

        private int CalcNeighborsAlive(int startX, int startY)
        {
            int neighborsAlive = 0;

            int startCoordX = (startX - 1 < 0) ? startX : startX - 1;
            int startCoordY = (startY - 1 < 0) ? startY : startY - 1;
            int endCoordX = (startX + 1 >= Columns) ? startX : startX + 1;
            int endCoordY = (startY + 1 >= Rows) ? startY : startY + 1;

            for (int y = startCoordY; y <= endCoordY; y++)
            {
                for (int x = startCoordX; x <= endCoordX; x++)
                {
                    if (CellGrid[y, x].IsAlive)
                    {
                        neighborsAlive++;
                    }

                }
            }

            return (CellGrid[startY, startX].IsAlive) ? --neighborsAlive : neighborsAlive;
        }

        private void DecideFate(Cell cell, int startX, int startY)
        {
            int neighborsAlive = CalcNeighborsAlive(startX, startY);

            if (cell.IsAlive)
            {
                if (neighborsAlive > 3 || neighborsAlive < 2)
                {
                    cell.Kill();
                }
            }
            else
            {
                if (neighborsAlive == 3)
                {
                    cell.Revive();
                }
            }
        }

        private void Moirai()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    DecideFate(CellGrid[y, x], x, y);
                }
            }
        }

        private void UpdateCycle()
        {
            foreach (Cell cell in CellGrid)
            {
                cell.UpdateAliveStatus();
            }
        }

        public void RunCycleStep()
        {
            Moirai();
            UpdateCycle();
        }

    }
}
