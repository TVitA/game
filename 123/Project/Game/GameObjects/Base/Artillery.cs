using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

using OpenTK;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Graphics;
using GameEngine.Physics;
using GameEngine.Utilities;

namespace Game.GameObjects
{
    /// <summary>
    /// Artillery base class.
    /// </summary>
    public abstract class Artillery : GameObject
    {
        /// <summary>
        /// Artillery properties.
        /// </summary>
        private ArtilleryProperties artilleryProperties;

        /// <summary>
        /// Launcher rotation speed.
        /// </summary>
        private Single launcherRotationSpeed = 50.0f;

        /// <summary>
        /// Time elapsed between frames.
        /// </summary>
        private Single timeElapsed;

        /// <summary>
        /// Sprite renderer.
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Rigidbody.
        /// </summary>
        private Rigidbody rigidbody;

        /// <summary>
        /// Animator.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Launcher.
        /// </summary>
        private Sprite launcher;

        /// <summary>
        /// Dot sprite.
        /// </summary>
        private Sprite dot;

        /// <summary>
        /// File animator.
        /// </summary>
        private Animator fireAnimator;

        /// <summary>
        /// Rocket type.
        /// </summary>
        private readonly Type rocketType;

        /// <summary>
        /// Queue of rockets.
        /// </summary>
        private readonly Queue<Rocket> rockets;

        /// <summary>
        /// Artillery constructor.
        /// </summary>
        /// <param name="bodySpritePath">Artillery body sprite.</param>
        /// <param name="launcherSpritePath">Launcher sprite path.</param>
        /// <param name="rocketType">Rocket type.</param>
        public Artillery(String bodySpritePath, String launcherSpritePath, Type rocketType)
        {
            rockets = new Queue<Rocket>();

            artilleryProperties = new ArtilleryProperties()
            {
                Health = 100.0f,
                Ammo = 10,
            };

            this.rocketType = rocketType;

            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();
            animator = AddComponent<Animator>();
            fireAnimator = AddComponent<Animator>();

            var body = spriteRenderer.Sprites.AddByName(bodySpritePath,
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            body.ZOrder = 10.10f;

            dot = spriteRenderer.Sprites.AddByName(@"Textures\Others\Launcher_pulley.png",
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            dot.ZOrder = 10.16f;

            launcher = spriteRenderer.Sprites.AddByName(launcherSpritePath,
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            launcher.RotationPoint = new Vector2(0.0f, -18.0f);
            launcher.ZOrder = 10.15f;

            animator.AnimationFrames.AddByName(@"Textures\Others\Track_pos1.png",
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            animator.AnimationFrames.AddByName(@"Textures\Others\Track_pos2.png",
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            animator.AnimationFrames.SetZOrderToAll(10.09f);
            animator.AnimationFrames.SetScaleToAll(0.6f);
            animator.Paused = true;

            fireAnimator.AnimationFrames.AddRange(new[]
            {
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(1, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(65, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(129, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(193, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(257, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(321, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(385, 393, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(449, 393, 63, 103)),

                new Sprite(@"Textures\Others\Fire.png", new Rectangle(1, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(65, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(129, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(193, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(257, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(321, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(385, 266, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(449, 266, 63, 103)),

                new Sprite(@"Textures\Others\Fire.png", new Rectangle(1, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(65, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(129, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(193, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(257, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(321, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(385, 138, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(449, 138, 63, 103)),

                new Sprite(@"Textures\Others\Fire.png", new Rectangle(1, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(65, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(129, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(193, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(257, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(321, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(385, 10, 63, 103)),
                new Sprite(@"Textures\Others\Fire.png", new Rectangle(449, 10, 63, 103)),
            });

            fireAnimator.AnimationFrames.SetZOrderToAll(16.0f);
            fireAnimator.Delay = 0.03f;
            fireAnimator.AnimationFrames.SetScaleToAll(4.0f);
            fireAnimator.AnimationFrames.SetOffsetToAll(0.0f, 30.0f);
            fireAnimator.Enabled = false;

            spriteRenderer.Sprites.SetScaleToAll(0.6f);

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-89, -43), new Vector2(89, 33)));
            rigidbody.OnTriggerEnter += ReceiveDamage;
            rigidbody.Resistance = new Vector2(30.0f, 0.0f);
            rigidbody.Mass = 10.0f;
        }

        /// <summary>
        /// Returns artillery properties.
        /// </summary>
        public ArtilleryProperties ArtilleryProperties
        {
            get => artilleryProperties;

            set => artilleryProperties = value;
        }

        /// <summary>
        /// Returns launcher rotation speed.
        /// </summary>
        protected Single LauncherRotationSpeed => launcherRotationSpeed;

        /// <summary>
        /// Returns sprite renderer.
        /// </summary>
        protected SpriteRenderer SpriteRenderer => spriteRenderer;

        /// <summary>
        /// Returns rigidbody.
        /// </summary>
        protected Rigidbody Rigidbody => rigidbody;

        /// <summary>
        /// Returns animator.
        /// </summary>
        protected Animator Animator => animator;

        /// <summary>
        /// Returns launcher sprite.
        /// </summary>
        protected Sprite Launcher => launcher;

        /// <summary>
        /// Returns dot sprite.
        /// </summary>
        protected Sprite Dot => dot;

        /// <summary>
        /// Rocket positions array.
        /// </summary>
        protected abstract Vector2[] RocketsPositions { get; }

        /// <summary>
        /// OnTriggerEnter event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">TriggerEnterEventArgs.</param>
        private void ReceiveDamage(Object sender, TriggerEnterEventArgs e)
        {
            if (e.Collider.Rigidbody.Owner is Rocket && !rocketType.IsAssignableFrom(e.Collider.Rigidbody.Owner.GetType()))
            {
                var rocket = (Rocket)e.Collider.Rigidbody.Owner;

                artilleryProperties.Health -= rocket.Damage - (rocket.Damage / 100.0f * (artilleryProperties.Armor * 100.0f));

                if (artilleryProperties.Health <= 0.0f)
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (rockets.Count < 1)
            {
                timeElapsed += (Single)deltaTime;

                if (timeElapsed >= artilleryProperties.ReloadTime && artilleryProperties.Ammo > 0)
                {
                    ReloadRocket();
                    artilleryProperties.Ammo--;
                    timeElapsed = 0.0f;
                }
            }

            UpdateAnimation();
            UpdateRocketsPosition();
            CheckBounds();
        }

        /// <summary>
        /// Check bounds.
        /// </summary>
        private void CheckBounds()
        {
            if (Position.X + rigidbody.Colliders[0].Width / 2.0f > Engine.ClientWidth)
            {
                var x = Engine.ClientWidth - rigidbody.Colliders[0].Width / 2.0f;
                var y = Position.Y;

                Position = new Vector2(x, y);

                rigidbody.Velocity = new Vector2(0.0f, rigidbody.Velocity.Y);
            }

            if (Position.X - rigidbody.Colliders[0].Width / 2.0f < 0.0f)
            {
                var x = rigidbody.Colliders[0].Width / 2.0f;
                var y = Position.Y;

                Position = new Vector2(x, y);

                rigidbody.Velocity = new Vector2(0.0f, rigidbody.Velocity.Y);
            }

            if (Position.Y - rigidbody.Colliders[0].Heigth / 2.0f - 5.0f < 0.0f)
            {
                var x = Position.X;
                var y = rigidbody.Colliders[0].Heigth / 2.0f + 5.0f;

                Position = new Vector2(x, y);

                rigidbody.Velocity = new Vector2(rigidbody.Velocity.X, 0.0f);
            }
        }

        /// <summary>
        /// Update animation.
        /// </summary>
        private void UpdateAnimation()
        {
            if (rigidbody.Velocity.X != 0)
            {
                animator.Paused = false;

                animator.Delay = 10.0f / Math.Abs(rigidbody.Velocity.X);
            }
            else
            {
                animator.Paused = true;
            }
        }

        /// <summary>
        /// Update rockets position.
        /// </summary>
        private void UpdateRocketsPosition()
        {
            //var i = 3 - rockets.Count;

            foreach (Rocket rocket in rockets)
            {
                rocket.Position = launcher.Offset + RocketsPositions[0];

                TransformationHelper.RotateAroundPoint(rocket,
                    (launcher.RotationPoint + launcher.Offset) * rocket.SpriteRenderer.Sprites[0].Scale,
                    launcher.Rotation);

                rocket.Position += Position;
            }
        }

        /// <summary>
        /// Launch rocket.
        /// </summary>
        protected void Fire()
        {
            if (rockets.Count > 0)
            {
                rockets.Dequeue().Launch();
            }
        }

        /// <summary>
        /// Reload rocket.
        /// </summary>
        protected void ReloadRocket()
        {
            var rocket = (Rocket)Activator.CreateInstance(rocketType);

            rocket.Owner = this;

            rockets.Enqueue(rocket);

            Engine.RegisterObject(rocket);
        }

        /// <summary>
        /// Die.
        /// </summary>
        protected void Die()
        {
            spriteRenderer.Sprites.SetColorToAll(Color.FromArgb(110, 48, 0));
            animator.AnimationFrames.SetColorToAll(Color.FromArgb(110, 48, 0));

            rigidbody.Enabled = false;
            fireAnimator.Enabled = true;

            String player = "";

            if (this is FirstArtillery)
            {
                player = "Second";
            }
            else
            {
                player = "First";
            }

            Task.Factory.StartNew(() =>
            {
                MessageBox.Show(player + " player win!", "Game over.");

                Engine.Stop();
            });
        }
    }
}
