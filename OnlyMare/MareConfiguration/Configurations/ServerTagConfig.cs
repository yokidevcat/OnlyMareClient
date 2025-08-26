using OnlyMare.OnlyMareConfiguration.Models;

namespace OnlyMare.OnlyMareConfiguration.Configurations;

public class ServerTagConfig : IOnlyMareConfiguration
{
    public Dictionary<string, ServerTagStorage> ServerTagStorage { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public int Version { get; set; } = 0;
}