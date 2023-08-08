using CsharpServiceTemplateNetCore.Middleware;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class MiddlewareDi
{
    public static WebApplication SetMiddleware
    (
        this WebApplication app
    )
    {
        app.UseMiddleware<DateTimeMiddleware>();

        return app;
    } 
}