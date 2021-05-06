using System;

namespace JUnity.Basic
{
    /// <summary>
    /// Base component class
    /// </summary>
    public abstract class GameComponent : IDisposable
    {
        /// <summary>
        /// Component owner object
        /// </summary>
        public readonly GameObject owner;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="object">Game object</param>
        internal GameComponent(GameObject @object)
        {
            owner = @object;
            Enabled = true;
        }

        /// <summary>
        /// Is component enable
        /// </summary>
        public bool Enabled { get; set; }

        internal virtual void OnRegisterComponent() { }

        internal abstract void CallComponent(double deltaTime);

        /// <summary>
        /// Dispose overloading
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
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Destructor
        /// </summary>
        ~GameComponent()
        {
            Dispose(false);
        }
    }

    internal interface IUniqueComponent { }

    internal interface IFixedUpdatableComponent { }
}
