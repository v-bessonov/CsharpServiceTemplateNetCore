using System.Web.Http;
using CsharpServiceTemplateNetCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class ExceptionHandlerDi
{
    public static WebApplication SetExceptionHandler
    (
        this WebApplication app
    )
    {
        app.UseExceptionHandler(exceptionHandlerApp =>  
        {  
            exceptionHandlerApp.Run(async context =>  
            {  
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;  
  
                var exceptionHandlerPathFeature =  
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var httpError = exceptionHandlerPathFeature?.Error;
                var error = new Error
                {
                    Code = 100,
                    Message = httpError?.Message ?? "An exception was thrown.",
                    //StackTrace = httpError?.StackTrace
                };
                

                if (httpError is HttpResponseException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    error.Code = 401;
                }
                else if (httpError is InvalidOperationException)  
                {  
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    error.Code = 400;
                }
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error)); 
            });  
        }); 

        return app;
    }
}