using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Models;

namespace CsharpServiceTemplateNetCore.Api;

public static class BizErrorApi
{
    public static WebApplication MapBizErrorApi
    (
        this WebApplication app
    )
    {
        app.MapGet("/v{version:apiVersion}/bizerror", async () =>
            {
                await Task.Delay(1000);
                throw new InvalidOperationException("BizError was thrown");
            })
            .WithName("BizError")
            .WithTags(Tags.Errors)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(1.0)
            .HasApiVersion(2.0)
            .Produces<DateTime>()
            .Produces<Error>(400)
            .Produces<Error>(500);
        
        app.MapGet("/v{version:apiVersion}/genericerror", async () =>
            {
                await Task.Delay(1000);
                throw new InvalidDataException("GenericError was thrown");
            })
            .WithName("GenericError")
            .WithTags(Tags.Errors)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(2.0)
            .Produces<DateTime>()
            .Produces<Error>(400)
            .Produces<Error>(500);

        return app;
    }
}