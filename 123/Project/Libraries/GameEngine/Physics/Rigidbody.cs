using System;

using OpenTK;

using GameEngine.Basic;
using GameEngine.Physics.BaseColliderClasses;
using GameEngine.Utilities;


namespace GameEngine.Physics
{
    /// <summary>
    /// Rigidbody component.
    /// </summary>
    public sealed class Rigidbody : GameComponent, IUniqueComponent, IFixedUpdatableComponent
    {
        /// <summary>
        /// On trigger enter event.
        /// </summary>
        public event EventHandler<TriggerEnterEventArgs> OnTriggerEnter;

        /// <summary>
        /// Gravity force.
        /// </summary>
        private static Single gravity = 500.0f;

        /// <summary>
        /// Rigidbody colliders.
        /// </summary>
        private ColliderCollection colliders;

        /// <summary>
        /// Mass of game object.
        /// </summary>
        private Single mass;

        /// <summary>
        /// Is rigidbody should use gravity.
        /// </summary>
        private Boolean useGravity;

        /// <summary>
        /// Game object velosity.
        /// </summary>
        private Vector2 velocity;
        /// <summary>
        /// Game object force.
        /// </summary>
        private Vector2 force;
        /// <summary>
        /// Air resistance.
        /// </summary>
        private Vector2 resistance;

        /// <summary>
        /// Rigidbody constructor.
        /// </summary>
        /// <param name="owner">Owner of this game component.</param>
        internal Rigidbody(GameObject owner)
            : base(owner)
        {
            mass = 1.0f;

            useGravity = true;

            colliders = new ColliderCollection();
        }

        /// <summary>
        /// Returns gravity value.
        /// </summary>
        public static Single Gravity
        {
            get => gravity;

            set => gravity = value;
        }

        /// <summary>
        /// Returns collection of colliders.
        /// </summary>
        public ColliderCollection Colliders => colliders;

        /// <summary>
        /// Returns mass of game object.
        /// </summary>
        public Single Mass
        {
            get => mass;

            set
            {
                if (value <= 0.0f)
                {
                    throw new ArgumentOutOfRangeException();
                }

                mass = value;
            }
        }

        /// <summary>
        /// Returns value which defines whether should use gravity by rigidbody.
        /// </summary>
        public Boolean UseGravity
        {
            get => useGravity;

            set => useGravity = value;
        }

        /// <summary>
        /// Returns game object velocity.
        /// </summary>
        public Vector2 Velocity
        {
            get => velocity;

            set => velocity = value;
        }

        /// <summary>
        /// Returns game object force.
        /// </summary>
        public Vector2 Force
        {
            get => force;

            set => force = value;
        }

        /// <summary>
        /// Returns air resistance.
        /// </summary>
        public Vector2 Resistance
        {
            get => resistance;

            set => resistance = value;
        }

        /// <summary>
        /// Call action when game component register.
        /// </summary>
        internal override void OnRegisterComponent()
        {
            foreach (Collider collider in Colliders)
            {
                collider.Register(this);
            }
        }

        /// <summary>
        /// Refactoring render orders by this component.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        internal override void CallComponent(Double deltaTime)
        {
            Vector2 attachedForce = force;

            if (useGravity)
            {
                attachedForce.Y -= gravity * mass;
            }

            attachedForce -= resistance * velocity;

            velocity += attachedForce / mass * (Single)deltaTime;

            Owner.Position += velocity * (Single)deltaTime;

            foreach (Collider thisCollider in colliders)
            {
                foreach (Collider collider in Collider.AllColliders)
                {
                    if (!ReferenceEquals(thisCollider, collider))
                    {
                        thisCollider.CheckCollision(collider);
                    }
                }
            }
        }

        /// <summary>
        /// Call OnTriggerEnter event handlers.
        /// </summary>
        /// <param name="collider">Collider.</param>
        internal void TriggerNotify(Collider collider)
        {
            OnTriggerEnter?.Invoke(this, new TriggerEnterEventArgs(collider));
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        /// <param name="disposing">Releasing managed resources.</param>
        private protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                foreach (Collider collider in Colliders)
                {
                    collider.Dispose();
                }
            }
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Rigidbody()
        {
            Dispose(false);
        }
    }
}
