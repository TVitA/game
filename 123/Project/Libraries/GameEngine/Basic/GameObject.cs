using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;

using OpenTK;

using GameEngine.Exceptions;

namespace GameEngine.Basic
{
    /// <summary>
    /// Base class for all game objects.
    /// </summary>
    public abstract class GameObject : Object
    {
        /// <summary>
        /// Change rotation event.
        /// </summary>
        internal event EventHandler<RotationEventArgs> ChangeRotation;

        /// <summary>
        /// List of components.
        /// </summary>
        private readonly List<GameComponent> components;
        /// <summary>
        /// List of fixed components.
        /// </summary>
        private readonly List<GameComponent> fixedComponents;

        /// <summary>
        /// Position.
        /// </summary>
        private Vector2 position;
        /// <summary>
        /// Rotation.
        /// </summary>
        private Single rotation;

        /// <summary>
        /// Game object constructor.
        /// </summary>
        public GameObject()
            : base()
        {
            components = new List<GameComponent>();

            fixedComponents = new List<GameComponent>();
        }

        /// <summary>
        /// Returns list of components of this game object.
        /// </summary>
        public List<GameComponent> Components => components;

        /// <summary>
        /// Returns list of fixed components of this game object.
        /// </summary>
        public List<GameComponent> FixedComponents => fixedComponents;

        /// <summary>
        /// Returns position.
        /// </summary>
        public Vector2 Position
        {
            get => position;

            set => position = value;
        }

        /// <summary>
        /// Returns rotation or set new value to rotation and then call ChangeRotation event.
        /// </summary>
        public Single Rotation
        {
            get => rotation;

            set
            {
                var deltaAngle = value - rotation;

                if (deltaAngle != 0.0f)
                {
                    OnChangeRotation(new RotationEventArgs(rotation, deltaAngle));
                }

                rotation = value;
            }
        }

        /// <summary>
        /// Call registered methods on ChangeRotation event.
        /// </summary>
        /// <param name="e">Rotation event arguments.</param>
        protected virtual void OnChangeRotation(RotationEventArgs e)
        {
            var temp = Volatile.Read(ref ChangeRotation);

            temp?.Invoke(this, e);
        }

        /// <summary>
        /// Returns game object component of TComponent type.
        /// </summary>
        /// <typeparam name="TComponent">Component type.</typeparam>
        /// <returns>Component.</returns>
        public TComponent GetComponent<TComponent>()
            where TComponent : GameComponent
        {
            foreach (GameComponent component in components)
            {
                if (component is TComponent)
                {
                    return component as TComponent;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates new component.
        /// </summary>
        /// <typeparam name="TComponent">Component type.</typeparam>
        /// <returns>Created component.</returns>
        public TComponent AddComponent<TComponent>()
            where TComponent : GameComponent
        {
            if (typeof(IUniqueComponent).IsAssignableFrom(typeof(TComponent)) && GetComponent<TComponent>() != null)
            {
                throw new DublicateComponentException();
            }

            var ctor = typeof(TComponent).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(GameObject) }, null);

            var component = (TComponent)ctor.Invoke(new[] { this });

            if (typeof(IFixedUpdatableComponent).IsAssignableFrom(typeof(TComponent)))
            {
                FixedComponents.Add(component);
            }
            else
            {
                Components.Add(component);
            }

            return component;
        }

        /// <summary>
        /// Call OnRegisterComponent of each component of this game object.
        /// </summary>
        internal void OnRegisterObject()
        {
            foreach (GameComponent component in components)
            {
                component.OnRegisterComponent();
            }

            foreach (GameComponent fixedComponent in fixedComponents)
            {
                fixedComponent.OnRegisterComponent();
            }
        }

        /// <summary>
        /// Call Update
        /// Call CallComponent of each enabled component of this game object.
        /// </summary>
        /// <param name="deltaTime">Time since last call.</param>
        internal void OnUpdate(Double deltaTime)
        {
            Update(deltaTime);

            foreach (GameComponent component in components)
            {
                if (component.Enabled)
                {
                    component.CallComponent(deltaTime);
                }
            }
        }

        /// <summary>
        /// Call FixedUpdate.
        /// Call CallComponent of each enabled fixed component of this game object.
        /// </summary>
        /// <param name="deltaTime">Time since last call.</param>
        internal void OnFixedUpdate(Double deltaTime)
        {
            FixedUpdate(deltaTime);

            foreach (GameComponent fixedComponent in FixedComponents)
            {
                if (fixedComponent.Enabled)
                {
                    fixedComponent.CallComponent(deltaTime);
                }
            }
        }

        /// <summary>
        /// Update function.
        /// </summary>
        /// <param name="deltaTime">Time since last call.</param>
        public virtual void Update(Double deltaTime) { }

        /// <summary>
        /// Fixed update function.
        /// </summary>
        /// <param name="deltaTime">Time since last call.</param>
        public virtual void FixedUpdate(Double deltaTime) { }

        /// <summary>
        /// Add this game object in queue of destroying.
        /// </summary>
        public void Destroy()
        {
            Engine.ObjectsToDelete.Enqueue(this);
        }
    }
}
