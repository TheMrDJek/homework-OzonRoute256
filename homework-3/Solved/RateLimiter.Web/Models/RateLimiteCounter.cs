namespace RateLimiter.Web.Models;

public class RateLimiteCounter
{
    public DateTime Timestamp { get; set; }

    public double Count { get; set; }
}