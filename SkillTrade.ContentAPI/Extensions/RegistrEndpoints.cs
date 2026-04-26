using SkillTrade.ContentAPI.Endpoints;

namespace SkillTrade.ContentAPI.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPagesEndpoints();

            return app;
        }
    }
}
