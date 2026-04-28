namespace SkillTrade.CoursesAPI.Endpoints
{
    public static class CoursesEndpoint
    {
        public static IEndpointRouteBuilder MapCoursesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/courses/all", () =>
            {

            });

            app.MapPost("/api/courses/search/name", () =>
            {

            });

            app.MapPost("/api/courses/search/my", () =>
            {

            });

            app.MapPost("/api/courses/progres", () =>
            {

            });

            app.MapPost("/api/courses/subscribe", () =>
            {

            });


            return app;
        }
    }
}
