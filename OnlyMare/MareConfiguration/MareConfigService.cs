using OnlyMare.OnlyMareConfiguration.Configurations;

namespace OnlyMare.OnlyMareConfiguration;

public class OnlyMareConfigService : ConfigurationServiceBase<OnlyMareConfig>
{
    public const string ConfigName = "config.json";

    public OnlyMareConfigService(string configDir) : base(configDir)
    {
    }

    public override string ConfigurationName => ConfigName;
}