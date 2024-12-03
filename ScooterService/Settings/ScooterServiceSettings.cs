namespace ScooterService.Settings;

public class ScooterServiceSettings
{
    public string? ScooterServiceDbContextConnectionString { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? IdentityServerUri { get; set; }
}