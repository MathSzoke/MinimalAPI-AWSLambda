var builder = WebApplication.CreateBuilder(args);

builder.BuilderGeneral();

var app = builder.Build();

app.AppGeneral();
app.AppEnvironmentConfig();

app.Run();
