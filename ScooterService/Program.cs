using ScooterService.DI;
using ScooterService.Settings;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var settings = ScooterServiceSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

ApplicationConfigurator.ConfigureServices(builder, settings);

var app = builder.Build();

ApplicationConfigurator.ConfigureApplication(app);

app.Run();