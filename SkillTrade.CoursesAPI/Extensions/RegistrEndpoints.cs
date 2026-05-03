using SkillTrade.CoursesAPI.Endpoints;

namespace SkillTrade.CoursesAPI.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCoursesEndpoints(); 

            return app;
        }
    }
}
