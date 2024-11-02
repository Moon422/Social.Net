using Social.Net.Core.Caching;

namespace Social.Net.Services.Users.Caching;

public static class UserCacheKeyConstants
{
    public const string ProfilePrefix = "Social.Net.Users.Profile.";
    public const string PasswordPrefix = "Social.Net.Users.Password.";

    public static CacheKey GetProfileByIdCachekey => new(
        "Social.Net.Users.Profile.GetProfileById.{0}", ProfilePrefix);
    
    public static CacheKey GetProfileByEmailCachekey => new(
        "Social.Net.Users.Profile.GetProfileByEmail.{0}", ProfilePrefix);

    public static CacheKey GetPasswordByProfileIdCacheKey => new(
        "Social.Net.Users.Password.GetPasswordByProfileId.{0}", PasswordPrefix);
}