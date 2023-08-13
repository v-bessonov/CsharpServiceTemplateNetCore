using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class KestrelDi
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
                if (Convert.ToBoolean(builder.Configuration.GetSection("https").Value))
                {
                    listenOptions.UseHttps();
                }

            });
        });

        return builder;
    }
    
    
    public static WebApplication UseHttps
    (
        this WebApplication app
    )
    {
        if (Convert.ToBoolean(app.Configuration.GetSection("https").Value))
        {
            app.UseHttpsRedirection(); 
        }

        return app;
    }
}