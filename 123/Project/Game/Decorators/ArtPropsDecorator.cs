using System;

using Game.GameObjects;

namespace Game.Decorators
{
    public class ArtPropsDecorator : ArtilleryProperties
    {
        /// <summary>
        /// Properties to decorate.
        /// </summary>
        private ArtilleryProperties artilleryProperties;

        /// <summary>
        /// Decorator constructor.
        /// </summary>
        /// <param name="artilleryProperties">Artillery properties</param>
        public ArtPropsDecorator(ArtilleryProperties artilleryProperties)
        {
            this.artilleryProperties = artilleryProperties;
        }

        /// <summary>
        /// Ammo.
        /// </summary>
        public override Int32 Ammo { get => artilleryProperties.Ammo; set => artilleryProperties.Ammo = value; }

        /// <summary>
        /// Armor resistence.
        /// </summary>
        public override Single Armor => artilleryProperties.Armor;

        /// <summary>
        /// Engine power.
        /// </summary>
        public override Single EnginePower => artilleryProperties.EnginePower;

        /// <summary>
        /// Artillery health points.
        /// </summary>
        public override Single Health { get => artilleryProperties.Health; set => artilleryProperties.Health = value; }

        /// <summary>
        /// Missle reload time.
        /// </summary>
        public override Single ReloadTime => artilleryProperties.ReloadTime;
    }
}
