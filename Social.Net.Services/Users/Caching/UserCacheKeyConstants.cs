using Social.Net.Core.Caching;

namespace Social.Net.Services.Users.Caching;

public static class UserCacheKeyConstants
{
    public const string ProfilePrefix = "Social.Net.Users.Profile.";
    public const string PasswordPrefix = "Social.Net.Users.Password.";
    public const string RefreshTokenPrefix = "Social.Net.Users.RefreshToken.";

    public static CacheKey GetProfileByIdCachekey => new(
        "Social.Net.Users.Profile.GetProfileById.{0}", ProfilePrefix);
    
    public static CacheKey GetProfileByEmailCachekey => new(
        "Social.Net.Users.Profile.GetProfileByEmail.{0}", ProfilePrefix);
    
    public static CacheKey GetProfileByUsernameCachekey => new(
        "Social.Net.Users.Profile.GetProfileByUsername.{0}", ProfilePrefix);

    public static CacheKey GetPasswordByProfileIdCacheKey => new(
        "Social.Net.Users.Password.GetPasswordByProfileId.{0}", PasswordPrefix);
    
    public static CacheKey GetRefreshTokenByTokenCacheKey => new(
        "Social.Net.Users.RefreshToken.GetRefreshTokenByToken.{0}", RefreshTokenPrefix);
    
    public static CacheKey GetRefreshTokensByProfileIdCacheKey => new(
        "Social.Net.Users.RefreshToken.GetRefreshTokensByProfileId.{0}", RefreshTokenPrefix);
}