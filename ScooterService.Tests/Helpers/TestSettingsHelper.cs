using ScooterService.Settings;

namespace ScooterService.Tests.Helpers;

public class TestSettingsHelper
{
    public static ScooterServiceSettings GetSettings()
    {
        return ScooterServiceSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}