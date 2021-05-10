using System;

using OpenTK;

namespace GameEngine.Physics.BaseColliderClasses
{
    /// <summary>
    /// AxisAlignedBoundingBox class.
    /// </summary>
    internal sealed class AxisAlignedBoundingBox
    {
        /// <summary>
        /// Owner.
        /// </summary>
        private Collider owner;

        /// <summary>
        /// Min.
        /// </summary>
        private Vector2 min;
        /// <summary>
        /// Max.
        /// </summary>
        private Vector2 max;

        /// <summary>
        /// AxisAlignedBoundingBox constructor.
        /// </summary>
        /// <param name="owner">Onwer.</param>
        /// <param name="min">MIn.</param>
        /// <param name="max">Max.</param>
        internal AxisAlignedBoundingBox(Collider owner, Vector2 min, Vector2 max)
        {
            this.owner = owner;

            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Returns min.
        /// </summary>
        internal Vector2 Min => min;

        /// <summary>
        /// Returns max.
        /// </summary>
        internal Vector2 Max => max;

        /// <summary>
        /// Defines touching between two axis aligned bounding boxes.
        /// </summary>
        /// <param name="aabb">Other axis aligned bounding box.</param>
        /// <returns>True is touching, else false.</returns>
        internal Boolean IsTouching(AxisAlignedBoundingBox aabb)
        {
            if (max.X + owner.Rigidbody.Owner.Position.X < aabb.min.X + aabb.owner.Rigidbody.Owner.Position.X
                || min.X + owner.Rigidbody.Owner.Position.X > aabb.max.X + aabb.owner.Rigidbody.Owner.Position.X)
            {
                return false;
            }

            if (max.Y + owner.Rigidbody.Owner.Position.Y < aabb.min.Y + aabb.owner.Rigidbody.Owner.Position.Y
                || min.Y + owner.Rigidbody.Owner.Position.Y > aabb.max.Y + aabb.owner.Rigidbody.Owner.Position.Y)
            {
                return false;
            }
            
            return true;
        }
    }
}
