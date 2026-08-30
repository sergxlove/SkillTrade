var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SkillTrade_ContentAPI>("contentAPI");
builder.AddProject<Projects.SkillTrade_CoursesAPI>("coursesAPI");
builder.AddProject<Projects.SkillTrade_LoginAPI>("loginAPI");
builder.AddProject<Projects.SkillTrade_Proxy>("proxy");

builder.Build().Run();
