namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class CorsDi
{
    public static WebApplicationBuilder SetCors
    (
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddCors();
        
        return builder;
    }
    
    public static WebApplication SetCors
    (
        this WebApplication app
    )
    {
        app.UseCors();

        return app;
    }
}