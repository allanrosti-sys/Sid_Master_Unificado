using Microsoft.AspNetCore.Mvc;

namespace SID_WEBAPI.Controllers;

[ApiController]
[Route("api/library")]
public class LibraryController : ControllerBase
{
    [HttpGet("components")]
    public IActionResult GetComponents()
    {
        // Mock de dados para validação da UI
        var components = new[]
        {
            new 
            { 
                id = "comp-001", 
                name = "Bloco Motor Padrão", 
                description = "Controle de partida direta com reversão e feedback de falha.",
                tags = new[] { "siemens", "motor", "standard" },
                version = "1.2.0"
            },
            new 
            { 
                id = "comp-002", 
                name = "Válvula PID", 
                description = "Bloco de controle PID para válvulas proporcionais com ajuste automático.",
                tags = new[] { "rockwell", "pid", "analog" },
                version = "2.0.0"
            }
        };

        // Simula um pequeno delay para testar o estado de 'Loading' na UI
        System.Threading.Thread.Sleep(500);

        return Ok(components);
    }
}