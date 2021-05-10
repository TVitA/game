using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Game.Decorators;
using Game.GameObjects;

namespace Game.Tests
{
    [TestClass()]
    public class ArtPropsWithArmorUpTests : Object
    {
        [TestMethod()]
        public void Decorate_Test()
        {
            // Arrange
            var artilleryProperties = new ArtilleryProperties();

            artilleryProperties.Armor = 0.5f;

            // Act
            artilleryProperties = new ArtPropsWithArmorUp(artilleryProperties);

            // Assert
            Assert.IsTrue(artilleryProperties.Armor == 0.7f);
        }
    }
}
