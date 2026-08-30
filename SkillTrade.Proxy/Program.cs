using SkillTrade.Aspire.ServiceDefault;

namespace SkillTrade.Proxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
            builder.AddServiceDefaults();
            var app = builder.Build();
            app.MapDefaultEndpoints();

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/api/content/login");
            });

            app.MapReverseProxy();
            app.Run();
        }
    }
}
