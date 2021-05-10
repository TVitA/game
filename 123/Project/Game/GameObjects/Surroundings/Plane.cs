using System;

using OpenTK;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Graphics;

using Game.Factory;

namespace Game.GameObjects
{
    /// <summary>
    /// Plane class.
    /// </summary>
    public class Plane : GameObject
    {
        /// <summary>
        /// Plane sprite.
        /// </summary>
        Sprite sprite;
        /// <summary>
        /// Plane sprite renderer.
        /// </summary>
        SpriteRenderer spriteRenderer;

        /// <summary>
        /// Plane speed.
        /// </summary>
        Single speed;
        /// <summary>
        /// Plane drop point.
        /// </summary>
        Single dropPoint;

        /// <summary>
        /// Is first.
        /// </summary>
        Boolean isFirst;
        /// <summary>
        /// Is dropped.
        /// </summary>
        Boolean isDropped;
        /// <summary>
        /// Is ammo.
        /// </summary>
        Boolean isAmmo;

        /// <summary>
        /// Bonus factory.
        /// </summary>
        BonusFactory factory;

        public Plane()
            : base()
        {
            isFirst = true;

            factory = new BonusFactory();

            speed = 500.0f;

            spriteRenderer = AddComponent<SpriteRenderer>();

            sprite = spriteRenderer.Sprites.AddByName(@"Textures\Others\Plane.png");

            sprite.ZOrder = 10.129f;
        }

        /// <summary>
        /// Check bounds.
        /// </summary>
        private void CheckBounds()
        {
            if (Position.X + sprite.Width / 2.0f < -10.0f)
            {
                Destroy();
            }
        }

        /// <summary>
        /// Fixed update.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (isFirst)
            {
                isFirst = false;

                Position = new Vector2(Engine.ClientWidth + sprite.Width, Position.Y);
            }

            var x = Position.X - speed * (Single)deltaTime;

            var y = Engine.ClientHeight - sprite.Height / 2.0f - 10.0f;

            Position = new Vector2(x, y);

            CheckBounds();

            Send();
        }

        /// <summary>
        /// Send.
        /// </summary>
        private void Send()
        {
            if (Position.X <= dropPoint && !isDropped)
            {
                if (isAmmo)
                {
                    Engine.RegisterObject(factory.GetAmmoBox(Position));
                }
                else
                {
                    Engine.RegisterObject(factory.GetRandomBox(Position));
                }

                isDropped = true;
            }
        }

        /// <summary>
        /// Send present.
        /// </summary>
        /// <param name="artillery">Artillery.</param>
        public static void SendPresent(Artillery artillery)
        {
            var plane = new Plane();

            plane.dropPoint = artillery.Position.X;

            Engine.RegisterObject(plane);
        }

        /// <summary>
        /// Send ammo.
        /// </summary>
        /// <param name="artillery"></param>
        public static void SendAmmo(Artillery artillery)
        {
            var plane = new Plane()
            {
                isAmmo = true
            };

            plane.dropPoint = artillery.Position.X;

            Engine.RegisterObject(plane);
        }
    }
}
