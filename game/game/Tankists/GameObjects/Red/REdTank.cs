using JUnity.Input;
using OpenTK;
using OpenTK.Input;

namespace Tankists.GameObjects
{
    class RedTank : Tank
    {
        public RedTank()
            : base(@"Textures\Red\Red_tank.png", @"Textures\Red\Red_launcher.png", typeof(RedRocket))
        {
            rocketsPositions = new[] {
                new Vector2(launcher.offset.X + 21.0f, launcher.offset.Y - 1.0f),
                new Vector2(launcher.offset.X + 21.0f, launcher.offset.Y - 13.0f),
                new Vector2(launcher.offset.X + 21.0f, launcher.offset.Y - 25.0f)
            };

            animator.AnimationFrames.SetOffsetToAll(10.0f, -59.0f);
            launcher.offset = new Vector2(-20.0f, 33.0f);
            dot.offset = new Vector2(-20.0f, 9.0f);
        }

        private readonly Vector2[] rocketsPositions;

        protected override Vector2[] RocketsPositions => rocketsPositions;

        public override void FixedUpdate(double deltaTime)
        {
            if (TankProperties.Hp <= 0.0f)
            {
                return;
            }

            if (InputManager.KeyboardState.IsKeyDown(Key.A))
            {
                rigidbody.force.X = -TankProperties.EnginePower;
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.D))
            {
                rigidbody.force.X = TankProperties.EnginePower;
            }
            else
            {
                rigidbody.force.X = 0.0f;
            }

            if (InputManager.KeyboardState.IsKeyDown(Key.W))
            {
                launcher.Rotation += launcherRotationSpeed * (float)deltaTime;
                if (launcher.Rotation > 89.0f)
                {
                    launcher.Rotation = 89.0f;
                }
            }
            else if (InputManager.KeyboardState.IsKeyDown(Key.S))
            {
                launcher.Rotation -= launcherRotationSpeed * (float)deltaTime;
                if (launcher.Rotation < -10.0f)
                {
                    launcher.Rotation = -10.0f;
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
