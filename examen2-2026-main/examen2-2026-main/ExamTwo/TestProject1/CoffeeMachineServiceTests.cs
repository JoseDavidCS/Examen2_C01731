using NUnit.Framework;
using Moq;
using ExamTwo.Services;
using ExamTwo.Repositories;
using ExamTwo.Models;
using System.Collections.Generic;

namespace TestProject1
{
    [TestFixture]
    public class CoffeeMachineServiceTests
    {
        private ICoffeeMachineService _service;
        private Mock<ICoffeeRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ICoffeeRepository>();

            _mockRepo.Setup(r => r.GetCoffees()).Returns(new Dictionary<string, Coffee>
            {
                { "Americano", new Coffee { Name="Americano", PriceColones=10, PriceCents=950, Quantity=10 } },
                { "Cappuccino", new Coffee { Name="Cappuccino", PriceColones=8, PriceCents=1200, Quantity=5 } }
            });

            _mockRepo.Setup(r => r.GetCoins()).Returns(new Dictionary<int, int>
            {
                { 500, 10 },
                { 100, 20 },
                { 50, 30 }
            });

            _service = new CoffeeMachineService(_mockRepo.Object);
        }

        [Test]
        public void GetCoffeePrices_CorrectValues()
        {
            var prices = _service.GetCoffeePrices();
            Assert.That(prices["Americano"], Is.EqualTo(10));
            Assert.That(prices["Cappuccino"], Is.EqualTo(8));
        }

        [Test]
        public void BuyCoffee_PaymentIsEnough()
        {
            var request = new OrderRequest
            {
                Order = new Dictionary<string, int> { { "Americano", 1 } },
                Payment = new Payment { TotalAmount = 2000 }
            };

            var result = _service.BuyCoffee(request);

            Assert.That(result, Does.Contain("Su vuelto es de"));
        }

        [Test]
        public void BuyCoffee_PaymentIsInsufficient()
        {
            var request = new OrderRequest
            {
                Order = new Dictionary<string, int> { { "Americano", 1 } },
                Payment = new Payment { TotalAmount = 100 }
            };

            Assert.That(() => _service.BuyCoffee(request), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void BuyCoffee_NotEnoughStock()
        {
            var request = new OrderRequest
            {
                Order = new Dictionary<string, int> { { "Cappuccino", 10 } },
                Payment = new Payment { TotalAmount = 20000 }
            };

            Assert.That(() => _service.BuyCoffee(request), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void GetQuantities()
        {
            var quantities = _service.GetQuantities();
            Assert.That(quantities["Americano"], Is.EqualTo(10));
            Assert.That(quantities["Cappuccino"], Is.EqualTo(5));
        }
    }
}
