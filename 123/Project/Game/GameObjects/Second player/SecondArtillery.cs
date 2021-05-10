using System;

using OpenTK;
using OpenTK.Input;

using GameEngine.Input;

namespace Game.GameObjects
{
    /// <summary>
    /// Second artillery class.
    /// </summary>
    public class SecondArtillery : Artillery
    {
        /// <summary>
        /// Rocket positions array.
        /// </summary>
        private readonly Vector2[] rocketsPositions;

        /// <summary>
        /// First artillery constructor.
        /// </summary>
        public SecondArtillery()
            : base(@"Textures\Second player\Green_tank.png", @"Textures\Second player\Green_launcher.png", typeof(SecondRocket))
        {
            SpriteRenderer.Sprites.SetFlipToAll(true, false);
            Animator.AnimationFrames.SetFlipToAll(true, false);

            rocketsPositions = new[]
            {
                new Vector2(Launcher.Offset.X - 22.0f, Launcher.Offset.Y - 1.0f),
                new Vector2(Launcher.Offset.X - 22.0f, Launcher.Offset.Y - 13.0f),
                new Vector2(Launcher.Offset.X - 22.0f, Launcher.Offset.Y - 25.0f)
            };

            Animator.AnimationFrames.SetOffsetToAll(-10.0f, -59.0f);

            Launcher.Offset = new Vector2(20.0f, 33.0f);
            Dot.Offset = new Vector2(20.0f, 9.0f);

            Position = new Vector2(10000, Position.Y);
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

            if (InputManager.KeyboardState.IsKeyDown(Key.Left))
            {
                Rigidbody.Force = new Vector2(-ArtilleryProperties.EnginePower, Rigidbody.Force.Y);
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.Right))
            {
                Rigidbody.Force = new Vector2(ArtilleryProperties.EnginePower, Rigidbody.Force.Y);
            }
            else
            {
                Rigidbody.Force = new Vector2(0.0f, Rigidbody.Force.Y);
            }

            if (InputManager.KeyboardState.IsKeyDown(Key.Up))
            {
                Launcher.Rotation -= LauncherRotationSpeed * (Single)deltaTime;

                if (Launcher.Rotation < -89.0f)
                {
                    Launcher.Rotation = -89.0f;
                }
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.Down))
            {
                Launcher.Rotation += LauncherRotationSpeed * (Single)deltaTime;

                if (Launcher.Rotation > 10.0f)
                {
                    Launcher.Rotation = 10.0f;
                }
            }

            if (InputManager.IsKeyJustPressed(Key.ControlRight))
            {
                Fire();
            }

            base.FixedUpdate(deltaTime);
        }
    }
}
