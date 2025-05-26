namespace WebApi.Middleware;

public class ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _configuration = configuration;
    private const string APIKEY_HEADER_NAME = "X-API-KEY";

    public async Task InvokeAsync(HttpContext context)
    {
        var defaultApiKey = _configuration["SecretKeys:Default"] ?? null;

        if (string.IsNullOrEmpty(defaultApiKey) || !context.Request.Headers.TryGetValue(APIKEY_HEADER_NAME, out var headerApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or missing API Key");
            return;
        }

        if (!string.Equals(headerApiKey, defaultApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}