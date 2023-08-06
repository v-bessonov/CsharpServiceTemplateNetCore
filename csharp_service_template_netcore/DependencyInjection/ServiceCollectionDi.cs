using CsharpServiceTemplateNetCore.Interfaces;
using CsharpServiceTemplateNetCore.Services;
using CsharpServiceTemplateNetCore.Workers;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class ServiceCollectionDi
{
    
    public static IServiceCollection AddApplicationServices
    (
        this IServiceCollection services
    )
    {
        services.AddScoped<IDateTimeService, DateTimeService>();

        return services;
    }
    
    public static IServiceCollection AddHostedServices
    (
        this IServiceCollection services
    )
    {
        services.AddHostedService<HeartBeat>();
        services.AddHostedService<ScheduleHeartBeat>();

        return services;
    }
    
}