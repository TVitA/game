using System;
using System.Collections.Generic;

namespace GameEngine.Physics.BaseColliderClasses
{
    /// <summary>
    /// Base collider class.
    /// </summary>
    public abstract class Collider : Object, IDisposable
    {
        /// <summary>
        /// List of all collider.
        /// </summary>
        private static readonly List<Collider> allColliders = new List<Collider>();

        /// <summary>
        /// Checker of using dispose method.
        /// </summary>
        private Boolean isDisposed;

        /// <summary>
        /// Owner of this collider.
        /// </summary>
        private Rigidbody rigidbody;

        /// <summary>
        /// Axis aligned bounding box.
        /// </summary>
        private AxisAlignedBoundingBox aabb;

        /// <summary>
        /// Is trigger collider.
        /// </summary>
        private Boolean isTrigger;

        /// <summary>
        /// Is static collider.
        /// </summary>
        private Boolean isStatic;

        /// <summary>
        /// Collider constructor.
        /// </summary>
        public Collider()
            : base()
        {
            isDisposed = false;
        }

        /// <summary>
        /// Returns list of all colliders.
        /// </summary>
        public static List<Collider> AllColliders => allColliders;

        /// <summary>
        /// Returns owner of this collider.
        /// </summary>
        public Rigidbody Rigidbody => rigidbody;

        /// <summary>
        /// Returns axis aligned bounding box.
        /// </summary>
        private protected AxisAlignedBoundingBox AABB => aabb;

        /// <summary>
        /// Returns value that defines whether this is trigger collider.
        /// </summary>
        public Boolean IsTrigger
        {
            get => isTrigger;

            set => isTrigger = value;
        }

        /// <summary>
        /// Returns value that defines whether this is static collider.
        /// </summary>
        public Boolean IsStatic
        {
            get => isStatic;

            set => isStatic = value;
        }

        /// <summary>
        /// Collider width.
        /// </summary>
        public Single Width => aabb.Max.X - aabb.Min.X;

        /// <summary>
        /// Collider height.
        /// </summary>
        public Single Heigth => aabb.Max.Y - aabb.Min.Y;

        /// <summary>
        /// Register method.
        /// </summary>
        /// <param name="rigidbody">Onwer.</param>
        internal void Register(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;

            allColliders.Add(this);

            rigidbody.Owner.ChangeRotation += (sender, e) =>
            {
                Rotate(e.DeltaAngle);

                aabb = GenerateAABB();
            };

            Rotate(rigidbody.Owner.Rotation);

            aabb = GenerateAABB();
        }


        /// <summary>
        /// Rotate collider.
        /// </summary>
        /// <param name="angle">Angle.</param>
        protected abstract void Rotate(Single angle);

        /// <summary>
        /// Generate axis aligned bounding box for this collider.
        /// </summary>
        /// <returns>AxisAlignedBoundingBox object.</returns>
        private protected abstract AxisAlignedBoundingBox GenerateAABB();

        /// <summary>
        /// Draw collider.
        /// </summary>
        internal abstract void Draw();

        /// <summary>
        /// Resolve collision method.
        /// </summary>
        /// <param name="collider">Collider.</param>
        protected abstract void ResolveCollision(Collider collider);

        /// <summary>
        /// Checker of collisions between two colliders.
        /// </summary>
        /// <param name="collider">Second collider.</param>
        internal void CheckCollision(Collider collider)
        {
            if (rigidbody.Enabled && collider.rigidbody.Enabled && aabb.IsTouching(collider.aabb))
            {
                ResolveCollision(collider);
            }
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        /// <param name="isDisposing">Releasing managed resources.</param>
        public void Dispose(Boolean isDisposing)
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
        /// Destructor.
        /// </summary>
        ~Collider()
        {
            Dispose(false);
        }
    }
}
