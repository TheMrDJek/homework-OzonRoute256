namespace RateLimiter.Web.Models;

public class RateLimiteRule
{
    public TimeSpan PeriodTimespan { get; set; }
    
    public SemaphoreSlim SemaphoreLimit { get; set; }
}