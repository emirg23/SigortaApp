using SigortaApp.Repositories;
using SigortaApp.Repositories.Interfaces;
using SigortaApp.Services;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Extensions;

public static class StartupExtensions
{
    public static void ConfigureStartup(this IServiceCollection services)
    {
        services.AddServices();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IInsuranceRepository, InsuranceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IInsuranceService, InsuranceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVehicleService, VehicleService>();
    }
}
