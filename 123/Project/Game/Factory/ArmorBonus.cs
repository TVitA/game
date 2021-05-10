using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Factory
{
    /// <summary>
    /// Armor bonus class.
    /// </summary>
    public class ArmorBonus : Bonus
    {
        /// <summary>
        /// Armor bonus constructor.
        /// </summary>
        /// <param name="position">Spawn position.</param>
        public ArmorBonus(Vector2 position)
            : base(@"Textures\Others\Shield.png", position)
        { }

        /// <summary>
        /// Decorate method.
        /// </summary>
        /// <param name="artillery">Artillery to decorate.</param>
        public override void Decorate(Artillery artillery)
        {
            artillery.ArtilleryProperties = new ArtPropsWithArmorUp(artillery.ArtilleryProperties);
        }
    }
}
