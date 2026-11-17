using FlowerShopApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FlowerShopApp.Tests
{
    [TestClass]
    public class FlowerTests
    {
        private Flower rose;
        private readonly DateTime today = new DateTime(2026, 10, 27);

        [TestInitialize]
        public void Setup()
        {
            rose = new Flower(1, FlowerType.ROSE, FlowerColor.RED, 80m, 60, today.AddDays(-2));
        }

        [TestMethod]
        public void GetPrice_ReturnsFlowerPrice()
        {
            decimal price = rose.GetPrice();

            Assert.AreEqual(80m, price);
        }

        [TestMethod]
        public void GetPrice_ThroughInterface_ReturnsFlowerPrice()
        {
            IPriceable item = rose;

            Assert.AreEqual(80m, item.GetPrice());
        }

        [TestMethod]
        [DataRow(0, true)]
        [DataRow(3, true)]
        [DataRow(7, true)]
        [DataRow(8, false)]
        [DataRow(30, false)]
        public void IsFresh_DependsOnDaysSinceArrival(int days, bool expected)
        {
            Flower flower = new Flower(2, FlowerType.TULIP, FlowerColor.YELLOW, 40m, 40, today.AddDays(-days));

            bool result = flower.IsFresh(today);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsFresh_ArrivalDateInFuture_ThrowsArgumentException()
        {
            Assert.ThrowsExactly<ArgumentException>(() => rose.IsFresh(today.AddDays(-5)));
        }

        [TestMethod]
        [DataRow(10, 72.0)]
        [DataRow(25, 60.0)]
        [DataRow(90, 8.0)]
        public void ApplyDiscount_ValidPercent_DecreasesPrice(int percent, double expected)
        {
            rose.ApplyDiscount(percent);

            Assert.AreEqual((decimal)expected, rose.Price);
        }

        [TestMethod]
        public void ApplyDiscount_RoundsToKopecks()
        {
            Flower flower = new Flower(3, FlowerType.LILY, FlowerColor.WHITE, 33.33m, 70, today);

            flower.ApplyDiscount(15);

            Assert.AreEqual(28.33m, flower.Price);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-5)]
        [DataRow(91)]
        public void ApplyDiscount_InvalidPercent_ThrowsArgumentOutOfRangeException(int percent)
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => rose.ApplyDiscount(percent));
        }
    }
}