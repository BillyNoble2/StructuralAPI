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
        public IActionResult Calculate([FromBody] FlatBarDataIn request)
        {
            try
            {
                FlatBarDataOut response;
                response = CalculationEngines.FlatBarDesigner.Calculator(request);
                return Ok(new { response });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

