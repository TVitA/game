using JUnity;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tankists.GameObjects;
using Tankists.GameObjects.Base;
using Tankists.GameObjects.Surroundings;

namespace Tankists
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

            Application.Run(new LauncherForm());
        }
    }
}
