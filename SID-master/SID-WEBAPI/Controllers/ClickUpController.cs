using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SID_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClickUpController : ControllerBase
    {
        // TODO: Handle the API key securely. Do not hardcode it.
        private const string ApiKey = "pk_49109101_OHUDNFPPQ9WKNJU14ULMYVYX4OB8YGBD";

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            // This is a placeholder implementation.
            // The actual implementation should use an HTTP client to call the ClickUp API.
            var tasks = new List<object>
            {
                new { name = "Task 1", time_estimate = "2h" },
                new { name = "Task 2", time_estimate = "4h" }
            };

            return Ok(tasks);
        }
    }
}
