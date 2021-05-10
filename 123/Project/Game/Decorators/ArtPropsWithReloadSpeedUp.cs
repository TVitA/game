using System;

using Game.GameObjects;

namespace Game.Decorators
{
    /// <summary>
    /// Reload speed decorator.
    /// </summary>
    public class ArtPropsWithReloadSpeedUp : ArtPropsDecorator
    {
        /// <summary>
        /// Decorator constructor.
        /// </summary>
        /// <param name="artilleryProperties">Props to decorate.</param>
        public ArtPropsWithReloadSpeedUp(ArtilleryProperties artilleryProperties)
            : base(artilleryProperties)
        { }

        /// <summary>
        /// Overloaded reload time prop.
        /// </summary>
        public override Single ReloadTime { get => base.ReloadTime - (base.ReloadTime / 100.0f * 20.0f); }
    }
}
