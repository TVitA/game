namespace Tankists
{
    /// <summary>
    /// Reload bonus decorator
    /// </summary>
    public class ReloadBonus : Bonus
    {
        /// <summary>
        /// Decorator ctor
        /// </summary>
        /// <param name="tankProperties">Props to decorate</param>
        public ReloadBonus(TankProperties tankProperties)
            : base(tankProperties)
        { }

        /// <summary>
        /// Overloaded reload time prop
        /// </summary>
        public override float ReloadTime { get => tankProperties.ReloadTime - (tankProperties.ReloadTime / 100.0f * 20.0f); }
    }
}
