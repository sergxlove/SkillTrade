namespace SkillTrade.LoginAPI.Endpoints
{
    public static class AdminEndpoints
    {
        public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/admin/stats", () =>
            {

            });

            app.MapGet("/api/admin/users/all", () =>
            {

            });

            app.MapPost("/api/admin/users/add", () =>
            {

            });

            app.MapDelete("/api/admin/users/del", () =>
            {

            });

            app.MapGet("/api/admin/courses/all", () =>
            {

            });

            app.MapDelete("/api/admin/courses/del", () =>
            {

            });

            app.MapGet("/api/admin/backup", () =>
            {

            });

            return app;
        }
    }
}
