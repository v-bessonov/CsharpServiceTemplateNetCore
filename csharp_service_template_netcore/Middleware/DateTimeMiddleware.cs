using System.Globalization;
using CsharpServiceTemplateNetCore.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace CsharpServiceTemplateNetCore.Middleware;

public class DateTimeMiddleware
{
    private readonly RequestDelegate _next;
 
    public DateTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task InvokeAsync(HttpContext context, IDateTimeService dateTimeService)
    {
        if ((context.Request.Path.Value?.ToLower() ?? string.Empty) == "/now")
        {
            context.Response.Headers.Add("X-Time", 
                dateTimeService.Now().ToString(CultureInfo.InvariantCulture));
        }
        
        var token = await context.GetTokenAsync("access_token");
        // in case of distributed systems try to get token from redis cache
        // and if it not there throw unauthorized exception
        
        await _next.Invoke(context);
    }
}