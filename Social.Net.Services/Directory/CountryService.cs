using Social.Net.Core;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Directory;
using Social.Net.Data;
using Social.Net.Services.Caching;
using Social.Net.Services.Directory.Caching;

namespace Social.Net.Services.Directory;

[ScopedDependency(typeof(ICountryService))]
public class CountryService(IRepository<Country> countryRepository, ICacheService cacheService) : ICountryService
{
    public async Task<Country?> GetCountryByIdAsync(int countryId)
    {
        if (countryId <= 0)
        {
            return null;
        }
        
        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetCountryByIdCachekey, countryId);
        return await cacheService.GetAsync(cacheKey, async () =>
        {
            var country = await countryRepository.GetByIdAsync(countryId);
            return country;
        });
    }

    public async Task<IList<Country>> GetAllCountriesAsync()
    {
        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetAllCountriesCachekey);
        return await cacheService.GetAsync(cacheKey, async () => await countryRepository.GetAllAsync());
    }

    public async Task<PagedList<Country>> SearchCountriesAsync(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var countries = await countryRepository.QueryBuilder
            .OrderBy(c => c.DisplayOrder)
            .ToPagedListAsync(pageIndex, pageSize);

        return countries;
    }

    public async Task InsertCountryAsync(Country country, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(country);

        await countryRepository.InsertAsync(country, deferInsert);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.CountryPrefix);
        }
    }

    public async Task UpdateCountryAsync(Country country, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(country);

        await countryRepository.UpdateAsync(country, deferUpdate);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.CountryPrefix);
        }
    }

    public async Task DeleteCountryAsync(Country country, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(country);

        await countryRepository.DeleteAsync(country, deferDelete);
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.CountryPrefix);
        }
    }
}