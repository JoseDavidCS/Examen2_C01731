using Microsoft.AspNetCore.Mvc;
using ExamTwo.Models;
using ExamTwo.Services;

namespace ExamTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineService _service;

        public CoffeeMachineController(ICoffeeMachineService service)
        {
            _service = service;
        }

        [HttpGet("getCoffees")]
        public ActionResult<Dictionary<string, int>> GetCoffeePrices() =>
            Ok(_service.GetCoffeePrices());

        [HttpGet("getCoffeePricesInCents")]
        public ActionResult<Dictionary<string, int>> GetCoffeePricesInCents() =>
            Ok(_service.GetCoffeePricesInCents());

        [HttpGet("getQuantity")]
        public ActionResult<Dictionary<string, int>> GetQuantity() =>
            Ok(_service.GetQuantities());

        [HttpPost("buyCoffee")]
        public ActionResult<string> BuyCoffee([FromBody] OrderRequest request)
        {
            try
            {
                return Ok(_service.BuyCoffee(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
