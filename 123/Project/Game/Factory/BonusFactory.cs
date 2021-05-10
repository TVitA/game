using System;

using OpenTK;

namespace Game.Factory
{
    /// <summary>
    /// Bonus factory class.
    /// </summary>
    public class BonusFactory : Object
    {
        /// <summary>
        /// Bonus factory constructor.
        /// </summary>
        public BonusFactory()
            : base()
        { }

        /// <summary>
        /// Returns random bonus.
        /// </summary>
        /// <param name="position">Spawn position.</param>
        /// <returns>Some random bonus.</returns>
        public Bonus GetRandomBox(Vector2 position)
        {
            var r = new Random();
            switch (r.Next(0, 3))
            {
                case 0:
                    return new AmmoBonus(position);
                case 1:
                    return new ArmorBonus(position);
                case 2:
                    return new ReloadSpeedBonus(position);
            }
            return null;
        }

        /// <summary>
        /// Returns ammo bonus.
        /// </summary>
        /// <param name="position">Spawn position.</param>
        /// <returns>AmmoBonus.</returns>
        public Bonus GetAmmoBox(Vector2 position)
        {
            return new AmmoBonus(position);
        }
    }
}
