using JUnity;
using JUnity.Basic;
using JUnity.Graphics;
using JUnity.Physics;
using JUnity.Utilities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tankists.GameObjects
{
    /// <summary>
    /// Base tank class
    /// </summary>
    public abstract class Tank : GameObject
    {
        /// <summary>
        /// Tank props
        /// </summary>
        public TankProperties TankProperties { get; set; }

        /// <summary>
        /// Launcher rotation speed
        /// </summary>
        protected float launcherRotationSpeed = 50.0f;
        private float timeElapsed;

        /// <summary>
        /// Sprite renderer
        /// </summary>
        protected SpriteRenderer spriteRenderer;

        /// <summary>
        /// Rigidbody
        /// </summary>
        protected Rigidbody rigidbody;

        /// <summary>
        /// Animator
        /// </summary>
        protected Animator animator;

        /// <summary>
        /// Launcher
        /// </summary>
        protected Sprite launcher;

        /// <summary>
        /// Dot sprite
        /// </summary>
        protected Sprite dot;

        private Animator fireAnimator;

        /// <summary>
        /// Rocket positions array
        /// </summary>
        protected abstract Vector2[] RocketsPositions { get; }

        private readonly Type rocketType;
        private readonly Queue<Rocket> rockets = new Queue<Rocket>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bodySpritePath">Tank body sprite</param>
        /// <param name="launcherSpritePath">Launcher sprite path</param>
        /// <param name="rocketType">Rocket type</param>
        public Tank(string bodySpritePath, string launcherSpritePath, Type rocketType)
        {
            TankProperties = new TankProperties()
            {
                Hp = 100.0f,
                Ammo = 10
            };

            this.rocketType = rocketType;

            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();
            animator = AddComponent<Animator>();
            fireAnimator = AddComponent<Animator>();

            var body = spriteRenderer.Sprites.AddByName(bodySpritePath, OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            body.ZOrder = 10.10f;

            dot = spriteRenderer.Sprites.AddByName(@"Textures\Same\Launcher_pulley.png", OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            dot.ZOrder = 10.16f;

            launcher = spriteRenderer.Sprites.AddByName(launcherSpritePath, OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            launcher.rotationPoint = new Vector2(0.0f, -18.0f);
            launcher.ZOrder = 10.15f;

            animator.AnimationFrames.AddByName(@"Textures\Same\Track_pos1.png", OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            animator.AnimationFrames.AddByName(@"Textures\Same\Track_pos2.png", OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            animator.AnimationFrames.SetZOrderToAll(10.09f);
            animator.AnimationFrames.SetScaleToAll(0.6f);
            animator.Paused = true;

            fireAnimator.AnimationFrames.AddRange(new[] {
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(1, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(65, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(129, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(193, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(257, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(321, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(385, 393, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(449, 393, 63, 103)),

                new Sprite(@"Textures\Same\Fire.png", new Rectangle(1, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(65, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(129, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(193, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(257, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(321, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(385, 266, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(449, 266, 63, 103)),

                new Sprite(@"Textures\Same\Fire.png", new Rectangle(1, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(65, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(129, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(193, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(257, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(321, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(385, 138, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(449, 138, 63, 103)),

                new Sprite(@"Textures\Same\Fire.png", new Rectangle(1, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(65, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(129, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(193, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(257, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(321, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(385, 10, 63, 103)),
                new Sprite(@"Textures\Same\Fire.png", new Rectangle(449, 10, 63, 103))
            });
            fireAnimator.AnimationFrames.SetZOrderToAll(16.0f);
            fireAnimator.Delay = 0.03f;
            fireAnimator.AnimationFrames.SetScaleToAll(4.0f);
            fireAnimator.AnimationFrames.SetOffsetToAll(0.0f, 30.0f);
            fireAnimator.Enabled = false;

            spriteRenderer.Sprites.SetScaleToAll(0.6f);

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-89, -43), new Vector2(89, 33)));
            rigidbody.OnTriggerEnter += ReceiveDamage;
            rigidbody.resistance = new Vector2(30.0f, 0.0f);
            rigidbody.Mass = 10.0f;
        }

        private void ReceiveDamage(object sender, TriggerEnterEventArgs e)
        {
            if (e.other.Rigidbody.owner is Rocket && !rocketType.IsAssignableFrom(e.other.Rigidbody.owner.GetType()))
            {
                var tmp = (Rocket)e.other.Rigidbody.owner;
                TankProperties.Hp -= tmp.Damage - (tmp.Damage / 100.0f * (TankProperties.ArmorResistance * 100.0f));
                if (TankProperties.Hp <= 0.0f)
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// Fixed update method
        /// </summary>
        /// <param name="deltaTime">Time between frames</param>
        public override void FixedUpdate(double deltaTime)
        {
            if (rockets.Count < 3)
            {
                timeElapsed += (float)deltaTime;
                if (timeElapsed >= TankProperties.ReloadTime && TankProperties.Ammo > 0)
                {
                    ReloadRocket();
                    TankProperties.Ammo--;
                    timeElapsed = 0.0f;
                }
            }

            UpdateAnimation();
            UpdateRocketsPosition();
            CheckBounds();
        }

        private void CheckBounds()
        {
            if (position.X + rigidbody.Colliders[0].Width / 2.0f > Engine.ClientWidth)
            {
                position.X = Engine.ClientWidth - rigidbody.Colliders[0].Width / 2.0f;
                rigidbody.velocity.X = 0.0f;
            }

            if (position.X - rigidbody.Colliders[0].Width / 2.0f < 0.0f)
            {
                position.X = rigidbody.Colliders[0].Width / 2.0f;
                rigidbody.velocity.X = 0.0f;
            }

            if (position.Y - rigidbody.Colliders[0].Heigth / 2.0f - 5.0f < 0.0f)
            {
                position.Y = rigidbody.Colliders[0].Heigth / 2.0f + 5.0f;
                rigidbody.velocity.Y = 0.0f;
            }
        }

        private void UpdateAnimation()
        {
            if (rigidbody.velocity.X != 0)
            {
                animator.Paused = false;
                animator.Delay = 10.0f / Math.Abs(rigidbody.velocity.X);
            }
            else
            {
                animator.Paused = true;
            }
        }

        private void UpdateRocketsPosition()
        {
            var i = 3 - rockets.Count;
            foreach (var item in rockets)
            {
                item.position = launcher.offset + RocketsPositions[i++];
                TransformationHelper.RotateAroundPoint(item, (launcher.rotationPoint + launcher.offset) * item.SpriteRenderer.Sprites[0].scale, launcher.Rotation);
                item.position += position;
            }
        }

        /// <summary>
        /// Launch rocket
        /// </summary>
        protected void Fire()
        {
            if (rockets.Count > 0)
            {
                rockets.Dequeue().Launch();
            }
        }

        /// <summary>
        /// Reload rocket
        /// </summary>
        protected void ReloadRocket()
        {
            var rocket = (Rocket)Activator.CreateInstance(rocketType);
            rocket.myTank = this;
            rockets.Enqueue(rocket);
            Engine.RegisterObject(rocket);
        }

        /// <summary>
        /// Die
        /// </summary>
        protected void Die()
        {
            spriteRenderer.Sprites.SetColorToAll(Color.FromArgb(110, 48, 0));
            animator.AnimationFrames.SetColorToAll(Color.FromArgb(110, 48, 0));
            rigidbody.Enabled = false;
            fireAnimator.Enabled = true;

            string tankType = "";
            if (this is RedTank)
            {
                tankType = "Красный";
            }
            else
            {
                tankType = "Зелёный";
            }

            Task task = new Task(() => {
                MessageBox.Show(tankType + " танк уничтожен. GGWP");
                Engine.Stop();
            });
            task.Start();
        }
    }
}
