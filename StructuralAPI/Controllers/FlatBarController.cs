using Microsoft.AspNetCore.Mvc;
using StructuralAPI.CalculationEngines;
using StructuralAPI.Models;
using Newtonsoft.Json;

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
                var json = JsonConvert.SerializeObject(response);
                return Ok(json);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

