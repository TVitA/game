using JUnity;
using JUnity.Basic;
using JUnity.Graphics;
using JUnity.Physics;
using OpenTK;
using System;

namespace Tankists.GameObjects
{
    /// <summary>
    /// Rocket base class
    /// </summary>
    public abstract class Rocket : GameObject
    {
        /// <summary>
        /// Rocket damage
        /// </summary>
        public float Damage { get; protected set; }

        internal SpriteRenderer SpriteRenderer { get; private set; }
        internal GameObject myTank;

        /// <summary>
        /// Rocket sprite
        /// </summary>
        protected Sprite sprite;

        /// <summary>
        /// Rocket animator
        /// </summary>
        protected Animator animator;

        private readonly Rigidbody rigidbody;
        private readonly Animator explosionAnimator;
        
        /// <summary>
        /// Rocket force
        /// </summary>
        protected float force = 1800.0f;

        /// <summary>
        /// Rocket force time
        /// </summary>
        protected float forceTime = 0.7f;

        private bool isLaunched;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="spriteFilename">Image filename</param>
        public Rocket(string spriteFilename)
        {
            Damage = 30.0f;

            SpriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();
            animator = AddComponent<Animator>();
            explosionAnimator = AddComponent<Animator>();

            sprite = SpriteRenderer.Sprites.AddByName(spriteFilename);
            sprite.ZOrder = 10.13f;
            sprite.scale = new Vector2(0.6f, 0.6f);

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-43, -7), new Vector2(43, 7)));
            rigidbody.Colliders[0].IsTrigger = true;
            rigidbody.OnTriggerEnter += Rigidbody_OnTriggerEnter;
            rigidbody.Enabled = false;

            animator.Delay = 0.01667f;
            animator.AnimationFrames.AddRange(new[]
            {
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 324, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 261, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 196, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 132, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 69, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(20, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(84, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(148, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(212, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(276, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(340, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(404, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(468, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(532, 4, 26, 52)),
                new Sprite(@"Textures\Same\JetStream.png", new System.Drawing.Rectangle(596, 4, 26, 52))
            });
            animator.AnimationFrames.SetZOrderToAll(10.121f);
            animator.Enabled = false;

            explosionAnimator.AnimationFrames.AddRange(new[]
            {
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(4, 203, 186, 180)),
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(195, 203, 186, 180)),
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(386, 203, 186, 180)),
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(577, 203, 186, 180)),
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(766, 203, 186, 180)),

                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(4, 3, 186, 180)),
                new Sprite(@"Textures\Same\Explosion.png", new System.Drawing.Rectangle(195, 3, 186, 180))
            });
            explosionAnimator.AnimationFrames.SetZOrderToAll(15.0f);
            explosionAnimator.Delay = 0.1f;
            explosionAnimator.OnAnimationEnded += Boom;
            explosionAnimator.Enabled = false;
            explosionAnimator.AnimationFrames.SetScaleToAll(1.3f);

            rigidbody.Mass = 1.0f;
            rigidbody.resistance = new Vector2(0.3f, 0.3f);
        }

        private void Rigidbody_OnTriggerEnter(object sender, TriggerEnterEventArgs e)
        {
            if (!e.other.IsTrigger && e.other.Rigidbody.owner != myTank)
            {
                PlayBoom();
            }
        }

        private void PlayBoom()
        {
            SpriteRenderer.Enabled = false;
            animator.Enabled = false;
            rigidbody.Enabled = false;
            explosionAnimator.Enabled = true;
        }

        /// <summary>
        /// Fixed update method
        /// </summary>
        /// <param name="deltaTime">Time between frames</param>
        public override void FixedUpdate(double deltaTime)
        {
            if (isLaunched && rigidbody.velocity != Vector2.Zero)
            {
                if (sprite.FlipX)
                {
                    Rotation = (float)MathHelper.RadiansToDegrees(Math.Atan2(-rigidbody.velocity.Y, -rigidbody.velocity.X));
                }
                else
                {
                    Rotation = (float)MathHelper.RadiansToDegrees(Math.Atan2(rigidbody.velocity.Y, rigidbody.velocity.X));
                }

                if (forceTime > 0.0f)
                {
                    forceTime -= (float)deltaTime;
                }
                else
                {
                    rigidbody.force = Vector2.Zero;
                    animator.Enabled = false;
                    rigidbody.UseGravity = true;
                }

                CheckBounds();
            }
        }

        /// <summary>
        /// Launch rocket method
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
            rigidbody.force = dir * force;
            rigidbody.UseGravity = false;
            animator.Enabled = true;
        }

        /// <summary>
        /// Tank explosion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Boom(object sender, EventArgs e)
        {
            Destroy();
        }

        private void CheckBounds()
        {
            if (position.Y < -10)
            {
                PlayBoom();
            }

            if (position.X > Engine.ClientWidth + 10 || position.X < -10)
            {
                Destroy();
            }
        }
    }
}
