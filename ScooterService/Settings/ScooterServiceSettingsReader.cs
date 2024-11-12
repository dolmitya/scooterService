namespace ScooterService.Settings;

public class ScooterServiceSettingsReader
{
    public static ScooterServiceSettings Read(IConfiguration configuration)
    {
        return new ScooterServiceSettings()
        {
            ServiceUri = configuration.GetValue<Uri>("Uri"),
            ScooterServiceDbContextConnectionString = configuration.GetValue<string>("HotelChainDbContext")
        };
    }
}