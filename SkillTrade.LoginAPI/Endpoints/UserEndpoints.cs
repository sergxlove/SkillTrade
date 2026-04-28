namespace SkillTrade.LoginAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/login/user/balance", () =>
            {

            });


            return app;
        }
    }
}
