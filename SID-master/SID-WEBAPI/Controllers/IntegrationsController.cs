using Microsoft.AspNetCore.Mvc;

namespace SID_WEBAPI.Controllers;

[ApiController]
[Route("api/integrations")]
public class IntegrationsController : ControllerBase
{
    // Retorna o status consolidado de todos os módulos externos
    [HttpGet("status")]
    public IActionResult GetIntegrationsStatus()
    {
        // Simulando verificação em tempo real de múltiplos serviços
        // Em produção, isso testaria conexões reais de socket/http/db
        
        var statuses = new List<object>
        {
            new { 
                module = "Siemens TIA Portal", 
                status = "online", 
                details = "Openness API v17 detectada. Pronto para exportação.",
                lastCheck = DateTime.Now 
            },
            new { 
                module = "Rockwell Automation", 
                status = "warning", 
                details = "L5X Parser ativo, mas Studio 5000 não detectado no host.",
                lastCheck = DateTime.Now 
            },
            new { 
                module = "ClickUp API", 
                status = "online", 
                details = "Conectado. Token válido. Sincronização de tarefas ativa.",
                lastCheck = DateTime.Now.AddMinutes(-2) 
            },
            new { 
                module = "SID Complex DB", 
                status = "error", 
                details = "Não foi possível conectar ao SQL Server local (timeout).",
                lastCheck = DateTime.Now 
            }
        };

        return Ok(new 
        { 
            summary = "2 Online, 1 Warning, 1 Error",
            modules = statuses 
        });
    }
}