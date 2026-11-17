using FlowerShopApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace FlowerShopApp.Tests
{
    [TestClass]
    public class FlowerShopTests
    {
        private readonly DateTime today = new DateTime(2026, 10, 27);
        private FlowerShop shop;
        private Flower rose;
        private Flower tulip;
        private Flower oldRose;
        private Customer customer;
        private Employee florist;

        [TestInitialize]
        public void Setup()
        {
            shop = new FlowerShop("Квітковий рай", "м. Харків, вул. Сумська, 10");
            rose = new Flower(1, FlowerType.ROSE, FlowerColor.RED, 80m, 60, today);
            tulip = new Flower(2, FlowerType.TULIP, FlowerColor.YELLOW, 40m, 40, today.AddDays(-3));
            oldRose = new Flower(3, FlowerType.ROSE, FlowerColor.WHITE, 70m, 50, today.AddDays(-10));
            customer = new Customer(1, "Анна", "+380671234567");
            florist = new Employee("Олена", "+380501112233", EmployeePosition.FLORIST, 18000m);
        }

        private Bouquet MakeBouquet()
        {
            Bouquet b = new Bouquet("Весняний", WrappingType.PAPER);
            b.Items.Add(new BouquetItem(rose, 5));
            return b;
        }

        [TestMethod]
        public void AddToStock_Flower_AddedSameObject()
        {
            shop.AddToStock(rose);

            Assert.AreEqual(1, shop.Stock.Count);
            Assert.AreSame(rose, shop.Stock[0]);
        }

        [TestMethod]
        public void AddToStock_DuplicateId_ThrowsInvalidOperationException()
        {
            shop.AddToStock(rose);
            Flower copy = new Flower(1, FlowerType.LILY, FlowerColor.WHITE, 60m, 70, today);

            Assert.ThrowsExactly<InvalidOperationException>(() => shop.AddToStock(copy));
        }

        [TestMethod]
        public void AddToStock_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => shop.AddToStock(null));
        }

        [TestMethod]
        public void HireEmployee_Employee_Added()
        {
            shop.HireEmployee(florist);

            CollectionAssert.Contains(shop.Employees, florist);
        }

        [TestMethod]
        public void FireEmployee_Hired_ReturnsTrueAndEmployeeStillExists()
        {
            shop.HireEmployee(florist);

            bool fired = shop.FireEmployee(florist);

            Assert.IsTrue(fired);
            Assert.AreEqual(0, shop.Employees.Count);
            Assert.AreEqual("Олена", florist.Name);
        }

        [TestMethod]
        public void FireEmployee_NotHired_ReturnsFalse()
        {
            Assert.IsFalse(shop.FireEmployee(florist));
        }

        [TestMethod]
        public void RegisterCustomer_SamePhoneTwice_ThrowsInvalidOperationException()
        {
            shop.RegisterCustomer(customer);
            Customer other = new Customer(2, "Ганна", "+380671234567");

            Assert.ThrowsExactly<InvalidOperationException>(() => shop.RegisterCustomer(other));
        }

        [TestMethod]
        public void CreateOrder_ReturnsNewOrderAndAddsToList()
        {
            shop.RegisterCustomer(customer);

            Order order = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));

            Assert.IsNotNull(order);
            Assert.AreEqual(1, shop.Orders.Count);
            Assert.AreSame(order, shop.Orders[0]);
            Assert.AreEqual(OrderStatus.NEW, order.Status);
        }

        [TestMethod]
        public void CreateOrder_TwoOrders_NumbersIncrease()
        {
            shop.RegisterCustomer(customer);

            Order first = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));
            Order second = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(2));

            Assert.AreEqual(1, first.Number);
            Assert.AreEqual(2, second.Number);
        }

        [TestMethod]
        public void CreateOrder_UnregisteredCustomer_ThrowsInvalidOperationException()
        {
            Assert.ThrowsExactly<InvalidOperationException>(() =>
                shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1)));
        }

        [TestMethod]
        public void CreateOrder_EmptyBouquet_ThrowsArgumentException()
        {
            shop.RegisterCustomer(customer);
            Bouquet empty = new Bouquet("Порожній", WrappingType.NONE);

            Assert.ThrowsExactly<ArgumentException>(() =>
                shop.CreateOrder(customer, empty, "вул. Пушкінська, 5", DateTime.Today.AddDays(1)));
        }

        [TestMethod]
        public void CreateOrder_DateInPast_ThrowsArgumentOutOfRangeException()
        {
            shop.RegisterCustomer(customer);

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
                shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(-1)));
        }

        [TestMethod]
        public void FindFlowers_ByType_ReturnsOnlyThisType()
        {
            shop.AddToStock(rose);
            shop.AddToStock(tulip);
            shop.AddToStock(oldRose);

            List<Flower> roses = shop.FindFlowers(FlowerType.ROSE);

            Assert.AreEqual(2, roses.Count);
            Assert.IsTrue(roses.TrueForAll(f => f.Type == FlowerType.ROSE));
        }

        [TestMethod]
        public void FindFlowers_NoSuchType_ReturnsEmptyList()
        {
            shop.AddToStock(rose);

            List<Flower> orchids = shop.FindFlowers(FlowerType.ORCHID);

            Assert.IsNotNull(orchids);
            Assert.AreEqual(0, orchids.Count);
        }

        [TestMethod]
        public void RemoveStaleFlowers_RemovesOnlyOldFlowers()
        {
            shop.AddToStock(rose);
            shop.AddToStock(tulip);
            shop.AddToStock(oldRose);

            int removed = shop.RemoveStaleFlowers(today);

            Assert.AreEqual(1, removed);
            Assert.AreEqual(2, shop.Stock.Count);
            CollectionAssert.DoesNotContain(shop.Stock, oldRose);
        }

        [TestMethod]
        public void GetOrdersByStatus_ReturnsMatchingOrders()
        {
            shop.RegisterCustomer(customer);
            Order paid = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));
            shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(2));
            paid.Pay();

            List<Order> result = shop.GetOrdersByStatus(OrderStatus.PAID);

            Assert.AreEqual(1, result.Count);
            Assert.AreSame(paid, result[0]);
        }

        [TestMethod]
        public void GetTotalRevenue_CountsOnlyPaidAndCompleted()
        {
            shop.RegisterCustomer(customer);
            shop.HireEmployee(florist);
            Order o1 = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));
            Order o2 = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));
            Order o3 = shop.CreateOrder(customer, MakeBouquet(), "вул. Пушкінська, 5", DateTime.Today.AddDays(1));
            o1.Pay();
            o2.AssignFlorist(florist);
            o2.Pay();
            o2.Complete();
            o3.Cancel();

            decimal revenue = shop.GetTotalRevenue();

            Assert.AreEqual(1200m, revenue);
        }

        [TestMethod]
        public void GetTotalRevenue_NoOrders_ReturnsZero()
        {
            Assert.AreEqual(0m, shop.GetTotalRevenue());
        }
    }
}