using Microsoft.AspNetCore.Mvc;
using SkillTrade.Core.Abstractions;
using SkillTrade.Core.Models;
using SkillTrade.Core.Requests;
using SkillTrade.LoginAPI.Abstractions;
using SkillTrade.LoginAPI.Requests;
using System.Security.Claims;

namespace SkillTrade.LoginAPI.Endpoints
{
    public static class LoginEndpoints
    {
        public static IEndpointRouteBuilder MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/login/login", async (HttpContext context, 
                LoginRequest request,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
                [FromServices] IConfiguration configuration,
                CancellationToken token) =>
            {
                try
                {
                    if (request.Login == string.Empty || request.Password == string.Empty)
                        return Results.BadRequest("Пустые значения логин или пароль");
                    if (!await userService.VerifyAsync(request.Login, request.Password, token))
                        return Results.BadRequest("Неверный логин или пароль");
                    string userRole = await userService.GetRoleAsync(request.Login, token);
                    Guid userId = await userService.GetIdAsync(request.Login, token);
                    IConfigurationSection? jwtSettings = configuration.GetSection("JwtSettings");
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, userRole),
                        new Claim(ClaimTypes.Email, request.Login),
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    };
                    var jwttoken = jwtService.GenerateToken(new JwtRequest()
                    {
                        Audience = jwtSettings["Audience"]!,
                        Issuer = jwtSettings["Issuer"]!,
                        Claims = claims,
                        SecretKey = jwtSettings["SecretKey"]!,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Lifetime"]!))
                    });
                    context.Response.Cookies.Append("jwt", jwttoken!);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireRateLimiting("LoginPolicy");

            app.MapPost("/api/login/reg", async (HttpContext context,
                RegistrRequest request,
                [FromServices] IUsersService userService,
                [FromServices] IJwtProviderService jwtService,
                [FromServices] IPasswordHasherService passwordHasherService,
                [FromServices] IConfiguration configuration,
                CancellationToken token) =>
            {
                try
                {
                    var user = Users.Create(Guid.NewGuid(), request.Login, request.Name, request.Password,
                        request.Role, 0, DateTime.UtcNow);
                    if (!user.IsSuccess) return Results.BadRequest(user.Error);
                    if(await userService.ExistsByLoginAsync(user.Value.Login, token))
                    {
                        return Results.BadRequest("Данный пользователь уже есть");
                    }
                    var result = await userService.CreateAsync(user.Value, token);
                    IConfigurationSection? jwtSettings = configuration.GetSection("JwtSettings");
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, request.Role),
                        new Claim(ClaimTypes.Email, request.Login),
                        new Claim(ClaimTypes.NameIdentifier, user.Value.Id.ToString())
                    };
                    var jwttoken = jwtService.GenerateToken(new JwtRequest()
                    {
                        Audience = jwtSettings["Audience"]!,
                        Issuer = jwtSettings["Issuer"]!,
                        Claims = claims,
                        SecretKey = jwtSettings["SecretKey"]!,
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Lifetime"]!))
                    });
                    context.Response.Cookies.Append("jwt", jwttoken!);
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireRateLimiting("GeneralPolicy");

            app.MapGet("/api/login/logout", (HttpContext context) =>
            {
                try
                {
                    context.Response.Cookies.Delete("jwt");
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            }).RequireAuthorization("OnlyForAuthUser")
            .RequireRateLimiting("GeneralPolicy");

            return app;
        }
    }
}
