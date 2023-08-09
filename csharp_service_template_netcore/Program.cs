using CsharpServiceTemplateNetCore.Api.V1;
using CsharpServiceTemplateNetCore.Api.V2;
using CsharpServiceTemplateNetCore.Api.VersionNeutral;
using CsharpServiceTemplateNetCore.DependencyInjection;
using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;

var builder = WebApplication.CreateBuilder(args)
    .SetKestrel()
    .SetServices()
    .SetAddApiVersioning()
    .AddSwaggerServices();

var app = builder.Build()
    .SetMiddleware()
    .SetMetrics()
    .SetHealthCheck()
    .SetExceptionHandler()
    .MapDateTimeApi()
    .MapBizErrorApiV1()
    .MapBizErrorApiV2()
    .MapWeatherForecastApiV1()
    .SetDevelopmentEnvironment();

app.Run();