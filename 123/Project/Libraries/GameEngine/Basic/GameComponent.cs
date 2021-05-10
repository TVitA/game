using System;

namespace GameEngine.Basic
{
    /// <summary>
    /// Base component class.
    /// </summary>
    public abstract class GameComponent : IDisposable
    {
        /// <summary>
        /// Component owner object.
        /// </summary>
        private readonly GameObject owner;

        /// <summary>
        /// Is component enable.
        /// </summary>
        private Boolean enabled;

        /// <summary>
        /// Game component constructor.
        /// </summary>
        /// <param name="owner">Onwer of game component.</param>
        internal GameComponent(GameObject owner)
        {
            this.owner = owner;

            enabled = true;
        }

        /// <summary>
        /// Returns owner of this component.
        /// </summary>
        public GameObject Owner => owner;

        /// <summary>
        /// Returns whether enabled this component.
        /// </summary>
        public Boolean Enabled
        { 
            get => enabled;
            
            set => enabled = value;
        }

        /// <summary>
        /// Call when registered this component.
        /// </summary>
        internal virtual void OnRegisterComponent() { }

        /// <summary>
        /// Call by engine every frame for concrete component.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        internal abstract void CallComponent(Double deltaTime);

        /// <summary>
        /// Dispose overloading.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose overloading.
        /// </summary>
        /// <param name="disposing">Clean up managed code.</param>
        private protected abstract void Dispose(Boolean disposing);

        /// <summary>
        /// Destructor.
        /// </summary>
        ~GameComponent()
        {
            Dispose(false);
        }
    }
}
