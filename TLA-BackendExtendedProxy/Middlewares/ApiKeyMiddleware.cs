namespace TLA_BackendExtendedProxy.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ServiceCommunicationApiKey";


        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var VALID_API_KEY = context.RequestServices.GetRequiredService<IConfiguration>()["ServiceCommunicationApiKey"];

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API-nyckel saknas.");
                return;
            }


            if (!VALID_API_KEY.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Ogiltig API-nyckel.");
                return; 
            }


            await _next(context);
        }
    }
}