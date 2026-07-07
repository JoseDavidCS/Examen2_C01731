using ExamTwo.Models;
using ExamTwo.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ExamTwo.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly ICoffeeRepository _repository;

        public CoffeeMachineService(ICoffeeRepository repository)
        {
            _repository = repository;
        }

        public Dictionary<string, int> GetCoffeePrices() =>
            _repository.GetCoffees().ToDictionary(c => c.Key, c => c.Value.PriceColones);

        public Dictionary<string, int> GetCoffeePricesInCents() =>
            _repository.GetCoffees().ToDictionary(c => c.Key, c => c.Value.PriceCents);

        public Dictionary<string, int> GetQuantities() =>
            _repository.GetCoffees().ToDictionary(c => c.Key, c => c.Value.Quantity);

        public string BuyCoffee(OrderRequest request)
        {
            if (request.Order == null || request.Order.Count == 0)
                throw new ArgumentException("Orden vacía.");

            if (request.Payment.TotalAmount <= 0)
                throw new ArgumentException("Dinero insuficiente.");

            var coffees = _repository.GetCoffees();
            var coins = _repository.GetCoins();

            var costoTotal = request.Order.Sum(o => coffees[o.Key].PriceCents * o.Value);

            if (request.Payment.TotalAmount < costoTotal)
                throw new ArgumentException("Dinero insuficiente.");

            foreach (var cafe in request.Order)
            {
                if (cafe.Value > coffees[cafe.Key].Quantity)
                    throw new InvalidOperationException($"No hay suficientes {cafe.Key} en la máquina.");

                coffees[cafe.Key].Quantity -= cafe.Value;
            }

            var change = request.Payment.TotalAmount - costoTotal;
            string result = $"Su vuelto es de: {change} colones. Desglose:";

            foreach (var coin in coins.Keys.OrderByDescending(c => c))
            {
                var count = Math.Min(change / coin, coins[coin]);
                if (count > 0)
                {
                    result += $" {count} moneda de {coin},";
                    change -= coin * count;
                }
            }

            if (change > 0)
                throw new InvalidOperationException("No hay suficiente cambio en la máquina.");

            return result;
        }
    }
}
