using System.Text;
using CsharpServiceTemplateNetCore.Api.V1;
using CsharpServiceTemplateNetCore.Api.V2;
using CsharpServiceTemplateNetCore.Api.VersionNeutral;
using CsharpServiceTemplateNetCore.DependencyInjection;
using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args)
    .SetKestrel()
    .SetServices()
    .SetAddApiVersioning()
    .AddSwaggerServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build()
    .SetMiddleware()
    .SetMetrics()
    .SetHealthCheck()
    .SetExceptionHandler()
    .MapAuthApi()
    .MapDateTimeApi()
    .MapBizErrorApiV1()
    .MapBizErrorApiV2()
    .MapWeatherForecastApiV1()
    .SetDevelopmentEnvironment();

app.UseAuthentication();
app.UseAuthorization();

app.Run();