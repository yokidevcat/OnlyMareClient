using OnlyMare.LightlessConfiguration.Configurations;

namespace OnlyMare.LightlessConfiguration;

public class LightlessConfigService : ConfigurationServiceBase<LightlessConfig>
{
    public const string ConfigName = "config.json";

    public LightlessConfigService(string configDir) : base(configDir)
    {
    }

    public override string ConfigurationName => ConfigName;
}