using System.Collections.Concurrent;
using RateLimiter.Web.Store;

namespace RateLimiter.Web.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimiteRuleStore _ruleStore;
    private readonly ILogger<RateLimitMiddleware> _logger;

    public RateLimitMiddleware(RequestDelegate next, IRateLimiteRuleStore ruleStore, ILogger<RateLimitMiddleware> logger)
    {
        _next = next;
        _ruleStore = ruleStore;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        
        if (_ruleStore.CheckRateLimite(ip))
        {
            _ruleStore.EnqueueCounter(ip, DateTime.Now);
            await _ruleStore.GetRule(ip).SemaphoreLimit.WaitAsync();
            await Task.Delay(50000);
            _ruleStore.DequeueCounter(ip);
            _ruleStore.GetRule(ip).SemaphoreLimit.Release();
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 429;
            await context.Response.WriteAsync("Error: request limit exceeded");
        }
    }
}