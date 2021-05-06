using OpenTK;
using Tankists.GameObjects;

namespace Tankists
{
    /// <summary>
    /// Armor box class
    /// </summary>
    public class ArmorBox : BoxBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pos">Spawn position</param>
        public ArmorBox(Vector2 pos)
            : base(@"Textures\Same\Shield.png", pos)
        { }

        /// <summary>
        /// Decorate method
        /// </summary>
        /// <param name="tank">Tank to decorate</param>
        public override void Decorate(Tank tank)
        {
            tank.TankProperties = new ArmorBonus(tank.TankProperties);
        }
    }
}
