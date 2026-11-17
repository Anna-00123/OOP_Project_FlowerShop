using FlowerShopApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;

namespace FlowerShopApp.Tests
{
    [TestClass]
    public class OrderTests
    {
        private Customer customer;
        private Employee florist;
        private Employee courier;
        private Bouquet bouquet;
        private Order order;

        [TestInitialize]
        public void Setup()
        {
            DateTime today = new DateTime(2026, 10, 27);
            customer = new Customer(1, "Анна", "+380671234567");
            florist = new Employee("Олена", "+380501112233", EmployeePosition.FLORIST, 18000m);
            courier = new Employee("Ігор", "+380509998877", EmployeePosition.COURIER, 15000m);
            bouquet = new Bouquet("Весняний", WrappingType.PAPER);
            bouquet.Items.Add(new BouquetItem(new Flower(1, FlowerType.ROSE, FlowerColor.RED, 80m, 60, today), 5));
            order = new Order(1, customer, bouquet, "вул. Сумська, 10", today.AddDays(1));
        }

        [TestMethod]
        public void AssignFlorist_Florist_IsAssigned()
        {
            order.AssignFlorist(florist);

            Assert.AreSame(florist, order.Florist);
        }

        [TestMethod]
        public void AssignFlorist_NotFlorist_ThrowsArgumentException()
        {
            Assert.ThrowsExactly<ArgumentException>(() => order.AssignFlorist(courier));
        }

        [TestMethod]
        public void AssignFlorist_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => order.AssignFlorist(null));
        }

        [TestMethod]
        public void GetTotal_CheapBouquet_AddsDeliveryPrice()
        {
            Assert.AreEqual(600m, order.GetTotal());
        }

        [TestMethod]
        public void GetTotal_ExpensiveBouquet_FreeDelivery()
        {
            bouquet.Items[0].Quantity = 25;

            Assert.AreEqual(2050m, order.GetTotal());
        }

        [TestMethod]
        public void Pay_NewOrder_StatusPaidAndBonusAdded()
        {
            order.Pay();

            Assert.AreEqual(OrderStatus.PAID, order.Status);
            Assert.AreEqual(60, customer.BonusPoints);
        }

        [TestMethod]
        public void Pay_AlreadyPaid_ThrowsInvalidOperationException()
        {
            order.Pay();

            Assert.ThrowsExactly<InvalidOperationException>(() => order.Pay());
        }

        [TestMethod]
        public void Complete_PaidWithFlorist_StatusCompleted()
        {
            order.AssignFlorist(florist);
            order.Pay();

            order.Complete();

            Assert.AreEqual(OrderStatus.COMPLETED, order.Status);
        }

        [TestMethod]
        public void Complete_WithoutFlorist_ThrowsInvalidOperationException()
        {
            order.Pay();

            Assert.ThrowsExactly<InvalidOperationException>(() => order.Complete());
        }

        [TestMethod]
        public void Complete_NotPaid_ThrowsInvalidOperationException()
        {
            order.AssignFlorist(florist);

            Assert.ThrowsExactly<InvalidOperationException>(() => order.Complete());
        }

        [TestMethod]
        public void Cancel_NewOrder_StatusCancelled()
        {
            order.Cancel();

            Assert.AreEqual(OrderStatus.CANCELLED, order.Status);
        }

        [TestMethod]
        public void Cancel_CompletedOrder_ThrowsInvalidOperationException()
        {
            order.AssignFlorist(florist);
            order.Pay();
            order.Complete();

            Assert.ThrowsExactly<InvalidOperationException>(() => order.Cancel());
        }

        [TestMethod]
        public void PrintToDisplay_ThroughInterface_ContainsMainData()
        {
            IPrintable printable = order;

            string text = printable.PrintToDisplay();

            StringAssert.Contains(text, "1");
            StringAssert.Contains(text, "Анна");
            StringAssert.Contains(text, "Весняний");
            StringAssert.Contains(text, "NEW");
        }
    }
}