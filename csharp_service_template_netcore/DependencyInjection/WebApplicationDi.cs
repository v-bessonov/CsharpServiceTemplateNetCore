using CsharpServiceTemplateNetCore.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class WebApplicationDi
{
    
    public static WebApplication SetDevelopmentEnvironment
    (
        this WebApplication app
    )
    {
        if (app.Environment.IsDevelopment())
        {
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        return app;
    }
    
    public static WebApplication SetMetrics
    (
        this WebApplication app
    )
    {
        app.UseMetricServer();
        app.UseHttpMetrics();

        return app;
    }
    
    public static WebApplication SetHealthCheck
    (
        this WebApplication app
    )
    {
        app.MapHealthChecks("/healthz", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });

        return app;
    }
    public static WebApplication SetMiddleware
    (
        this WebApplication app
    )
    {
        app.UseMiddleware<DateTimeMiddleware>();

        return app;
    }
    
    public static WebApplication SetExceptionHandler
    (
        this WebApplication app
    )
    {
        app.UseExceptionHandler(exceptionHandlerApp =>  
        {  
            exceptionHandlerApp.Run(async context =>  
            {  
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;  
  
                var exceptionHandlerPathFeature =  
                    context.Features.Get<IExceptionHandlerPathFeature>();  
  
                if (exceptionHandlerPathFeature?.Error is InvalidOperationException)  
                {  
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message 
                                                      ??  "An exception was thrown."); 
                }
                else
                {
                    await context.Response.WriteAsync("An exception was thrown."); 
                }
            });  
        }); 

        return app;
    }
}