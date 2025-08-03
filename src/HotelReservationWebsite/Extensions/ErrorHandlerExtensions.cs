using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionHandlerExtensions
{
    public static void UseJsonExceptionHandler(this IApplicationBuilder app, bool includeStackTrace = false)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionHandlerFeature?.Error != null)
                {
                    var error = exceptionHandlerFeature.Error;

                    var response = new
                    {
                        Message = "An unexpected error occurred.",
                        Detail = error.Message,
                        StackTrace = includeStackTrace ? error.StackTrace : null
                    };

                    var options = new JsonSerializerOptions { IgnoreNullValues = true };
                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
            });
        });
    }
}