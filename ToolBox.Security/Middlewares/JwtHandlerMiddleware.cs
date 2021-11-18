using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Security.Services;

namespace ToolBox.Security.Middlewares
{
    public class JwtHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, JwtService jwtService)
        {
            string token = GetToken(context.Request.Headers);
            if(!(token is null))
            {
                if(jwtService.TryGetClaims(token, out ClaimsPrincipal claims))
                {
                    context.User = claims;
                }
            }
            await _next.Invoke(context);
        }

        private string GetToken(IHeaderDictionary headers, string prefix = "Bearer")
        {
            if (headers.TryGetValue("Authorization", out StringValues values))
            {
                return values
                    .FirstOrDefault(v => v.StartsWith(prefix + " "))
                    ?.Replace(prefix + " ", string.Empty);
            }
            return null;
        }
    }
}
