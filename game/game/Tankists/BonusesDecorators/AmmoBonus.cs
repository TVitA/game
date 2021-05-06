namespace Tankists
{
    /// <summary>
    /// Ammo decorator
    /// </summary>
    public class AmmoBonus : Bonus
    {
        /// <summary>
        /// Decoration ctor
        /// </summary>
        /// <param name="tankProperties">Props to decorate</param>
        public AmmoBonus(TankProperties tankProperties)
            : base(tankProperties)
        {
            Ammo += 10;
        }
    }
}
