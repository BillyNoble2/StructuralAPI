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

    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("download")]
        public IActionResult DownloadDocument()
        {
            try
            {
                string content = "Hello, this is a sample Word document created by the API.";
                byte[] document = _documentService.CreateWordDocument(content);

                if (document == null || document.Length == 0)
                {
                    return NotFound("Document could not be created.");
                }

                return File(document, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "sample.docx");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, $"An error occurred while creating the document: {ex.Message}");
            }
        }
    }
}

