using Microsoft.AspNetCore.Mvc;
using SID_WEBAPI.Models;

namespace SID_WEBAPI.Controllers;

[ApiController]
[Route("api/sid")]
public class SidModulesController : ControllerBase
{
    [HttpGet("modules")]
    public IActionResult GetModules()
    {
        // MVP: inventÃ¡rio inicial dos mÃ³dulos do SID-master para a UI do portal.
        // A ideia aqui Ã© permitir navegaÃ§Ã£o e diagnÃ³stico, mesmo antes da migraÃ§Ã£o completa.
        var modules = new List<SidModuleInfo>
        {
            new()
            {
                Id = "sid-clickup",
                Name = "SID ClickUp",
                Description = "IntegraÃ§Ã£o para exportaÃ§Ã£o/importaÃ§Ã£o de tarefas do ClickUp.",
                Status = "MVP (DiagnÃ³stico)",
                Icon = "ClipboardList",
                HasAction = true,
                ActionEndpoint = "/api/sid/clickup/status"
            },
            new()
            {
                Id = "sid-complex",
                Name = "SID Complex",
                Description = "Gerenciamento de tabelas e lÃ³gica complexa de engenharia.",
                Status = "MVP (Mock)",
                Icon = "Puzzle",
                HasAction = true,
                ActionEndpoint = "/api/sid/complex/entities"
            },
            new()
            {
                Id = "sid-msql",
                Name = "SID MSQL",
                Description = "Conector para bancos de dados Microsoft SQL Server.",
                Status = "MVP (Simulado)",
                Icon = "Database",
                HasAction = true,
                ActionEndpoint = "/api/sid/db/validate"
            },
            new()
            {
                Id = "sid-rockwell",
                Name = "SID TPM Rockwell",
                Description = "Ferramentas para processamento de arquivos .L5X e .L5K.",
                Status = "MVP (DiagnÃ³stico)",
                Icon = "Factory",
                HasAction = true,
                ActionEndpoint = "/api/sid/rockwell/status"
            },
            new()
            {
                Id = "sid-pgtemplate",
                Name = "SID PGTemplate",
                Description = "Gerador de templates para TIA Portal (Openness).",
                Status = "Planejado",
                Icon = "FileText",
                HasAction = false,
                ActionEndpoint = null
            }
        };

        return Ok(modules);
    }

    [HttpGet("clickup/status")]
    public IActionResult GetClickUpStatus()
    {
        return Ok(new
        {
            status = "NÃ£o configurado",
            message = "No MVP, a integraÃ§Ã£o estÃ¡ em modo diagnÃ³stico. Configure a chave quando a tela de credenciais estiver pronta."
        });
    }

    [HttpGet("rockwell/status")]
    public IActionResult GetRockwellStatus()
    {
        return Ok(new
        {
            status = "Aguardando arquivo",
            message = "Nenhum arquivo .L5X/.L5K carregado no contexto atual."
        });
    }

    [HttpGet("db/validate")]
    public IActionResult ValidateDb()
    {
        // MVP: validaÃ§Ã£o simulada, apenas para exercitar UI/fluxo.
        return Ok(new { status = "OK", message = "String de conexÃ£o vÃ¡lida (simulaÃ§Ã£o)." });
    }

    [HttpGet("complex/entities")]
    public IActionResult GetComplexEntities()
    {
        // MVP: dados simulados para exercitar tabelas/visualizaÃ§Ã£o.
        var entities = new[]
        {
            new { name = "tblAreas", rows = 15, lastUpdate = DateTime.UtcNow.AddDays(-1) },
            new { name = "tblMotors", rows = 120, lastUpdate = DateTime.UtcNow },
            new { name = "tblValves", rows = 45, lastUpdate = DateTime.UtcNow }
        };
        return Ok(entities);
    }
}

