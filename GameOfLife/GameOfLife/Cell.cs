using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Cell
    {
        public bool IsAlive { get; private set; }
        private bool _willBeAlive;

        public Cell(bool initIsAlive = false)
        {
            IsAlive = initIsAlive;
        }

        public void Kill()
        {
            _willBeAlive = false;
        }

        public void Revive()
        {
            _willBeAlive = true;
        }

        public void UpdateAliveStatus()
        {
            IsAlive = _willBeAlive;
        }

    }
}
