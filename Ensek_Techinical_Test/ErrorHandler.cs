using Ensek_Techinical_Test.Core;
using System.Net;

public static class ErrorHandler
{
    public static void RegisterErrorHandler(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.Map("/error", ErroHandler);
    }

    private static IResult ErroHandler(HttpContext httpContext, ILogger<Program> logger)
    {
        Exception? exception = httpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        var (errorType, message, severity) = exception switch
        {
            Error error => (error.ErrorType, error.ErrorMessage, error.Severity),
            _ => (ErrorType.Unexpected, "Unexpected error occured:" + exception?.Message, ErrorSeverity.Unknown)
        };

        //generate appropriate HttpStatusCode
        var StatusCode = errorType switch
        {
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Validation => HttpStatusCode.BadRequest,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            ErrorType.Unexpected => HttpStatusCode.Forbidden,
            ErrorType.Failure => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };

        //log error
        logger.LogError("ErrorType:{ErrorType}, Messag:{ErrorMessage}, Severity:{Severity}", errorType, message, severity);

        //return response
        return Results.Problem(title: message, statusCode: (int)StatusCode);
    }
}

