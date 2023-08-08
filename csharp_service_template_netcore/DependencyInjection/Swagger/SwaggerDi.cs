using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CsharpServiceTemplateNetCore.DependencyInjection.Swagger;

public static class SwaggerDi
{
    public static ApiVersionSet GetApiVersionSet
    (
        this WebApplication app
    )
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .HasApiVersion(new ApiVersion(2.0))
            .ReportApiVersions()
            .Build();
    }

    public static WebApplicationBuilder AddSwaggerServices
    (
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
        return builder;
    }

    public static WebApplication SetDevelopmentEnvironment
    (
        this WebApplication app
    )
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    var descriptions = app.DescribeApiVersions();
        
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
        
                    options.RoutePrefix = string.Empty;
                });
        }

        return app;
    }
}