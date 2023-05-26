using System.Collections.Concurrent;
using RateLimiter.Web.Models;

namespace RateLimiter.Web.Store;

class RateLimiteRuleStore : IRateLimiteRuleStore
{
    private readonly ConcurrentDictionary<string, RateLimiteRule> _rules = new();
    private readonly ConcurrentDictionary<string, Queue<DateTime>> _rateLimiteCounters = new();

    public RateLimiteRuleStore()
    {
        var rule = new RateLimiteRule {SemaphoreLimit = new SemaphoreSlim(10), PeriodTimespan = TimeSpan.FromMinutes(1)};
        var queue = new Queue<DateTime>();
        _rules.AddOrUpdate("0.0.0.1", _ => rule, (_, _) => rule);
        _rateLimiteCounters.AddOrUpdate("0.0.0.1", _ => queue, (_, _) => queue);
    }
    
    public void AddRule(string ip, RateLimiteRule rule)
    {
        var queue = new Queue<DateTime>();
        _rules.AddOrUpdate(ip, _ => rule, (_, _) => rule);
        _rateLimiteCounters.AddOrUpdate(ip, _ => queue, (_, _) => queue);
    }

    public void RemoveRule(string ip)
    {
        _rules.TryRemove(ip, out _);
        _rules.TryRemove(ip, out _);
    }

    public RateLimiteRule GetRule(string ip)
    {
         return _rules.ContainsKey(ip) ? _rules[ip] : _rules["0.0.0.1"];
    }

    public void EnqueueCounter(string ip, DateTime time)
    {
        _rateLimiteCounters[ip].Enqueue(time);
    }

    public void DequeueCounter(string ip)
    {
        _rateLimiteCounters[ip].Dequeue();
    }

    public bool CheckRateLimite(string ip)
    {
        if (!_rules.ContainsKey(ip))
        {
            ip = "0.0.0.1";
        }

        if (_rateLimiteCounters[ip].Count > 0)
        {
            return _rules[ip].SemaphoreLimit.CurrentCount > 0 
                   && _rateLimiteCounters[ip].Peek() - _rateLimiteCounters[ip].Last() < _rules[ip].PeriodTimespan;
        }
        else
        {
            return true;
        }
    }
}