using Game.GameObjects;

namespace Game.Decorators
{
    /// <summary>
    /// Ammo decorator.
    /// </summary>
    public class ArtPropsWithAmmoUp : ArtPropsDecorator
    {
        /// <summary>
        /// Decoration constructor.
        /// </summary>
        /// <param name="artilleryProperties">Props to decorate.</param>
        public ArtPropsWithAmmoUp(ArtilleryProperties artilleryProperties)
            : base(artilleryProperties)
        {
            Ammo += 10;
        }
    }
}
