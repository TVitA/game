using System;

namespace Game.GameObjects
{
    /// <summary>
    /// Artillery properties class.
    /// </summary>
    public class ArtilleryProperties : Object
    {
        /// <summary>
        /// Artillery health points.
        /// </summary>
        private Single health;
        /// <summary>
        /// Artillery armor.
        /// </summary>
        private Single armor;
        /// <summary>
        /// Artillery ammo.
        /// </summary>
        private Int32 ammo;
        /// <summary>
        /// Artillery reload time.
        /// </summary>
        private Single reloadTime;
        /// <summary>
        /// Artillery engine power.
        /// </summary>
        private Single enginePower;

        /// <summary>
        /// Artillery properties constructor.
        /// </summary>
        public ArtilleryProperties()
            : base()
        {
            reloadTime = 3.0f;

            enginePower = 3000.0f;
        }

        /// <summary>
        /// Returns artillery health points.
        /// </summary>
        public virtual Single Health
        {
            get => health;

            set => health = value;
        }

        /// <summary>
        /// Returns artillery armor.
        /// </summary>
        public virtual Single Armor
        {
            get => armor;

            set => armor = value;
        }

        /// <summary>
        /// Returns artillery ammo.
        /// </summary>
        public virtual Int32 Ammo
        {
            get => ammo;

            set => ammo = value;
        }

        /// <summary>
        /// Returns artillery reload time.
        /// </summary>
        public virtual Single ReloadTime
        {
            get => reloadTime;

            set => reloadTime = value;
        }

        /// <summary>
        /// Returns artillery engine power.
        /// </summary>
        public virtual Single EnginePower
        {
            get => enginePower;

            set => enginePower = value;
        }
    }
}
