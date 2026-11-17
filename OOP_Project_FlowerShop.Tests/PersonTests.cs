using FlowerShopApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FlowerShopApp.Tests
{
    [TestClass]
    public class PersonTests
    {
        private Customer customer;
        private Employee florist;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer(1, "Анна", "+380671234567");
            florist = new Employee("Олена", "+380501112233", EmployeePosition.FLORIST, 18000m);
        }

        [TestMethod]
        public void Customer_GetContactInfo_ReturnsNameAndPhone()
        {
            Assert.AreEqual("Анна (+380671234567)", customer.GetContactInfo());
        }

        [TestMethod]
        public void Employee_GetContactInfo_ThroughInterface_ReturnsNameAndPhone()
        {
            IPerson person = florist;

            Assert.AreEqual("Олена (+380501112233)", person.GetContactInfo());
        }

        [TestMethod]
        [DataRow(450.0, 45)]
        [DataRow(99.99, 9)]
        [DataRow(5.0, 0)]
        public void AddBonus_OnePointForEveryTenUah(double sum, int expected)
        {
            customer.AddBonus((decimal)sum);

            Assert.AreEqual(expected, customer.BonusPoints);
        }

        [TestMethod]
        public void AddBonus_TwoOrders_PointsAreSummed()
        {
            customer.AddBonus(300m);
            customer.AddBonus(150m);

            Assert.AreEqual(45, customer.BonusPoints);
        }

        [TestMethod]
        public void AddBonus_NegativeSum_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => customer.AddBonus(-10m));
        }

        [TestMethod]
        public void UseBonus_EnoughPoints_ReturnsTrueAndDecreases()
        {
            customer.AddBonus(500m);

            bool used = customer.UseBonus(20);

            Assert.IsTrue(used);
            Assert.AreEqual(30, customer.BonusPoints);
        }

        [TestMethod]
        public void UseBonus_NotEnoughPoints_ReturnsFalseAndKeepsPoints()
        {
            customer.AddBonus(100m);

            bool used = customer.UseBonus(20);

            Assert.IsFalse(used);
            Assert.AreEqual(10, customer.BonusPoints);
        }

        [TestMethod]
        [DataRow(10, 19800.0)]
        [DataRow(50, 27000.0)]
        public void RaiseSalary_ValidPercent_IncreasesSalary(int percent, double expected)
        {
            florist.RaiseSalary(percent);

            Assert.AreEqual((decimal)expected, florist.Salary);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(51)]
        public void RaiseSalary_InvalidPercent_ThrowsArgumentOutOfRangeException(int percent)
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => florist.RaiseSalary(percent));
        }
    }
}