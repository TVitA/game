using System;

using OpenTK;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Graphics;
using GameEngine.Physics;

namespace Game.GameObjects
{
    /// <summary>
    /// Rocket base class.
    /// </summary>
    public abstract class Rocket : GameObject
    {
        /// <summary>
        /// Rocket damage.
        /// </summary>
        private Single damage;

        /// <summary>
        /// Rocket force
        /// </summary>
        private Single force = 1800.0f;

        /// <summary>
        /// Rocket force time
        /// </summary>
        private Single forceTime = 0.7f;

        /// <summary>
        /// Owner.
        /// </summary>
        private GameObject owner;

        /// <summary>
        /// Rocket sprite.
        /// </summary>
        private Sprite sprite;

        /// <summary>
        /// Sprite renderer.
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Rocket animator.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Rocket rigidbody.
        /// </summary>
        private readonly Rigidbody rigidbody;

        /// <summary>
        /// Rocket explosion animator.
        /// </summary>
        private readonly Animator explosionAnimator;

        /// <summary>
        /// Is launch rocket.
        /// </summary>
        private Boolean isLaunched;

        /// <summary>
        /// Rocket constructor.
        /// </summary>
        /// <param name="spriteFilename">Image filename.</param>
        public Rocket(String spriteFilename)
        {
            damage = 30.0f;

            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();
            animator = AddComponent<Animator>();
            explosionAnimator = AddComponent<Animator>();

            sprite = SpriteRenderer.Sprites.AddByName(spriteFilename);
            sprite.ZOrder = 10.13f;
            sprite.Scale = new Vector2(0.6f, 0.6f);

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-43, -7), new Vector2(43, 7)));
            rigidbody.Colliders[0].IsTrigger = true;
            rigidbody.OnTriggerEnter += Rigidbody_OnTriggerEnter;
            rigidbody.Enabled = false;

            animator.Delay = 0.01667f;

            animator.AnimationFrames.AddRange(new[]
            {
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 324, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 261, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 196, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 132, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 69, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(20, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(84, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(148, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(212, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(276, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(340, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(404, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(468, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(532, 4, 26, 52)),
                new Sprite(@"Textures\Others\JetStream.png", new System.Drawing.Rectangle(596, 4, 26, 52))
            });

            animator.AnimationFrames.SetZOrderToAll(10.121f);

            animator.Enabled = false;

            explosionAnimator.AnimationFrames.AddRange(new[]
            {
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(4, 203, 186, 180)),
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(195, 203, 186, 180)),
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(386, 203, 186, 180)),
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(577, 203, 186, 180)),
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(766, 203, 186, 180)),

                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(4, 3, 186, 180)),
                new Sprite(@"Textures\Others\Explosion.png", new System.Drawing.Rectangle(195, 3, 186, 180))
            });

            explosionAnimator.AnimationFrames.SetZOrderToAll(15.0f);
            explosionAnimator.Delay = 0.1f;
            explosionAnimator.OnAnimationEnded += Boom;
            explosionAnimator.Enabled = false;
            explosionAnimator.AnimationFrames.SetScaleToAll(1.3f);

            rigidbody.Mass = 1.0f;
            rigidbody.Resistance = new Vector2(0.3f, 0.3f);
        }

        /// <summary>
        /// Returns rocket damage.
        /// </summary>
        public Single Damage => damage;

        /// <summary>
        /// Returns rocket force.
        /// </summary>
        protected Single Force => force;

        /// <summary>
        /// Returns rocket force time.
        /// </summary>
        protected Single ForceTime => forceTime;

        /// <summary>
        /// Returns owner of rocket.
        /// </summary>
        internal GameObject Owner
        {
            get => owner;

            set => owner = value;
        }

        /// <summary>
        /// Ruturns rocket sprite.
        /// </summary>
        protected Sprite Sprite => sprite;

        /// <summary>
        /// Returns rocket sprite renderer.
        /// </summary>
        internal SpriteRenderer SpriteRenderer => spriteRenderer;

        /// <summary>
        /// Returns rocket animator.
        /// </summary>
        protected Animator Animator => animator;

        /// <summary>
        /// OnTriggerEnter event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">EventArgs.</param>
        private void Rigidbody_OnTriggerEnter(Object sender, TriggerEnterEventArgs e)
        {
            if (!e.Collider.IsTrigger && e.Collider.Rigidbody.Owner != owner)
            {
                PlayBoom();
            }
        }

        /// <summary>
        /// Call when rocket кeaches the goal.
        /// </summary>
        private void PlayBoom()
        {
            SpriteRenderer.Enabled = false;
            animator.Enabled = false;
            rigidbody.Enabled = false;
            explosionAnimator.Enabled = true;
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (isLaunched && rigidbody.Velocity != Vector2.Zero)
            {
                if (sprite.FlipX)
                {
                    Rotation = (Single)MathHelper.RadiansToDegrees(Math.Atan2(-rigidbody.Velocity.Y, -rigidbody.Velocity.X));
                }
                else
                {
                    Rotation = (Single)MathHelper.RadiansToDegrees(Math.Atan2(rigidbody.Velocity.Y, rigidbody.Velocity.X));
                }

                if (forceTime > 0.0f)
                {
                    forceTime -= (Single)deltaTime;
                }
                else
                {
                    rigidbody.Force = Vector2.Zero;
                    animator.Enabled = false;
                    rigidbody.UseGravity = true;
                }

                CheckBounds();
            }
        }

        /// <summary>
        /// Launch rocket method.
        /// </summary>
        public void Launch()
        {
            isLaunched = true;

            var dir = new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(Rotation)), (float)Math.Sin(MathHelper.DegreesToRadians(Rotation)));

            if (sprite.FlipX)
            {
                dir *= -1.0f;
            }

            rigidbody.Enabled = true;
            rigidbody.Force = dir * force;
            rigidbody.UseGravity = false;
            animator.Enabled = true;
        }

        /// <summary>
        /// Rocket explosion.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">EventArgs.</param>
        public void Boom(Object sender, EventArgs e)
        {
            Destroy();
        }

        /// <summary>
        /// Check bounds.
        /// </summary>
        private void CheckBounds()
        {
            if (Position.Y < -10)
            {
                PlayBoom();
            }

            if (Position.X > Engine.ClientWidth + 10 || Position.X < -10)
            {
                Destroy();
            }
        }
    }
}
