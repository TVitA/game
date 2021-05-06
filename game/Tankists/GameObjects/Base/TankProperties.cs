namespace Tankists
{
    /// <summary>
    /// Tank props
    /// </summary>
    public class TankProperties
    {
        /// <summary>
        /// Props ctor
        /// </summary>
        public TankProperties()
        {
            ReloadTime = 3.0f;
            EnginePower = 3000.0f;
        }

        /// <summary>
        /// Tank hp
        /// </summary>
        public virtual float Hp { get; set; }

        /// <summary>
        /// Reload time
        /// </summary>
        public virtual float ReloadTime { get; set; }

        /// <summary>
        /// Tank ammo
        /// </summary>
        public virtual int Ammo { get; set; }

        /// <summary>
        /// Tank armor resistance
        /// </summary>
        public virtual float ArmorResistance { get; set; }
        
        /// <summary>
        /// Tank engine power
        /// </summary>
        public virtual float EnginePower { get; set; }
    }
}
