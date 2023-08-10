using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Models;

namespace CsharpServiceTemplateNetCore.Api.V1;

public static class WeatherForecastApi
{
    public static WebApplication MapWeatherForecastApiV1
    (
        this WebApplication app
    )
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        
        app.MapGet( "/v{version:apiVersion}/weatherforecast", async () =>
            {
                await Task.Delay(1000);
                return Enumerable.Range( 1, 5 ).Select( index =>
                    new WeatherForecast
                    (
                        DateTime.Now.AddDays( index ),
                        Random.Shared.Next( -20, 55 ),
                        summaries[Random.Shared.Next( summaries.Length )]
                    ) );
            } )
            .WithName("GetWeatherForeCast")
            .WithTags(Tags.WeatherForecast)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(1.0)
            .AllowAnonymous()
            .Produces<IEnumerable<WeatherForecast>>()
            .Produces<Error>(400)
            .Produces<Error>(500);
            
            
        app.MapPost( "/v{version:apiVersion}/weatherforecast", 
                async (WeatherForecast forecast) =>
            {
                await Task.Delay(1000);
                return true;
            } )
            .WithName("CreateWeatherForeCast")
            .WithTags(Tags.WeatherForecast)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .Accepts<WeatherForecast>("application/json")
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(1.0)
            .AllowAnonymous()
            .Produces<bool>()
            .Produces<Error>(400)
            .Produces<Error>(500);
        
        app.MapPut( "/v{version:apiVersion}/weatherforecast", 
                async (WeatherForecast forecast) =>
                {
                    await Task.Delay(1000);
                    return true;
                } )
            .WithName("UpdateWeatherForeCast")
            .WithTags(Tags.WeatherForecast)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .Accepts<WeatherForecast>("application/json")
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(1.0)
            .AllowAnonymous()
            .Produces<bool>()
            .Produces<Error>(400)
            .Produces<Error>(500);
        
        
        app.MapDelete( "/v{version:apiVersion}/weatherforecast", 
                () => { } )
            .WithName("DeleteWeatherForeCast")
            .WithTags(Tags.WeatherForecast)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .HasApiVersion(1.0)
            .AllowAnonymous()
            .Produces<bool>()
            .Produces<Error>(400)
            .Produces<Error>(500);

        return app;
    }
}