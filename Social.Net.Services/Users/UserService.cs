using Microsoft.EntityFrameworkCore;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Users;
using Social.Net.Data;
using Social.Net.Services.Caching;
using Social.Net.Services.Users.Caching;

namespace Social.Net.Services.Users;

[ScopedDependency(typeof(IUserService))]
public class UserService(IRepository<Profile> profileRepository, 
    IRepository<Password> passwordRepository,
    ICacheService cacheService) : IUserService
{
    public async Task<Password?> GetPasswordByProfileIdAsync(int profileId)
    {
        if (profileId <= 0)
            return null;

        var cacheKey = cacheService.PrepareCacheKey(UserCacheKeyConstants.GetPasswordByProfileIdCacheKey, profileId);

        return await cacheService.GetAsync(cacheKey, async () =>
        {
            var password = await passwordRepository.QueryBuilder
                .FirstOrDefaultAsync(password => password.ProfileId == profileId);

            return password;
        });
    }

    public async Task InsertPasswordAsync(Password password, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(password);

        await passwordRepository.InsertAsync(password, deferInsert);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }

    public async Task UpdatePasswordAsync(Password password, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(password);

        await passwordRepository.UpdateAsync(password, deferUpdate);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }

    public async Task DeletePasswordAsync(Password password, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(password);

        await passwordRepository.DeleteAsync(password, deferDelete);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.PasswordPrefix);
        }
    }

    public async Task<Profile?> GetProfileByIdAsync(int profileId)
    {
        if (profileId <= 0)
        {
            return null;
        }
        
        var cacheKey = cacheService.PrepareCacheKey(UserCacheKeyConstants.GetProfileByIdCachekey, profileId);
        return await cacheService.GetAsync(cacheKey, async () =>
        {
            var profile = await profileRepository.GetByIdAsync(profileId);
            return profile;
        });
    }

    public async Task<Profile?> GetProfileByEmailAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        
        var cacheKey = cacheService.PrepareCacheKey(UserCacheKeyConstants.GetProfileByEmailCachekey, email);
        return await cacheService.GetAsync(cacheKey, async () =>
        {
            var profile = await profileRepository.QueryBuilder
                .FirstOrDefaultAsync(profile => profile.Email.ToLower() == email.ToLower());
            return profile;
        });
    }

    public async Task InsertProfileAsync(Profile profile, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(profile);

        await profileRepository.InsertAsync(profile, deferInsert);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.ProfilePrefix);
        }
    }

    public async Task UpdateProfileAsync(Profile profile, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(profile);

        await profileRepository.UpdateAsync(profile, deferUpdate);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.ProfilePrefix);
        }
    }

    public async Task DeleteProfileAsync(Profile profile, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(profile);

        await profileRepository.DeleteAsync(profile, deferDelete);

        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(UserCacheKeyConstants.ProfilePrefix);
        }
    }
}