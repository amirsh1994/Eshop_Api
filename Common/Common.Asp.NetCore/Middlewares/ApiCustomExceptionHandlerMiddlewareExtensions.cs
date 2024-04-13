﻿using System.Net;
using Common.Application.Validation;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.AspNetCore.Middlewares;

public static class ApiCustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseApiCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiCustomExceptionHandlerMiddleware>();
    }
}



public class ApiCustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiCustomExceptionHandlerMiddleware> _logger;
    private readonly IHostingEnvironment _env;

    public ApiCustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiCustomExceptionHandlerMiddleware> logger, IHostingEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        AppStatusCode apiStatusCode = AppStatusCode.ServerError;

        try
        {
            await _next(context);
        }
        catch (InvalidDomainDataException exception)
        {
            _logger.LogError(exception, exception.Message);
            apiStatusCode = AppStatusCode.LogicError;
            SetErrorMessage(exception);
            await WriteToResponseAsync();

        }
        catch (InvalidCommandException exception)
        {
            _logger.LogError(exception, exception.Message);
            httpStatusCode = HttpStatusCode.BadRequest;
            SetErrorMessage(exception);
            await WriteToResponseAsync();

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            SetErrorMessage(exception);
            await WriteToResponseAsync();

        }
        void SetErrorMessage(Exception exception)
        {
            message=exception.Message;
            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string?>()
                {
                    ["Exception"]=exception.Message,

                    ["StackTrace"]=exception.StackTrace
                    
                };
                if (exception.InnerException!=null)
                {
                    dic.Add("InnerException.Exception", exception.InnerException.Message);
                    dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                }
                message=JsonConvert.SerializeObject(dic);
            }

        }
        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException("The response has" +" already started, the http status code middleware will not be executed.");
            }

            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new MetaData()
                {
                    AppStatusCode = apiStatusCode,
                    Message = message,
                }
            };
            var json = JsonConvert.SerializeObject(result);
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

    }

}