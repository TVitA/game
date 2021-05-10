using OpenTK;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Factory
{
    /// <summary>
    /// Ammo bonus class.
    /// </summary>
    public class AmmoBonus : Bonus
    {
        /// <summary>
        /// Ammo bonus constructor.
        /// </summary>
        /// <param name="position">Bonus position.</param>
        public AmmoBonus(Vector2 position)
            : base(@"Textures\Others\Ammo.png", position)
        { }

        /// <summary>
        /// Decorate method.
        /// </summary>
        /// <param name="artillery">Artillery to decorate.</param>
        public override void Decorate(Artillery artillery)
        {
            artillery.ArtilleryProperties = new ArtPropsWithAmmoUp(artillery.ArtilleryProperties);
        }
    }
}
