using OpenTK;
using Tankists.GameObjects;

namespace Tankists
{
    /// <summary>
    /// Ammo box class
    /// </summary>
    public class AmmoBox : BoxBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pos">Spawn position</param>
        public AmmoBox(Vector2 pos)
            : base(@"Textures\Same\Ammo.png", pos)
        { }

        /// <summary>
        /// Decorate method
        /// </summary>
        /// <param name="tank">Tank to decorate</param>
        public override void Decorate(Tank tank)
        {
            tank.TankProperties = new AmmoBonus(tank.TankProperties);
        }
    }
}
