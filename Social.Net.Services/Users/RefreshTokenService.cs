using Microsoft.EntityFrameworkCore;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Users;
using Social.Net.Data;
using Social.Net.Services.Caching;
using Social.Net.Services.Users.Caching;

namespace Social.Net.Services.Users;

[ScopedDependency(typeof(IRefreshTokenService))]
public class RefreshTokenService(IRepository<RefreshToken> refreshTokenRepository,
    ICacheService cacheService) : IRefreshTokenService
{
    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        var cacheKey = cacheService.PrepareCacheKey(UserCacheKeyConstants.GetRefreshTokenByTokenCacheKey, token);
        return await cacheService.GetAsync(cacheKey, async () =>
        {
            return await refreshTokenRepository.QueryBuilder
                .FirstOrDefaultAsync(rt => rt.Token == token);
        });
    }

    public async Task<IList<RefreshToken>> GetRefreshTokensByProfileIdAsync(int profileId)
    {
        if (profileId <= 0)
        {
            throw new ArgumentException(null, nameof(profileId));
        }
        
        var cacheKey = cacheService.PrepareCacheKey(UserCacheKeyConstants.GetRefreshTokensByProfileIdCacheKey, profileId);
        return await cacheService.GetAsync<RefreshToken>(cacheKey, async () =>
        {
            return await refreshTokenRepository.QueryBuilder
                .Where(rt => rt.ProfileId == profileId)
                .ToListAsync();
        });
    }

    public async Task InsertRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        await refreshTokenRepository.InsertAsync(refreshToken, deferInsert);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        await refreshTokenRepository.UpdateAsync(refreshToken, deferUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }

    public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        await refreshTokenRepository.DeleteAsync(refreshToken, deferDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }
}