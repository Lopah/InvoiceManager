using InvoiceManager.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace InvoiceManager.API.Middleware
{
    public class SecretKeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SecretKeyValidatorMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Request.Headers["key"];

            if (!string.IsNullOrWhiteSpace(key))
            {
                if (_configuration.GetSection("secretKey").Value != key)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    throw new InvalidSecretKeyException(key);
                }
            }
            else
            {
                throw new InvalidSecretKeyException( );
            }

            await _next(context);
        }
    }
}
