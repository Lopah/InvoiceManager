using InvoiceManager.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace InvoiceManager.API.Extensions
{
    public static class SecretKeyValidatorMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretKeyValidation(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecretKeyValidatorMiddleware>( );
        }
    }
}
