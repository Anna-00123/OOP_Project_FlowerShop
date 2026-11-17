using FlowerShopApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace FlowerShopApp.Tests
{
    [TestClass]
    public class BouquetTests
    {
        private Flower rose;
        private Flower tulip;
        private Bouquet bouquet;

        [TestInitialize]
        public void Setup()
        {
            DateTime today = new DateTime(2026, 10, 27);
            rose = new Flower(1, FlowerType.ROSE, FlowerColor.RED, 80m, 60, today);
            tulip = new Flower(2, FlowerType.TULIP, FlowerColor.YELLOW, 40m, 40, today);
            bouquet = new Bouquet("Весняний", WrappingType.PAPER);
        }

        [TestMethod]
        public void AddFlower_NewFlower_CreatesBouquetItem()
        {
            bouquet.AddFlower(rose, 5);

            Assert.AreEqual(1, bouquet.Items.Count);
            Assert.AreSame(rose, bouquet.Items[0].Flower);
            Assert.AreEqual(5, bouquet.Items[0].Quantity);
        }

        [TestMethod]
        public void AddFlower_SameFlowerTwice_IncreasesQuantity()
        {
            bouquet.AddFlower(rose, 5);
            bouquet.AddFlower(rose, 2);

            Assert.AreEqual(1, bouquet.Items.Count);
            Assert.AreEqual(7, bouquet.Items[0].Quantity);
        }

        [TestMethod]
        public void AddFlower_DifferentFlowers_CreatesTwoItems()
        {
            bouquet.AddFlower(rose, 3);
            bouquet.AddFlower(tulip, 4);

            Assert.AreEqual(2, bouquet.Items.Count);
        }

        [TestMethod]
        public void AddFlower_NullFlower_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => bouquet.AddFlower(null, 1));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-3)]
        public void AddFlower_InvalidQuantity_ThrowsArgumentOutOfRangeException(int quantity)
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => bouquet.AddFlower(rose, quantity));
        }

        [TestMethod]
        public void AddFlower_MoreThanMaxFlowers_ThrowsInvalidOperationException()
        {
            bouquet.AddFlower(rose, 100);

            Assert.ThrowsExactly<InvalidOperationException>(() => bouquet.AddFlower(tulip, 2));
        }

        [TestMethod]
        public void RemoveFlower_ExistingFlower_ReturnsTrueAndRemovesItem()
        {
            bouquet.AddFlower(rose, 3);
            bouquet.AddFlower(tulip, 4);

            bool removed = bouquet.RemoveFlower(rose.Id);

            Assert.IsTrue(removed);
            Assert.AreEqual(1, bouquet.Items.Count);
            Assert.AreSame(tulip, bouquet.Items[0].Flower);
        }

        [TestMethod]
        public void RemoveFlower_MissingFlower_ReturnsFalse()
        {
            bouquet.AddFlower(rose, 3);

            bool removed = bouquet.RemoveFlower(99);

            Assert.IsFalse(removed);
            Assert.AreEqual(1, bouquet.Items.Count);
        }

        [TestMethod]
        public void GetFlowerCount_ReturnsSumOfQuantities()
        {
            bouquet.AddFlower(rose, 3);
            bouquet.AddFlower(tulip, 4);

            Assert.AreEqual(7, bouquet.GetFlowerCount());
        }

        [TestMethod]
        public void GetFlowerCount_EmptyBouquet_ReturnsZero()
        {
            Assert.AreEqual(0, bouquet.GetFlowerCount());
        }

        [TestMethod]
        [DataRow(WrappingType.NONE, 0.0)]
        [DataRow(WrappingType.FILM, 30.0)]
        [DataRow(WrappingType.PAPER, 50.0)]
        [DataRow(WrappingType.BOX, 200.0)]
        [DataRow(WrappingType.BASKET, 350.0)]
        public void GetWrappingPrice_ReturnsPriceForType(WrappingType wrapping, double expected)
        {
            Assert.AreEqual((decimal)expected, Bouquet.GetWrappingPrice(wrapping));
        }

        [TestMethod]
        public void GetPrice_ItemsAndWrapping_ReturnsSum()
        {
            bouquet.AddFlower(rose, 3);
            bouquet.AddFlower(tulip, 4);

            decimal price = bouquet.GetPrice();

            Assert.AreEqual(450m, price);
        }

        [TestMethod]
        public void GetPrice_EmptyBouquet_ReturnsWrappingPrice()
        {
            Bouquet box = new Bouquet("Коробка", WrappingType.BOX);

            Assert.AreEqual(200m, box.GetPrice());
        }

        [TestMethod]
        public void BouquetItem_GetPrice_ReturnsPriceTimesQuantity()
        {
            BouquetItem item = new BouquetItem(tulip, 5);

            Assert.AreEqual(200m, item.GetPrice());
        }

        [TestMethod]
        public void PrintToDisplay_ContainsNameCountAndPrice()
        {
            bouquet.AddFlower(rose, 3);

            string text = bouquet.PrintToDisplay();

            StringAssert.Contains(text, "Весняний");
            StringAssert.Contains(text, "3");
            StringAssert.Contains(text, "290");
        }
    }
}