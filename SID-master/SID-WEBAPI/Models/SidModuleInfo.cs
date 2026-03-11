namespace SID_WEBAPI.Models;

public sealed class SidModuleInfo
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Status { get; init; }
    public required string Icon { get; init; }
    public required bool HasAction { get; init; }
    public string? ActionEndpoint { get; init; }
}

