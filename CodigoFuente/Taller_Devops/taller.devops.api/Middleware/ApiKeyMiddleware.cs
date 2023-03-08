namespace taller.devops.api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, string apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestApiKey = context.Request.Headers["X-Parse-REST-API-Key"].FirstOrDefault();
            if (requestApiKey != null && requestApiKey == _apiKey)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key.");
            }
        }


    }



}
