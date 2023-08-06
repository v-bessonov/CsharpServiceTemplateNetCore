namespace CsharpServiceTemplateNetCore.Api;

public static class BizErrorApi
{
    public static WebApplication MapBizErrorApi
    (
        this WebApplication app
    )
    {
        app.MapGet("/bizerror", async () =>
            {
                await Task.Delay(1000);
                throw new InvalidOperationException("BizError was thrown");
            })
            .WithName("BizError")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
        
            })
            .Produces<DateTime>()
            .Produces(400)
            .Produces(500);
        
        app.MapGet("/genericerror", async () =>
            {
                await Task.Delay(1000);
                throw new InvalidDataException("GenericError was thrown");
            })
            .WithName("GenericError")
            .WithTags("Errors")
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