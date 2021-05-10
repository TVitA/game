using System;

using GameEngine.Graphics;

namespace GameEngine.Utilities
{
    /// <summary>
    /// Contains info aboud added/removed sprite.
    /// </summary>
    public sealed class SpriteEventArgs : EventArgs
    {
        /// <summary>
        /// Added/removed sprite.
        /// </summary>
        private readonly Sprite sprite;

        /// <summary>
        /// Creates new event args that contains info about sprite.
        /// </summary>
        /// <param name="sprite">Added or removed sprite.</param>
        public SpriteEventArgs(Sprite sprite)
        {
            this.sprite = sprite;
        }

        /// <summary>
        /// Returns added/removed sprite.
        /// </summary>
        public Sprite Sprite => sprite;
    }
}
