using ExamTwo.Models;
using System.Collections.Generic;

namespace ExamTwo.Services
{
    public interface ICoffeeMachineService
    {
        Dictionary<string, int> GetCoffeePrices();
        Dictionary<string, int> GetCoffeePricesInCents();
        Dictionary<string, int> GetQuantities();
        string BuyCoffee(OrderRequest request);
    }
}
