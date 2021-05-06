using OpenTK;
using System;

namespace JUnity.Physics.BaseColliderClasses
{
    internal sealed class AxisAlignedBoundingBox
    {
        internal Vector2 min;
        internal Vector2 max;

        private Collider parentCollider;

        internal AxisAlignedBoundingBox(Collider owner, Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
            this.parentCollider = owner;
        }

        internal bool IsToching(AxisAlignedBoundingBox other)
        {
            if (max.X + parentCollider.Rigidbody.owner.position.X < other.min.X + other.parentCollider.Rigidbody.owner.position.X || min.X + parentCollider.Rigidbody.owner.position.X > other.max.X + other.parentCollider.Rigidbody.owner.position.X) return false;
            if (max.Y + parentCollider.Rigidbody.owner.position.Y < other.min.Y + other.parentCollider.Rigidbody.owner.position.Y || min.Y + parentCollider.Rigidbody.owner.position.Y > other.max.Y + other.parentCollider.Rigidbody.owner.position.Y) return false;
            return true;
        }
    }
}
