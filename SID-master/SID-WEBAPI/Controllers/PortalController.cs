using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SID_WEBAPI.Models;
using SID_WEBAPI.Services;

namespace SID_WEBAPI.Controllers;

[ApiController]
[Route("api/portal")]
public class PortalController : ControllerBase
{
    private readonly IPortalSettingsService _settingsService;
    private readonly IHttpClientFactory _httpClientFactory;

    public PortalController(IPortalSettingsService settingsService, IHttpClientFactory httpClientFactory)
    {
        _settingsService = settingsService;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        return Ok(_settingsService.GetSettings());
    }

    [HttpPut("settings")]
    public IActionResult UpdateSettings([FromBody] PortalSettings settings)
    {
        _settingsService.UpdateSettings(settings);
        return Ok(new { message = "Configurações atualizadas com sucesso." });
    }

    // Mantido por compatibilidade com o frontend MVP (algumas telas usam POST).
    [HttpPost("settings")]
    public IActionResult UpdateSettingsPost([FromBody] PortalSettings settings)
    {
        return UpdateSettings(settings);
    }

    // Proxy reverso seguro (MVP) para contornar CORS e centralizar chamadas ao ecossistema Puchta.
    [HttpPost("proxy")]
    public async Task<IActionResult> ProxyRequest([FromBody] PortalProxyRequestDto request)
    {
        var settings = _settingsService.GetSettings();

        string? baseUrl = request.Base switch
        {
            "puchta_panel" => settings.PuchtaPanelUrl,
            "puchta_backend" => settings.PuchtaBackendUrl,
            "puchta_frontend" => settings.PuchtaFrontendUrl,
            _ => null
        };

        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            return BadRequest(new { error = "Base de URL inválida ou não configurada." });
        }

        if (string.IsNullOrWhiteSpace(request.Path))
        {
            return BadRequest(new { error = "Path não pode ser vazio." });
        }

        if (request.Path.Contains("..", StringComparison.Ordinal))
        {
            return BadRequest(new { error = "Path inválido." });
        }

        var verb = (request.Method ?? "GET").Trim().ToUpperInvariant();
        if (verb is not ("GET" or "POST" or "PUT"))
        {
            return BadRequest(new { error = "Método HTTP não permitido no MVP. Use GET/POST/PUT." });
        }

        // Normaliza a URL base e o caminho para evitar barras duplas.
        var targetUrl = $"{baseUrl.TrimEnd('/')}/{request.Path.TrimStart('/')}";

        try
        {
            using var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(8);

            var method = new HttpMethod(verb);
            var httpRequest = new HttpRequestMessage(method, targetUrl);

            if (request.Body != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var jsonBody = JsonSerializer.Serialize(request.Body);
                httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            var response = await client.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            object data;
            try
            {
                data = JsonSerializer.Deserialize<object>(content) ?? new { };
            }
            catch
            {
                data = content;
            }

            return StatusCode((int)response.StatusCode, new
            {
                target = targetUrl,
                status = (int)response.StatusCode,
                data
            });
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { error = "Erro ao conectar ao serviço externo.", details = ex.Message, target = targetUrl });
        }
    }
}

