using System;

using GameEngine.Physics.BaseColliderClasses;

namespace GameEngine.Physics
{
    /// <summary>
    /// Trigger enter event arguments class.
    /// </summary>
    public class TriggerEnterEventArgs : EventArgs
    {
        /// <summary>
        /// Collider.
        /// </summary>
        private readonly Collider collider;

        /// <summary>
        /// TriggerEnterEventArgs constructor.
        /// </summary>
        /// <param name="collider">Collider.</param>
        public TriggerEnterEventArgs(Collider collider)
        {
            this.collider = collider;
        }

        /// <summary>
        /// Returns collider.
        /// </summary>
        public Collider Collider => collider;
    }
}
