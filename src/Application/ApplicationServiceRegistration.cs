using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        return services
            .AddMediatRConfig()
            .AddMapsterConfig()
            .AddFluentValidationConfig();
    }

    private static IServiceCollection AddMediatRConfig(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceRegistration).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            //configuration.AddOpenBehavior(typeof(AuthPipelineBehavior<,>));
            //configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });
        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceRegistration).Assembly;
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }


}
