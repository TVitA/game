using System;

namespace GameEngine.Basic
{
    /// <summary>
    /// Base class for all game components of game objects
    /// </summary>
    public abstract class GameComponent : Object, IDisposable
    {
        /// <summary>
        /// Owner of component
        /// </summary>
        private readonly GameObject owner;

        /// <summary>
        /// Define whether enabled component
        /// </summary>
        private Boolean enabled;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">Owner of component</param>
        private protected GameComponent(GameObject owner)
            : base()
        {
            this.owner = owner;
        }

        /// <summary>
        /// Getter of field 'owner'
        /// </summary>
        public GameObject Owner => owner;

        /// <summary>
        /// Getter and setter of field 'enabled'
        /// </summary>
        public Boolean Enabled
        {
            get => enabled;

            set => enabled = value;
        }

        internal virtual void OnRegisterComponent() { }

        internal abstract void CallComponent(Double deltaTime);

        /// <summary>
        /// Realization of method 'Dispose' of 'IDisposible' interface
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose overloading
        /// </summary>
        /// <param name="disposing"></param>
        private protected abstract void Dispose(Boolean disposing);

        /// <summary>
        /// Destructor
        /// </summary>
        ~GameComponent()
        {
            Dispose(false);
        }
    }
}