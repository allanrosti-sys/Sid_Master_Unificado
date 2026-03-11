namespace SID_WEBAPI.Models;

public sealed class PortalProxyRequestDto
{
    // Base configurada no portal (whitelist): puchta_panel | puchta_backend | puchta_frontend
    public required string Base { get; init; }

    // Caminho relativo a partir da base (ex.: /api/version)
    public required string Path { get; init; }

    // GET | POST | PUT (MVP)
    public string Method { get; init; } = "GET";

    // Corpo opcional para POST/PUT (deve ser serializável em JSON)
    public object? Body { get; init; }
}

