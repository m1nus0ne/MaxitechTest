using WebApi.Services;

namespace WebApi.Midleware;

public class RequestThrottlingMiddleware(
    RequestDelegate next,
    IRequestThrottlingService throttlingService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (!throttlingService.TryAcquire())
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Сервис временно недоступен из-за большой нагрузки");
            return;
        }

        try
        {
            await next(context);
        }
        finally
        {
            throttlingService.Release();
        }
    }
}

