using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Tests
{
    [TestClass()]
    public class ArtPropsWithReloadSpeedUpTests : Object
    {
        [TestMethod()]
        public void Decorate_Test()
        {
            // Arrange
            var artilleryProperties = new ArtilleryProperties();

            artilleryProperties.ReloadTime = 3.0f;

            // Act
            artilleryProperties = new ArtPropsWithReloadSpeedUp(artilleryProperties);

            // Assert
            Assert.IsTrue(artilleryProperties.ReloadTime == 2.4f);
        }
    }
}
