namespace Tankists
{
    /// <summary>
    /// Decorator base class
    /// </summary>
    public abstract class Bonus : TankProperties
    {
        /// <summary>
        /// Props to decorate
        /// </summary>
        protected TankProperties tankProperties;

        /// <summary>
        /// Decorator ctor
        /// </summary>
        /// <param name="tankProperties"></param>
        public Bonus(TankProperties tankProperties)
        {
            this.tankProperties = tankProperties;
        }

        /// <summary>
        /// Ammo
        /// </summary>
        public override int Ammo { get => tankProperties.Ammo; set => tankProperties.Ammo = value; }

        /// <summary>
        /// Armor resistence
        /// </summary>
        public override float ArmorResistance => tankProperties.ArmorResistance;

        /// <summary>
        /// Engine power
        /// </summary>
        public override float EnginePower => tankProperties.EnginePower;

        /// <summary>
        /// Tank hp
        /// </summary>
        public override float Hp { get => tankProperties.Hp; set => tankProperties.Hp = value; }

        /// <summary>
        /// Missle reload time
        /// </summary>
        public override float ReloadTime => tankProperties.ReloadTime;
    }
}
