namespace ScooterService.Settings;

public class ScooterServiceSettingsReader
{
    public static ScooterServiceSettings Read(IConfiguration configuration)
    {
        return new ScooterServiceSettings
        {
            ScooterServiceDbContextConnectionString = configuration.GetValue<string>("AirTicketDbContext"),
            ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri")
        };
    }
}