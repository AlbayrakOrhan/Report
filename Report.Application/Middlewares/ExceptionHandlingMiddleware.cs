using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Report.Application.Abstracts;

namespace Report.Application.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ResponseBase()
        {
            Success = false,
            MessageCode = statusCode,
            Message = exception.Message,
            Errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static List<KeyValuePair<string, string[]>> GetErrors(Exception exception)
    {
        List<KeyValuePair<string, string[]>> errors = null;
        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .Select(x =>
                    new KeyValuePair<string, string[]>(x.Key, x.Select(y => y.ErrorMessage).ToArray())).ToList();
        }

        return errors;
    }
}