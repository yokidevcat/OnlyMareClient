using OnlyMare.OnlyMareConfiguration.Models;

namespace OnlyMare.OnlyMareConfiguration.Configurations;

public class UidNotesConfig : IOnlyMareConfiguration
{
    public Dictionary<string, ServerNotesStorage> ServerNotes { get; set; } = new(StringComparer.Ordinal);
    public int Version { get; set; } = 0;
}
