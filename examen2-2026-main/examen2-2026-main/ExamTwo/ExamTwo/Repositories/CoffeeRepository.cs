using ExamTwo.Models;

namespace ExamTwo.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly Dictionary<string, Coffee> _coffees;
        private readonly Dictionary<int, int> _coins;

        public CoffeeRepository()
        {
            _coffees = new Dictionary<string, Coffee>
            {
                { "Americano", new Coffee { Name="Americano", PriceColones=10, PriceCents=950, Quantity=10 } },
                { "Cappuccino", new Coffee { Name="Cappuccino", PriceColones=8, PriceCents=1200, Quantity=8 } },
                { "Lates", new Coffee { Name="Lates", PriceColones=10, PriceCents=1350, Quantity=10 } },
                { "Mocaccino", new Coffee { Name="Mocaccino", PriceColones=15, PriceCents=1500, Quantity=15 } }
            };

            _coins = new Dictionary<int, int>
            {
                { 500, 20 },
                { 100, 30 },
                { 50, 50 },
                { 25, 25 }
            };
        }

        public Dictionary<string, Coffee> GetCoffees() => _coffees;
        public Dictionary<int, int> GetCoins() => _coins;
    }
}
