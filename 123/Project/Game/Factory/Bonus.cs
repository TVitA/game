using System;

using OpenTK;

using GameEngine.Basic;
using GameEngine.Graphics;
using GameEngine.Physics;

using Game.GameObjects;

namespace Game.Factory
{
    public abstract class Bonus : GameObject
    {
        /// <summary>
        /// Bonus rigidbody.
        /// </summary>
        private Rigidbody rigidbody;

        /// <summary>
        /// bonus sprite renderer.
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Bonus constructor.
        /// </summary>
        /// <param name="pathToEmblem">Path to emblem.</param>
        /// <param name="position">Bonus position.</param>
        public Bonus(String pathToEmblem, Vector2 position)
            : base()
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-20.0f, -20.0f), new Vector2(20.0f, 20.0f)));
            rigidbody.Colliders[0].IsTrigger = true;
            rigidbody.Resistance = new Vector2(rigidbody.Resistance.X, 0.5f);
            rigidbody.Mass = 1.0f;
            rigidbody.OnTriggerEnter += Rigidbody_OnTriggerEnter;

            Position = position;

            var sprite = spriteRenderer.Sprites.AddByName(@"Textures\Others\Box.png");

            sprite.Scale = new Vector2(0.15f, 0.15f);

            var emblem = spriteRenderer.Sprites.AddByName(pathToEmblem);

            emblem.Width = 30;
            emblem.Height = 30;
            emblem.ZOrder = 0.1f;
        }

        /// <summary>
        /// OnTriggerEnter event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">TriggerEnterEventArgs.</param>
        private void Rigidbody_OnTriggerEnter(Object sender, TriggerEnterEventArgs e)
        {
            var artillery = e.Collider.Rigidbody.Owner as Artillery;

            if (artillery != null)
            {
                Decorate(artillery);

                Destroy();
            }
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (Position.Y - rigidbody.Colliders[0].Heigth / 2.0f < 0.0f)
            {
                Position = new Vector2(Position.X, rigidbody.Colliders[0].Heigth / 2.0f);

                rigidbody.UseGravity = false;
            }
        }

        /// <summary>
        /// Decorate method.
        /// </summary>
        /// <param name="artillery">Artillery to decorate.</param>
        public abstract void Decorate(Artillery artillery);
    }
}
