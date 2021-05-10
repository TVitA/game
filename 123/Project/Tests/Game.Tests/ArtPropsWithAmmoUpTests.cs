using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Tests
{
    [TestClass()]
    public class ArtPropsWithAmmoUpTests : Object
    {
        [TestMethod()]
        public void Decorate_Test()
        {
            // Arrange
            var artilleryProperties = new ArtilleryProperties();

            artilleryProperties.Ammo = 10;

            // Act
            artilleryProperties = new ArtPropsWithAmmoUp(artilleryProperties);

            // Assert
            Assert.IsTrue(artilleryProperties.Ammo == 20);
        }
    }
}
