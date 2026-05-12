namespace SkillTrade.CoursesAPI.Endpoints
{
    public static class LessonsEndpoints
    {
        public static IEndpointRouteBuilder MapLessonsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/courses/lessons/id", () =>
            {

            });

            app.MapDelete("/api/courses/lessons/id", () =>
            {

            });

            app.MapPost("/api/courses/lessons/complete/id", () =>
            {

            });

            app.MapPost("/api/courses/lessons/update/id", () => 
            {

            });

            return app;
        }
    }
}
