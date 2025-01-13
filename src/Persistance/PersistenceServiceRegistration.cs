using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistanceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                .AddEntityFrameworkConfig(configuration);
    }

    private static IServiceCollection AddEntityFrameworkConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}
