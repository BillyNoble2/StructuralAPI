using Microsoft.AspNetCore.Mvc;
using StructuralAPI.CalculationEngines;
using StructuralAPI.Models;

namespace StructuralAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlatBarController : Controller
    {
        [HttpPost(Name = "calculate")]
        public IActionResult Calculate([FromBody] FlatBarData request)
        {
            try
            {
                double result = 0;
                result = CalculationEngines.FlatBarDesigner.Calculator(request);
                return Ok(new { result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

