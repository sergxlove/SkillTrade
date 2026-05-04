using Microsoft.AspNetCore.Mvc;
using SkillTrade.Core.Infrastructures;
using SkillTrade.Core.Models;
using SkillTrade.LoginAPI.Abstractions;
using SkillTrade.LoginAPI.Requests;
using SkillTrade.LoginAPI.Responce;
using SkillTrade.LoginAPI.Services;

namespace SkillTrade.LoginAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/login/user/balance", async (HttpContext context,
                [FromBody] IdRequest request,
                [FromServices] IUsersService usersService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    decimal result = await usersService.GetBalanceAsync(request.Id, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/login/user/me", async (HttpContext context,
                [FromBody] IdRequest request,
                [FromServices] IUsersService usersService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    Users? user = await usersService.GetByIdAsync(request.Id, token);
                    if (user is null)
                        return Results.BadRequest();
                    ProfileResponce profile = new ProfileResponce
                    {
                        Login = user.Login,
                        Name = user.Name,
                        Role = user.Role,
                        Balance = user.Balance,
                        CreatedAt = user.CreatedAt
                    };
                    return Results.Ok(profile);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/login/password/change", async (HttpContext context,
                [FromBody] PasswordRequest request,
                [FromServices] IUsersService usersService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    Users? user = await usersService.GetByIdAsync(request.Id, token);
                    if (user is null)
                        return Results.BadRequest();
                    ResultModel<Users> newUser = Users.Create(user.Id, user.Login, user.Name,
                        request.NewPassword, user.Role, user.Balance, user.CreatedAt);
                    if (newUser.Error != string.Empty)
                        return Results.BadRequest(newUser.Error);
                    int result = await usersService.UpdatePasswordAsync(request.Id, 
                        newUser.Value.HashPassword, token);
                    if(result == 0)
                        return Results.BadRequest();
                    return Results.Ok();
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/login/user/stats", async (HttpContext context,
                [FromBody] IdRequest request,
                [FromServices] IUserCoursesService userCoursesService,
                CancellationToken token) =>
            {
                try
                {
                    if(request is null) 
                        return Results.BadRequest();
                    StatsResponce stats = new()
                    {
                        CountStarted = await userCoursesService.CountStartedCoursesAsync(request.Id, token),
                        CountEnded = await userCoursesService.CountEndedCoursesAsync(request.Id, token)
                    };
                    return Results.Ok(stats);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            return app;
        }
    }
}
