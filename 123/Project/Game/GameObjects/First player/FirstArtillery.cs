using System;

using OpenTK;
using OpenTK.Input;

using GameEngine.Input;

namespace Game.GameObjects
{
    /// <summary>
    /// First artillery class.
    /// </summary>
    class FirstArtillery : Artillery
    {
        /// <summary>
        /// Rocket positions array.
        /// </summary>
        private readonly Vector2[] rocketsPositions;

        /// <summary>
        /// First artillery constructor.
        /// </summary>
        public FirstArtillery()
            : base(@"Textures\First player\Red_tank.png", @"Textures\First player\Red_launcher.png", typeof(FirstRocket))
        {
            rocketsPositions = new[]
            {
                new Vector2(Launcher.Offset.X + 21.0f, Launcher.Offset.Y - 1.0f),
                new Vector2(Launcher.Offset.X + 21.0f, Launcher.Offset.Y - 13.0f),
                new Vector2(Launcher.Offset.X + 21.0f, Launcher.Offset.Y - 25.0f)
            };

            Animator.AnimationFrames.SetOffsetToAll(10.0f, -59.0f);
            Launcher.Offset = new Vector2(-20.0f, 33.0f);
            Dot.Offset = new Vector2(-20.0f, 9.0f);
        }

        /// <summary>
        /// Returns rocket positions array.
        /// </summary>
        protected override Vector2[] RocketsPositions => rocketsPositions;

        /// <summary>
        /// Fixed update function.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (ArtilleryProperties.Health <= 0.0f)
            {
                return;
            }

            if (InputManager.KeyboardState.IsKeyDown(Key.A))
            {
                Rigidbody.Force = new Vector2(-ArtilleryProperties.EnginePower, Rigidbody.Force.Y);
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.D))
            {
                Rigidbody.Force = new Vector2(ArtilleryProperties.EnginePower, Rigidbody.Force.Y);
            }
            else
            {
                Rigidbody.Force = new Vector2(0.0f, Rigidbody.Force.Y);
            }

            if (InputManager.KeyboardState.IsKeyDown(Key.W))
            {
                Launcher.Rotation += LauncherRotationSpeed * (Single)deltaTime;

                if (Launcher.Rotation > 89.0f)
                {
                    Launcher.Rotation = 89.0f;
                }
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.S))
            {
                Launcher.Rotation -= LauncherRotationSpeed * (Single)deltaTime;

                if (Launcher.Rotation < -10.0f)
                {
                    Launcher.Rotation = -10.0f;
                }
            }

            if (InputManager.IsKeyJustPressed(Key.ControlLeft))
            {
                Fire();
            }

            base.FixedUpdate(deltaTime);
        }
    }
}
