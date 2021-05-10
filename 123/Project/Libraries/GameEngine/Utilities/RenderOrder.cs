using System;

using GameEngine.Basic;
using GameEngine.Graphics;

namespace GameEngine.Utilities
{
    /// <summary>
    /// Render order class.
    /// </summary>
    internal readonly struct RenderOrder : IComparable<RenderOrder>
    {
        /// <summary>
        /// Owner.
        /// </summary>
        public readonly GameObject owner;

        /// <summary>
        /// Sprite.
        /// </summary>
        public readonly Sprite sprite;

        /// <summary>
        /// Render order ctor.
        /// </summary>
        /// <param name="sprite">Sprite.</param>
        /// <param name="owner">Owner.</param>
        public RenderOrder(Sprite sprite, GameObject owner)
        {
            this.owner = owner;

            this.sprite = sprite;
        }

        /// <summary>
        /// Compate two render orders.
        /// </summary>
        /// <param name="other">Second render order.</param>
        /// <returns></returns>
        public int CompareTo(RenderOrder other)
        {
            if (sprite.ZOrder > other.sprite.ZOrder)
            {
                return 1;
            }

            if (sprite.ZOrder < other.sprite.ZOrder)
            {
                return -1;
            }

            return 0;
        }
    }
}
