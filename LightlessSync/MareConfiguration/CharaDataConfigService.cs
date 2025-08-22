using LightlessSync.MareConfiguration.Configurations;

namespace LightlessSync.MareConfiguration;

public class CharaDataConfigService : ConfigurationServiceBase<CharaDataConfig>
{
    public const string ConfigName = "charadata.json";

    public CharaDataConfigService(string configDir) : base(configDir) { }
    public override string ConfigurationName => ConfigName;
}