using System;

namespace GameEngine.Basic
{
    public class RotationEventArgs : EventArgs
    {
        private readonly Single oldAngle;
        private readonly Single deltaAngle;

        public RotationEventArgs(Single oldAngle, Single deltaAngle)
            : base()
        {
            this.oldAngle = oldAngle;
            this.deltaAngle = deltaAngle;
        }

        public Single OldAngle => oldAngle;

        public Single DeltaAngle => deltaAngle;
    }
}