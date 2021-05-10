using System;

using GameEngine.Basic;

namespace Game.GameObjects
{
    /// <summary>
    /// Presents launcher class.
    /// </summary>
    internal class PresentsLauncher : GameObject
    {

        FirstArtillery firstArtillery;
        SecondArtillery secondArtillery;

        /// <summary>
        /// Time to next present.
        /// </summary>
        private Single next;

        /// <summary>
        /// Random.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Presents launcher constructor.
        /// </summary>
        /// <param name="firstArtillery">First player.</param>
        /// <param name="secondArtillery">Second player.</param>
        public PresentsLauncher(FirstArtillery firstArtillery, SecondArtillery secondArtillery)
        {
            this.firstArtillery = firstArtillery;
            this.secondArtillery = secondArtillery;

            next = random.Next(10, 20);
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            next -= (Single)deltaTime;

            if (next <= 0.0f)
            {
                next = random.Next(10, 50);

                if (firstArtillery.ArtilleryProperties.Ammo == 0)
                {
                    Plane.SendAmmo(firstArtillery);
                }
                else if (secondArtillery.ArtilleryProperties.Ammo == 0)
                {
                    Plane.SendAmmo(secondArtillery);
                }
                else
                {
                    var rnd = random.NextDouble();

                    if (rnd <= 0.5f)
                    {
                        Plane.SendPresent(firstArtillery);
                    }
                    else
                    {
                        Plane.SendPresent(secondArtillery);
                    }
                }
            }
        }
    }
}
