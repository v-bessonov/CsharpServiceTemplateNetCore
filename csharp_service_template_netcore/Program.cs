using CsharpServiceTemplateNetCore.Api;
using CsharpServiceTemplateNetCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args)
    .SetKestrel()
    .SetServices();


var app = builder.Build()
    .SetDevelopmentEnvironment()
    .SetMiddleware()
    .SetMetrics()
    .SetHealthCheck()
    .SetExceptionHandler()
    .MapDateTimeApi()
    .MapBizErrorApi();


app.Run();