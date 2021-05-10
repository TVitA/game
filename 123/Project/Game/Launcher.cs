using System;
using System.Drawing;
using System.Windows.Forms;

using GameEngine;

using Game.Decorators;
using Game.GameObjects;

namespace Game
{
    /// <summary>
    /// Launcher class.
    /// </summary>
    public partial class Launcher : Form
    {
        /// <summary>
        /// Launcher constructor.
        /// </summary>
        public Launcher()
        {
            InitializeComponent();

            Text = "Game";
        }

        private void StartGame_Click(Object sender, EventArgs e)
        {
            Close();

            FirstArtillery firstPlayer;
            SecondArtillery secondPlayer;

            using (var game = new Engine())
            {
                game.ClearColor = Color.Black;
                game.VSync = OpenTK.VSyncMode.Off;
                game.Width = 1280;
                game.Height = 900;
                game.WindowBorder = OpenTK.WindowBorder.Fixed;

                firstPlayer = new FirstArtillery();
                secondPlayer = new SecondArtillery();

                Engine.RegisterObject(new Background(@"Textures\Others\Background2.jpg"));
                Engine.RegisterObject(new Mountain());
                Engine.RegisterObject(firstPlayer);
                Engine.RegisterObject(secondPlayer);
                Engine.RegisterObject(new PresentsLauncher(firstPlayer, secondPlayer));
                Engine.RegisterObject(new ExitEvent());

                firstPlayer.ArtilleryProperties = new ArtPropsWithAmmoUp(firstPlayer.ArtilleryProperties);

                game.Run();
            }
        }
    }
}
