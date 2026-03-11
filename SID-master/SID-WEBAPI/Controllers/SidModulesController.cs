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
        // MVP: inventário inicial dos módulos do SID-master para a UI do portal.
        // A ideia aqui é permitir navegação e diagnóstico, mesmo antes da migração completa.
        var modules = new List<SidModuleInfo>
        {
            new()
            {
                Id = "sid-clickup",
                Name = "SID ClickUp",
                Description = "Integração para exportação/importação de tarefas do ClickUp.",
                Status = "MVP (Diagnóstico)",
                Icon = "ClipboardList",
                HasAction = true,
                ActionEndpoint = "/api/sid/clickup/status"
            },
            new()
            {
                Id = "sid-complex",
                Name = "SID Complex",
                Description = "Gerenciamento de tabelas e lógica complexa de engenharia.",
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
                Status = "MVP (Diagnóstico)",
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
            status = "Não configurado",
            message = "No MVP, a integração está em modo diagnóstico. Configure a chave quando a tela de credenciais estiver pronta."
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
        // MVP: validação simulada, apenas para exercitar UI/fluxo.
        return Ok(new { status = "OK", message = "String de conexão válida (simulação)." });
    }

    [HttpGet("complex/entities")]
    public IActionResult GetComplexEntities()
    {
        // MVP: dados simulados para exercitar tabelas/visualização.
        var entities = new[]
        {
            new { name = "tblAreas", rows = 15, lastUpdate = DateTime.UtcNow.AddDays(-1) },
            new { name = "tblMotors", rows = 120, lastUpdate = DateTime.UtcNow },
            new { name = "tblValves", rows = 45, lastUpdate = DateTime.UtcNow }
        };
        return Ok(entities);
    }
}

