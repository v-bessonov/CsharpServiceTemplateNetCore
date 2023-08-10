using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Interfaces;

namespace CsharpServiceTemplateNetCore.Api.VersionNeutral;

public static class DateTimeApi
{
    public static WebApplication MapDateTimeApi
    (
        this WebApplication app
    )
    {
        app.MapGet("/tomorrow", async (IDateTimeService dateTimeService) =>
            {
                await Task.Delay(1000);
                return dateTimeService.Now().AddDays(1);
            })
            .WithName("Tomorrow")
            .WithTags(Tags.DateTime)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description",
        
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .IsApiVersionNeutral()
            .RequireAuthorization()
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);
        
        app.MapGet("/now", async (IDateTimeService dateTimeService) =>
            {
                await Task.Delay(1000);
                return dateTimeService.Now();
            })
            .WithName("Now")
            .WithTags(Tags.DateTime)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description",
        
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .IsApiVersionNeutral()
            .AllowAnonymous()
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);
        
        app.MapGet("/yesterday", async (IDateTimeService dateTimeService) =>
            {
                await Task.Delay(1000);
                return dateTimeService.Now().AddDays(-1);
            })
            .WithName("Yesterday")
            .WithTags(Tags.DateTime)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description",
                Deprecated = true
        
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .IsApiVersionNeutral()
            .AllowAnonymous()
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);
        
        
        app.MapGet("/daybeforeyesterday", async (IDateTimeService dateTimeService) =>
            {
                await Task.Delay(1000);
                return dateTimeService.Now().AddDays(-2);
            })
            .WithName("DayBeforeYesterday")
            .WithTags(Tags.DateTime)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description",
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .IsApiVersionNeutral()
            .ExcludeFromDescription()
            .AllowAnonymous()
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);

        return app;
    }
}