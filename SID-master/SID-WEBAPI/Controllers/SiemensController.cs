using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SID_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiemensController : ControllerBase
    {
        private readonly string _siemensDataPath;

        public SiemensController(IConfiguration configuration)
        {
            _siemensDataPath = configuration.GetValue<string>("SiemensDataPath");
        }

        [HttpGet("blocks")]
        public IActionResult GetBlocks()
        {
            if (!Directory.Exists(_siemensDataPath))
            {
                return NotFound("Siemens data directory not found.");
            }

            var files = Directory.GetFiles(_siemensDataPath, "*.xml")
                                 .Select(Path.GetFileName)
                                 .ToList();

            return Ok(files);
        }

        [HttpGet("blocks/{blockName}")]
        public IActionResult GetBlock(string blockName)
        {
            var filePath = Path.Combine(_siemensDataPath, blockName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Block not found.");
            }

            var xmlContent = System.IO.File.ReadAllText(filePath);
            return Content(xmlContent, "application/xml");
        }
    }
}
