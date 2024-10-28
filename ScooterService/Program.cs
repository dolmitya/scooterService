using ScooterService.loC;

var builder = WebApplication.CreateBuilder(args);
DbContextConfiguration.ConfigureServices(builder);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

var app = builder.Build();
DbContextConfiguration.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.Run();