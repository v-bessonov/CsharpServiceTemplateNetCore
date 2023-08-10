using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Models;

namespace CsharpServiceTemplateNetCore.Api.V2;

public static class BizErrorApi
{
    public static WebApplication MapBizErrorApiV2
    (
        this WebApplication app
    )
    {
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
            .AllowAnonymous()
            .Produces<DateTime>()
            .Produces<Error>(400)
            .Produces<Error>(500);

        return app;
    }
}