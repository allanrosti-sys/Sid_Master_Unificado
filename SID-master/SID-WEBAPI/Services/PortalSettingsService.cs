using System.Text.Json;

namespace SID_WEBAPI.Services;

public sealed class PortalSettingsService : IPortalSettingsService
{
    private static readonly object FileLock = new();
    private readonly string _settingsFilePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
    };

    public PortalSettingsService(IWebHostEnvironment env)
    {
        // Armazena as configurações em uma pasta local do backend (sem depender do frontend).
        var dataDir = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDir);
        _settingsFilePath = Path.Combine(dataDir, "portal_settings.json");
    }

    public PortalSettings GetSettings()
    {
        lock (FileLock)
        {
            if (!File.Exists(_settingsFilePath))
            {
                var defaults = new PortalSettings();
                Persist(defaults);
                return defaults;
            }

            try
            {
                var json = File.ReadAllText(_settingsFilePath);
                var loaded = JsonSerializer.Deserialize<PortalSettings>(json, _jsonOptions);
                return Normalize(loaded ?? new PortalSettings());
            }
            catch
            {
                // Se o arquivo estiver corrompido, voltamos ao padrão para não travar a UI.
                var defaults = new PortalSettings();
                Persist(defaults);
                return defaults;
            }
        }
    }

    public void UpdateSettings(PortalSettings settings)
    {
        lock (FileLock)
        {
            Persist(Normalize(settings ?? new PortalSettings()));
        }
    }

    private void Persist(PortalSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, _jsonOptions);
        File.WriteAllText(_settingsFilePath, json);
    }

    private static PortalSettings Normalize(PortalSettings settings)
    {
        // Normaliza entradas do usuário para evitar problemas de URL.
        settings.PuchtaPanelUrl = (settings.PuchtaPanelUrl ?? string.Empty).Trim();
        settings.PuchtaBackendUrl = (settings.PuchtaBackendUrl ?? string.Empty).Trim();
        settings.PuchtaFrontendUrl = (settings.PuchtaFrontendUrl ?? string.Empty).Trim();
        return settings;
    }
}

