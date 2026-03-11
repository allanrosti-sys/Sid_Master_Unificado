using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SID_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet("components")]
        public async Task<IActionResult> GetComponents()
        {
            // TODO: In production, this file should be served from a more robust location,
            // not directly from the frontend project.
            var filePath = Path.Combine("..", "sid-react", "src", "data", "component-catalog.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Catálogo de componentes não encontrado.");
            }

            var json = await System.IO.File.ReadAllTextAsync(filePath);
            var components = JsonSerializer.Deserialize<object>(json);

            return Ok(components);
        }
    }
}
