using ExamTwo.Models;
using System.Collections.Generic;

namespace ExamTwo.Repositories
{
    public interface ICoffeeRepository
    {
        Dictionary<string, Coffee> GetCoffees();
        Dictionary<int, int> GetCoins();
    }
}
