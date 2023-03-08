namespace taller.devops.api.Middleware
{
    public static class ApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder, string apiKey)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>(apiKey);
        }
    }
}
