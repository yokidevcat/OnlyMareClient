using OnlyMare.LightlessConfiguration.Models;

namespace OnlyMare.LightlessConfiguration.Configurations;

public class ServerTagConfig : ILightlessConfiguration
{
    public Dictionary<string, ServerTagStorage> ServerTagStorage { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public int Version { get; set; } = 0;
}