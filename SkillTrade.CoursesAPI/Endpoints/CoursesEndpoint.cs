using Microsoft.AspNetCore.Mvc;
using SkillTrade.Core.Models;
using SkillTrade.CoursesAPI.Abstractions;
using SkillTrade.CoursesAPI.Requests;

namespace SkillTrade.CoursesAPI.Endpoints
{
    public static class CoursesEndpoint
    {
        public static IEndpointRouteBuilder MapCoursesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/courses/all", async (HttpContext context, 
                [FromServices] ICoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    List<Courses> result = (List<Courses>)await courseService.GetAllAsync(token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/search/name", async (HttpContext context,
                [FromServices] SearchNameRequest request,
                [FromServices] ICoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    List<Courses> result = (List<Courses>)await courseService
                        .SearchByTitleAsync(request.Name, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/search/id", async (HttpContext context,
                [FromServices] SearchIdRequest request,
                [FromServices] ICoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    Courses? course = await courseService.GetByIdAsync(request.Id, token);
                    if (course is null) return Results.BadRequest();
                    return Results.Ok(course);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/search/my", async (HttpContext context,
                [FromServices] SearchIdRequest request,
                [FromServices] IUserCoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    List<Guid> result = (List<Guid>)await courseService.GetByUserIdAsync(request.Id, 
                        token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/progres", async (HttpContext context,
                [FromServices] SearchIdRequest request,
                [FromServices] IUserCoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    int result = await courseService.GetProgressAsync(request.Id, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/subscribe", () =>
            {

            });

            return app;
        }
    }
}
