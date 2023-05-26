using RateLimiter.Web.Middleware;
using RateLimiter.Web.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IRateLimiteRuleStore, RateLimiteRuleStore>();

var app = builder.Build();


app.UseRouting();
app.MapControllers();
app.UseMiddleware<RateLimitMiddleware>();

app.Run();
