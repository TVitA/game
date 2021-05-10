using OpenTK;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Factory
{
    /// <summary>
    /// Reload speed bonus class.
    /// </summary>
    public class ReloadSpeedBonus : Bonus
    {
        /// <summary>
        /// Reload speed bonus class.
        /// </summary>
        /// <param name="position">Spawn position.</param>
        public ReloadSpeedBonus(Vector2 position)
            : base(@"Textures\Others\Reload.png", position)
        { }

        /// <summary>
        /// Decorate method.
        /// </summary>
        /// <param name="artillery">Artillery to decorate.</param>
        public override void Decorate(Artillery artillery)
        {
            artillery.ArtilleryProperties = new ArtPropsWithReloadSpeedUp(artillery.ArtilleryProperties);
        }
    }
}
