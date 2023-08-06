using CsharpServiceTemplateNetCore.Interfaces;

namespace CsharpServiceTemplateNetCore.Api;

public static class DateTimeApi
{
    public static WebApplication MapDateTimeApi
    (
        this WebApplication app
    )
    {
        app.MapGet("/now", async (IDateTimeService dateTimeService) =>
            {
                await Task.Delay(1000);
                return dateTimeService.Now();
            })
            .WithName("Nowt")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
        
            })
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);

        return app;
    }
}