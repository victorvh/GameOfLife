using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameRenderer GameRender = new GameRenderer();
            GameRender.FormBorderStyle = FormBorderStyle.FixedSingle;
            GameRender.MaximizeBox = false;
            GameRender.MinimizeBox = false;

            Application.Run(GameRender);
        }
    }
}
