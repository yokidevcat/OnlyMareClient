using OnlyMare.OnlyMareConfiguration.Configurations;

namespace OnlyMare.OnlyMareConfiguration;

public interface IConfigService<out T> : IDisposable where T : IOnlyMareConfiguration
{
    T Current { get; }
    string ConfigurationName { get; }
    string ConfigurationPath { get; }
    public event EventHandler? ConfigSave;
    void UpdateLastWriteTime();
}
