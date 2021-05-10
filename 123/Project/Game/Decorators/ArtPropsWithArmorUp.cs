using System;

using Game.GameObjects;

namespace Game.Decorators
{
    /// <summary>
    /// Armor decorator.
    /// </summary>
    public class ArtPropsWithArmorUp : ArtPropsDecorator
    {
        /// <summary>
        /// Armor decorator constructor.
        /// </summary>
        /// <param name="artilleryProperties">Props to decorate.</param>
        public ArtPropsWithArmorUp(ArtilleryProperties artilleryProperties)
            : base(artilleryProperties)
        { }

        /// <summary>
        /// Decorated armor resistance.
        /// </summary>
        public override Single Armor => base.Armor + 0.2f;
    }
}
