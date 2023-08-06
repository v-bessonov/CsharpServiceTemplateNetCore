using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class WebApplicationBuilderDi
{
    public static WebApplicationBuilder SetKestrel
    (
        this WebApplicationBuilder builder
    )
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(9001, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;

            });
        });

        return builder;
    }
    
    public static WebApplicationBuilder SetServices
    (
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddHostedServices()
            .AddApplicationServices()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddHealthChecks();

        return builder;
    }
}