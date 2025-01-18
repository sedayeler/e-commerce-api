﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerceAPI.API.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication app, ILogger<T> logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(contextFeature.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCodeContext = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "An error occurred."
                        }));
                    }
                });
            });
        }
    }
}
