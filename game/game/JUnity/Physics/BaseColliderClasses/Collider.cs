using System;
using System.Collections.Generic;

namespace JUnity.Physics.BaseColliderClasses
{
    /// <summary>
    /// Base collider class
    /// </summary>
    public abstract class Collider : IDisposable
    {
        internal void Register(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
            allColliders.Add(this);
            rigidbody.owner.OnRotationChanged += (o, x) =>
            {
                Rotate(x.deltaRotation);
                aabb = GenerateAABB();
            };
            Rotate(rigidbody.owner.Rotation);
            aabb = GenerateAABB();
        }

        /// <summary>
        /// Is trigger collider
        /// </summary>
        public bool IsTrigger { get; set; }

        /// <summary>
        /// Is static collider
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Collider width
        /// </summary>
        public float Width
        {
            get
            {
                return aabb.max.X - aabb.min.X;
            }
        }

        /// <summary>
        /// Collider height
        /// </summary>
        public float Heigth
        {
            get
            {
                return aabb.max.Y - aabb.min.Y;
            }
        }

        internal static readonly List<Collider> allColliders = new List<Collider>();

        /// <summary>
        /// Rotate collider
        /// </summary>
        /// <param name="angle"></param>
        protected abstract void Rotate(float angle);

        private protected abstract AxisAlignedBoundingBox GenerateAABB();

        /// <summary>
        /// Collider rigidbody component
        /// </summary>
        public Rigidbody Rigidbody { get; protected set; }

        private protected AxisAlignedBoundingBox aabb;

        internal abstract void Draw();

        /// <summary>
        /// Resolve collision method
        /// </summary>
        /// <param name="other"></param>
        protected abstract void ResolveCollision(Collider other);

        internal void CheckCollision(Collider other)
        {
            if (Rigidbody.Enabled && other.Rigidbody.Enabled && aabb.IsToching(other.aabb))
            {
                ResolveCollision(other);
            }
        }

        bool isDisposed = false;

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    allColliders.Remove(this);
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Collider()
        {
            Dispose(false);
        }
    }
}
