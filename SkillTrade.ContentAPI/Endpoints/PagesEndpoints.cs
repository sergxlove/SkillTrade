namespace SkillTrade.ContentAPI.Endpoints
{
    public static class PagesEndpoints
    {
        public static IEndpointRouteBuilder MapPagesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/content/login", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/LoginPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/actor", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/ActorPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/admin", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/AdminPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/lessons", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/LessonsPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/main", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/MainPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/policy", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/PrivatePolicyPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/profile", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/ProfilePage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/reg", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/RegPage.html");
                }
                catch { }
            });

            app.MapGet("/api/content/agreement", async (HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("wwwroot/Pages/UserAgreementPage.html");
                }
                catch { }
            });

            app.MapGet("/error/{statusCode:int}", async (int statusCode, HttpContext context) =>
            {
                try
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    switch (statusCode)
                    {
                        case 401:
                            await context.Response.SendFileAsync("wwwroot/Pages/Errors/Error401Page.html");
                            break;
                        case 403:
                            await context.Response.SendFileAsync("wwwroot/Pages/Errors/Error403Page.html");
                            break;
                        case 404:
                            await context.Response.SendFileAsync("wwwroot/Pages/Errors/Error404Page.html");
                            break;
                    }
                }
                catch { }
            });

            return app;
        }
    }
}
