using OpenTK;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JUnity.Basic
{
    /// <summary>
    /// Base class for all game objects
    /// </summary>
    public abstract class GameObject
    {
        private readonly List<GameComponent> components = new List<GameComponent>();
        private readonly List<GameComponent> fixedComponents = new List<GameComponent>();
        private float rotation;

        /// <summary>
        /// Obj components
        /// </summary>
        public virtual List<GameComponent> Components { get => components; }

        /// <summary>
        /// Obj fixed components
        /// </summary>
        public virtual List<GameComponent> FixedComponents { get => fixedComponents; }

        internal void OnRegisterObject()
        {
            foreach (GameComponent component in Components)
            {
                component.OnRegisterComponent();
            }

            foreach (GameComponent component in FixedComponents)
            {
                component.OnRegisterComponent();
            }
        }

        internal void OnUpdate(double deltaTime)
        {
            Update(deltaTime);

            foreach (GameComponent component in Components)
            {
                if (component.Enabled)
                {
                    component.CallComponent(deltaTime);
                }
            }
        }

        internal void OnFixedUpdate(double time)
        {
            FixedUpdate(time);

            foreach (GameComponent component in FixedComponents)
            {
                if (component.Enabled)
                {
                    component.CallComponent(time);
                }
            }
        }

        internal event EventHandler<RotationEventArgs> OnRotationChanged;

        /// <summary>
        /// Returns obj component
        /// </summary>
        /// <typeparam name="TComponent">Component type</typeparam>
        /// <returns>Component</returns>
        public TComponent GetComponent<TComponent>()
            where TComponent : GameComponent
        {
            foreach (GameComponent component in Components)
            {
                var answ = component as TComponent;

                if (answ != null && !Equals(answ.GetType(), typeof(GameComponent)))
                {
                    return answ;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Creates new component
        /// </summary>
        /// <typeparam name="TComponent">Component type</typeparam>
        /// <returns>Created component</returns>
        public TComponent AddComponent<TComponent>() where TComponent : GameComponent
        {
            if (typeof(IUniqueComponent).IsAssignableFrom(typeof(TComponent)) && GetComponent<TComponent>() != null)
            {
                throw new DublicateComponentException();
            }

            var ctor = typeof(TComponent).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(GameObject) }, null);
            
            var tmp = (TComponent)ctor.Invoke(new[] { this });

            if (typeof(IFixedUpdatableComponent).IsAssignableFrom(typeof(TComponent)))
            {
                FixedComponents.Add(tmp);
            }
            else
            {
                Components.Add(tmp);
            }
            
            return tmp;
        }

        /// <summary>
        /// Obj position
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// Obj rotation
        /// </summary>
        public float Rotation
        {
            get => rotation;
            set
            {
                var delta = value - rotation;
                rotation = value;
                if (delta != 0.0f)
                {
                    OnRotationChanged?.Invoke(this, new RotationEventArgs(rotation, delta));
                }
            }
        }

        /// <summary>
        /// Update function
        /// </summary>
        /// <param name="deltaTime">Time since last call</param>
        public virtual void Update(double deltaTime) { }

        /// <summary>
        /// Fixed update function
        /// </summary>
        /// <param name="deltaTime">Time since last call</param>
        public virtual void FixedUpdate(double deltaTime) { }

        /// <summary>
        /// Destroys obj
        /// </summary>
        public void Destroy()
        {
            Engine.objectsToDelete.Enqueue(this);
        }
    }

    /// <summary>
    /// Dublicate component exception
    /// </summary>
    public class DublicateComponentException : ApplicationException
    {
        /// <summary>
        /// Exception ctor
        /// </summary>
        public DublicateComponentException()
            : base("Unable to create two instance of unique component")
        { }

        /// <summary>
        /// Exception ctor
        /// </summary>
        /// <param name="msg">Error message</param>
        public DublicateComponentException(string msg)
            : base(msg)
        { }
    }

    internal class RotationEventArgs
    {
        public RotationEventArgs(float newRotation, float delta)
        {
            this.newRotation = newRotation;
            deltaRotation = delta;
        }

        public readonly float newRotation;
        public readonly float deltaRotation;
    }
}
