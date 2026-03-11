using System.Text.Json.Serialization;

namespace SID_WEBAPI.Services;

public interface IPortalSettingsService
{
    PortalSettings GetSettings();
    void UpdateSettings(PortalSettings settings);
}

public sealed class PortalSettings
{
    // URLs do ecossistema Puchta. São configuradas pelo usuário no portal do SID.
    [JsonPropertyName("puchtaPanelUrl")]
    public string PuchtaPanelUrl { get; set; } = "http://localhost:8099";

    [JsonPropertyName("puchtaBackendUrl")]
    public string PuchtaBackendUrl { get; set; } = "http://localhost:8021";

    [JsonPropertyName("puchtaFrontendUrl")]
    public string PuchtaFrontendUrl { get; set; } = "http://localhost:5173";
}

