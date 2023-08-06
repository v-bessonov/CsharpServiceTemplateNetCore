using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;

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
        
        builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
        {
            Description = "Todo web api implementation using Minimal Api in Asp.Net Core",
            Title = "CsharpServiceTemplateNetCore",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "vbessonov",
                Url = new Uri("https://github.com")
            }
        }));

        return builder;
    }
}