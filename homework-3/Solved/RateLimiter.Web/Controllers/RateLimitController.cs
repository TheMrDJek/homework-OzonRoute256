using Microsoft.AspNetCore.Mvc;
using RateLimiter.Web.Models;
using RateLimiter.Web.Store;

namespace RateLimiter.Web.Controllers;

[Route("rateLimit")]
public class RateLimitController : ControllerBase
{
    private readonly IRateLimiteRuleStore _ruleStore;

    public RateLimitController(IRateLimiteRuleStore ruleStore)
    {
        _ruleStore = ruleStore;
    }
    
    [HttpGet("testRate")] //url/rateLimit/testRate
    public async Task<ActionResult<string>> TestRateLimite()
    {
        return Ok("add Task");
    }

    [HttpPost("addRule")]
    public async Task<ActionResult<RateLimiteRule>> AddRule([FromQuery] string ip, int limit, TimeSpan period)
    {
        var rule = new RateLimiteRule
        {
            PeriodTimespan = period,
            SemaphoreLimit = new SemaphoreSlim(limit)
        };
        _ruleStore.AddRule(ip, rule);

        return Ok(rule);
    }
}