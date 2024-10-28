using Microsoft.EntityFrameworkCore;
using ScooterDataAccess;

namespace ScooterService.loC;

public class DbContextConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        var connectionString = configuration.GetValue<string>("ScooterServiceContext");

        builder.Services.AddDbContextFactory<ScooterDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ScooterDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}