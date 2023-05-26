using System.Collections.Concurrent;
using RateLimiter.Web.Models;

namespace RateLimiter.Web.Store;

public interface IRateLimiteRuleStore
{
    void AddRule(string ip, RateLimiteRule rule);

    void RemoveRule(string ip);

    RateLimiteRule GetRule(string ip);

    void EnqueueCounter(string ip, DateTime time);

    void DequeueCounter(string ip);

    bool CheckRateLimite(string ip);
}