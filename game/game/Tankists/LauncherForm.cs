using JUnity;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tankists.GameObjects;
using Tankists.GameObjects.Base;

namespace Tankists
{
    /// <summary>
    /// Launcher form
    /// </summary>
    public partial class LauncherForm : Form
    {
        /// <summary>
        /// Form ctor
        /// </summary>
        public LauncherForm()
        {
            InitializeComponent();
            Text = "Tankists";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            RedTank redTank;
            GreenTank greenTank;

            using (var game = new Engine())
            {
                game.ClearColor = Color.White;
                game.VSync = OpenTK.VSyncMode.Off;
                game.Width = 1000;
                game.Height = 600;
                game.WindowBorder = OpenTK.WindowBorder.Fixed;

                redTank = new RedTank();
                greenTank = new GreenTank();

                Engine.RegisterObject(new Background(@"Textures\Same\Background.jpg"));
                Engine.RegisterObject(new Mountain());
                Engine.RegisterObject(redTank);
                Engine.RegisterObject(greenTank);
                Engine.RegisterObject(new PresentsLauncher(redTank, greenTank));
                Engine.RegisterObject(new ExitEvent());
                redTank.TankProperties = new AmmoBonus(redTank.TankProperties);
                game.Run();
            }
        }
    }
}
