using System.Globalization;
using CsharpServiceTemplateNetCore.Interfaces;

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
        await _next.Invoke(context);
    }
}