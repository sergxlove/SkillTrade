using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace SkillTrade.LoginAPI.Endpoints
{
    public static class LoginEndpoints
    {
        public static IEndpointRouteBuilder MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/login/login", async (HttpContext context, 
                LoginRequest request,
                CancellationToken token) =>
            {
                
            });

            app.MapPost("/api/login/reg", () =>
            {

            });

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
