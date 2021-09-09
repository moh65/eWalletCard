using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Mofid.eWallet.Entities.Configurations;
using Mofid.eWallet.Infra.Security;
using Mofid.eWallet.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;

        public JWTMiddleware(RequestDelegate next, IUserService userService, IJWTService jwtService)
        {
            _next = next;
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await _SetUserInContext(context, token);

            await _next(context);
        }

        private async Task _SetUserInContext(HttpContext context, string token)
        {
            try
            {
                var userid = _jwtService.ValidateTokenAndGetClaim(token, "UserId");
                context.Items["user"] = await _userService.GetByIdAsync(userid);
            }
            catch(Exception)
            {                
            }
        }
    }

    public static class JWTMiddlewareExtension
    {
        public static IApplicationBuilder UseJWT(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JWTMiddleware>();
        }
    }
}