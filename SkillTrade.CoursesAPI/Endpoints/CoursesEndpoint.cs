using Microsoft.AspNetCore.Mvc;
using SkillTrade.Core.Infrastructures;
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
                    IEnumerable<Courses> result = await courseService.GetAllAsync(token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/search/name", async (HttpContext context,
                [FromBody] SearchNameRequest request,
                [FromServices] ICoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    IEnumerable<Courses> result = await courseService
                        .SearchByTitleAsync(request.Name, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/search/id", async (HttpContext context,
                [FromBody] SearchIdRequest request,
                [FromServices] ICoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
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
                [FromBody] SearchIdRequest request,
                [FromServices] IUserCoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    IEnumerable<UserCourses> result = await courseService.GetByUserIdAsync(request.Id, 
                        token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/progres", async (HttpContext context,
                [FromBody] SearchIdRequest request,
                [FromServices] IUserCoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    int result = await courseService.GetProgressAsync(request.Id, token);
                    return Results.Ok(result);
                }
                catch
                {
                    return Results.InternalServerError();
                }
            });

            app.MapPost("/api/courses/subscribe", async (HttpContext context,
                [FromBody] SubscribeRequest request,
                [FromServices] IUserCoursesService courseService,
                CancellationToken token) =>
            {
                try
                {
                    if (request is null)
                        return Results.BadRequest();
                    ResultModel<UserCourses> userCourse = UserCourses.Create(Guid.NewGuid(),
                        request.UserId, request.CourseId, 0, request.TotalProgress, DateTime.UtcNow);
                    if (userCourse.Error != string.Empty)
                        return Results.BadRequest(userCourse.Error);
                    await courseService.CreateAsync(userCourse.Value, token);
                    return Results.Ok();
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
