using System;

namespace GameEngine.Basic
{
    /// <summary>
    /// Contains info about rotation of game object.
    /// </summary>
    public class RotationEventArgs : EventArgs
    {
        /// <summary>
        /// Old angle.
        /// </summary>
        private readonly Single oldAngle;
        /// <summary>
        /// Delta between new and old rotations.
        /// </summary>
        private readonly Single deltaAngle;

        /// <summary>
        /// RotationEventArgs constructor.
        /// </summary>
        /// <param name="oldAngle">Old angle.</param>
        /// <param name="deltaAngle">Delta between new and old rotations.</param>
        public RotationEventArgs(Single oldAngle, Single deltaAngle)
            : base()
        {
            this.oldAngle = oldAngle;
            this.deltaAngle = deltaAngle;
        }

        /// <summary>
        /// Returns old angle.
        /// </summary>
        public Single OldAngle => oldAngle;

        /// <summary>
        /// Returns delta between new and old rotations.
        /// </summary>
        public Single DeltaAngle => deltaAngle;
    }
}