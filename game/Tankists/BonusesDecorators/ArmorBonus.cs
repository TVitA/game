namespace Tankists
{
    /// <summary>
    /// Armor decorator
    /// </summary>
    public class ArmorBonus : Bonus
    {
        /// <summary>
        /// Armor decorator ctor
        /// </summary>
        /// <param name="tankProperties">Props to decorate</param>
        public ArmorBonus(TankProperties tankProperties)
            : base(tankProperties)
        { }

        /// <summary>
        /// Decorated armor resistance
        /// </summary>
        public override float ArmorResistance => tankProperties.ArmorResistance + 0.2f;
    }
}
