namespace Social.Net.Core.Caching;

public class CacheKey(string key, string prefix)
{
    public string Key { get; set; } = key;
    public string Prefix { get; set; } = prefix;
}