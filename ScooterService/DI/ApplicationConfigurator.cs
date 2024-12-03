using ScooterService.loC;
using ScooterService.Settings;

namespace ScooterService.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, ScooterServiceSettings ScooterServiceSettings)
    {
        DbContextConfiguration.ConfigureServices(builder);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder);
        ServicesConfigurator.ConfigureServices(builder.Services, ScooterServiceSettings);
        AuthorizationConfigurator.ConfigureServices(builder.Services, ScooterServiceSettings);
        
        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfiguration.ConfigureApplication(app);
        AuthorizationConfigurator.ConfigureApplication(app);
        
        app.UseHttpsRedirection();
        app.MapControllers();
    }
}