using SkillTrade.LoginAPI.Endpoints;

namespace SkillTrade.LoginAPI.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapLoginEndpoints();
            app.MapUserEndpoints();

            return app;
        }
    }
}
