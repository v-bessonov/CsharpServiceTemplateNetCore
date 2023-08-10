using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Models;

namespace CsharpServiceTemplateNetCore.Api.V1;

public static class BizErrorApi
{
    public static WebApplication MapBizErrorApiV1
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
            .AllowAnonymous()
            .Produces<DateTime>()
            .Produces<Error>(400)
            .Produces<Error>(500);

        return app;
    }
}