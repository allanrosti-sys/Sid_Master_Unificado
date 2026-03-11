using Microsoft.AspNetCore.Mvc;

namespace SID_WEBAPI.Controllers;

[ApiController]
[Route("api")]
public class SystemController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        // Retorna status ok simples para validação de uptime
        return Ok(new { status = "ok", timestampUtc = DateTime.UtcNow });
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        // Retorna versão do serviço unificado
        return Ok(new { version = "1.0.0-mvp" });
    }
}
