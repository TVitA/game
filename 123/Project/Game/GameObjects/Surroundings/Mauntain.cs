using System;

using OpenTK;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Graphics;
using GameEngine.Physics;
using GameEngine.Physics.BaseColliderClasses;

namespace Game.GameObjects
{
    /// <summary>
    /// Mountain class.
    /// </summary>
    public class Mountain : GameObject
    {
        /// <summary>
        /// Mountain sprite.
        /// </summary>
        private Sprite sprite;

        /// <summary>
        /// Mountain constructor.
        /// </summary>
        public Mountain()
        {
            var spriteRenderer = AddComponent<SpriteRenderer>();

            var rigidbody = AddComponent<Rigidbody>();

            rigidbody.UseGravity = false;

            rigidbody.Colliders.Add(new PolygonCollider(new[]
            {
                new Vector2(-140, -90),
                new Vector2(-140, -69),
                new Vector2(-93, 23),
                new Vector2(-75, 62),
                new Vector2(-46, 80),
                new Vector2(8, 87),
                new Vector2(92, 50),
                new Vector2(104, 33),
                new Vector2(139, -58),
                new Vector2(139, -90),
            }));

            sprite = spriteRenderer.Sprites.AddByName(@"Textures\Others\Mountain.png",
                OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest,
                OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);

            sprite.ZOrder = 14.9f;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void Update(Double deltaTime)
        {
            Position = new Vector2(Engine.ClientWidth / 2.0f, sprite.Height / 2.0f);
        }
    }
}
