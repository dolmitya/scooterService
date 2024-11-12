using ScooterService.loC;
using ScooterService.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var settings = ScooterServiceSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);
DbContextConfiguration.ConfigureServices(builder);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

MapperConfigurator.ConfigureServices(builder.Services);
ServicesConfigurator.ConfigureServices(builder.Services, settings);

builder.Services.AddControllers();

var app = builder.Build();
DbContextConfiguration.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.Run();