using System;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Graphics;

namespace Game.GameObjects
{
    /// <summary>
    /// Background class.
    /// </summary>
    public class Background : GameObject
    {
        /// <summary>
        /// Background sprite.
        /// </summary>
        private Sprite sprite;

        /// <summary>
        /// Background constructor.
        /// </summary>
        /// <param name="filename">Path to background sprite.</param>
        public Background(String filename)
        {
            var spriteRenderer = AddComponent<SpriteRenderer>();

            sprite = spriteRenderer.Sprites.AddByName(filename);

            sprite.ZOrder = -100.0f;
        }

        /// <summary>
        /// Update function.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void Update(Double deltaTime)
        {
            sprite.Width = Engine.ClientWidth;

            sprite.Height = Engine.ClientHeight;

            Position = new OpenTK.Vector2(Engine.ClientWidth / 2.0f, Engine.ClientHeight / 2.0f);
        }
    }
}
