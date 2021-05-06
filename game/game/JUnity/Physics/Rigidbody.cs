using JUnity.Basic;
using JUnity.Physics.BaseColliderClasses;
using JUnity.Utilities;
using OpenTK;
using System;

namespace JUnity.Physics
{
    /// <summary>
    /// Rigidbody component
    /// </summary>
    public sealed class Rigidbody : GameComponent, IUniqueComponent, IFixedUpdatableComponent
    {
        /// <summary>
        /// Gravity force
        /// </summary>
        public static float gravity = 500.0f;

        internal Rigidbody(GameObject @object)
            : base(@object)
        {
            Mass = 1.0f;
            UseGravity = true;
            Colliders = new ColliderCollection();
        }

        /// <summary>
        /// Is rigidbody should use gravity
        /// </summary>
        public bool UseGravity { get; set; }

        /// <summary>
        /// Object mass
        /// </summary>
        public float Mass
        {
            get => mass;
            set
            {
                if (value > 0.0f)
                {
                    mass = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Mass", "Mass must be greater then 0");
                }
            }
        }

        /// <summary>
        /// Object velosity
        /// </summary>
        public Vector2 velocity;

        /// <summary>
        /// Object force
        /// </summary>
        public Vector2 force;

        /// <summary>
        /// Air resistance
        /// </summary>
        public Vector2 resistance;

        /// <summary>
        /// Rigidbody colliders
        /// </summary>
        public ColliderCollection Colliders { get; set; }

        /// <summary>
        /// On trigger enter event
        /// </summary>
        public event EventHandler<TriggerEnterEventArgs> OnTriggerEnter;

        private float mass;

        internal override void OnRegisterComponent()
        {
            foreach (var collider in Colliders)
            {
                collider.Register(this);
            }
        }

        internal override void CallComponent(double time)
        {
            Vector2 attachedForce = force;
            if (UseGravity)
            {
                attachedForce.Y -= gravity * Mass;
            }

            attachedForce -= resistance * velocity;
            velocity += attachedForce / Mass * (float)time;
            owner.position += velocity * (float)time;

            foreach (var myCollider in Colliders)
            {
                foreach (var collider in Collider.allColliders)
                {
                    if (!ReferenceEquals(myCollider, collider))
                    {
                        myCollider.CheckCollision(collider);
                    }
                }
            }
        }

        internal void TriggerNotify(Collider other)
        {
            OnTriggerEnter?.Invoke(this, new TriggerEnterEventArgs(other));
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var item in Colliders)
                {
                    item.Dispose();
                }
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Rigidbody()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// Trigger event arguments
    /// </summary>
    public class TriggerEnterEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="other"></param>
        public TriggerEnterEventArgs(Collider other)
        {
            this.other = other;
        }

        /// <summary>
        /// Others collider
        /// </summary>
        public readonly Collider other;
    }
}
