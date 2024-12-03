using AutoMapper;
using BL.Authorization;
using BL.Users.Manager;
using BL.Users.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScooterDataAccess;
using ScooterDataAccess.Entities;
using ScooterDataAccess.Repository;
using ScooterService.Settings;

namespace ScooterService.loC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, ScooterServiceSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<UserEntity>>(x => 
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<ScooterDbContext>>()));
        services.AddScoped<IUsersProvider>(x => 
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), 
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
        
        services.AddScoped<IAuthProvider>(x =>
            new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                settings.IdentityServerUri!,
                settings.ClientId!,
                settings.ClientSecret!,
                x.GetRequiredService<IMapper>()
            ));
    }
}