using JUnity.Basic;
using System;
using Tankists.GameObjects.Surroundings;

namespace Tankists.GameObjects.Base
{
    /// <summary>
    /// Presents launcher class
    /// </summary>
    internal class PresentsLauncher : GameObject
    {
        RedTank redTank;
        GreenTank greenTank;
        float next;
        Random random = new Random();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_redTank">First tank</param>
        /// <param name="_greenTank">Second tank</param>
        public PresentsLauncher(RedTank _redTank, GreenTank _greenTank)
        {
            redTank = _redTank;
            greenTank = _greenTank;
            next = random.Next(10, 20);
        }

        /// <summary>
        /// Fixed update method
        /// </summary>
        /// <param name="deltaTime">Time between frames</param>
        public override void FixedUpdate(double deltaTime)
        {
            next -= (float)deltaTime;
            if (next <= 0.0f)
            {
                next = random.Next(10, 50);

                if (redTank.TankProperties.Ammo == 0)
                {
                    Plane.SendAmmo(redTank);
                }
                else if (greenTank.TankProperties.Ammo == 0)
                {
                    Plane.SendAmmo(greenTank);
                }
                else
                {
                    var tmp = random.NextDouble();
                    if (tmp <= 0.5f)
                    {
                        Plane.SendPresent(redTank);
                    }
                    else
                    {
                        Plane.SendPresent(greenTank);
                    }
                }
            }
        }
    }
}
