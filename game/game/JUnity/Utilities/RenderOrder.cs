using JUnity.Basic;
using JUnity.Graphics;
using OpenTK;
using System;

namespace JUnity.Utilities
{
    internal readonly struct RenderOrder : IComparable<RenderOrder>
    {
        public RenderOrder(Sprite sprite, GameObject owner)
        {
            this.owner = owner;
            this.sprite = sprite;
        }

        public readonly GameObject owner;

        public readonly Sprite sprite;

        public int CompareTo(RenderOrder other)
        {
            if (sprite.ZOrder > other.sprite.ZOrder)
            {
                return 1;
            }
            else if (sprite.ZOrder < other.sprite.ZOrder)
            {
                return -1;
            }
            return 0;
        }
    }
}
