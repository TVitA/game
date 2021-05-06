using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tankists;
using Tankists.GameObjects;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ArmorBonusTest()
        {
            TankProperties tmp = new TankProperties();
            tmp.ArmorResistance = 0.5f;
            tmp = new ArmorBonus(tmp);
            Assert.IsTrue(tmp.ArmorResistance == 0.7f);
        }

        [TestMethod]
        public void ReloadBonusTest()
        {
            TankProperties tmp = new TankProperties();
            tmp.ReloadTime = 3f;
            tmp = new ReloadBonus(tmp);
            Assert.IsTrue(tmp.ReloadTime == 2.4f);
        }

        [TestMethod]
        public void AmmoBonusTest()
        {
            TankProperties tmp = new TankProperties();
            tmp.Ammo = 10;
            tmp = new AmmoBonus(tmp);
            Assert.IsTrue(tmp.Ammo == 20);
        }


    }
}
