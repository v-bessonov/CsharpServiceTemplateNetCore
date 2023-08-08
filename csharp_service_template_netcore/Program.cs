using CsharpServiceTemplateNetCore.Api;
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
    .MapBizErrorApi()
    .SetDevelopmentEnvironment();

app.Run();