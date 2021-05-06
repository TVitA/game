using OpenTK;
using System;

namespace Tankists
{
    /// <summary>
    /// Boxes factory
    /// </summary>
    public class BoxesFactory
    {
        /// <summary>
        /// Returns random box
        /// </summary>
        /// <param name="pos">Spawn position</param>
        /// <returns></returns>
        public BoxBase GetRandomBox(Vector2 pos)
        {
            var r = new Random();
            switch (r.Next(0, 3))
            {
                case 0:
                    return new AmmoBox(pos);
                case 1:
                    return new ArmorBox(pos);
                case 2:
                    return new ReloadBox(pos);
            }
            return null;
        }

        /// <summary>
        /// Returns ammo box
        /// </summary>
        /// <param name="pos">Spawn position</param>
        /// <returns></returns>
        public BoxBase GetAmmoBox(Vector2 pos)
        {
            return new AmmoBox(pos);
        }
    }
}
