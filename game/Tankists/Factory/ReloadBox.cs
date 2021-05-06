using OpenTK;
using Tankists.GameObjects;

namespace Tankists
{
    /// <summary>
    /// Reload box class
    /// </summary>
    public class ReloadBox : BoxBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pos">Spawn position</param>
        public ReloadBox(Vector2 pos)
            : base(@"Textures\Same\Reload.png", pos)
        { }

        /// <summary>
        /// Decorate method
        /// </summary>
        /// <param name="tank">Tank to decorate</param>
        public override void Decorate(Tank tank)
        {
            tank.TankProperties = new ReloadBonus(tank.TankProperties);
        }
    }
}
