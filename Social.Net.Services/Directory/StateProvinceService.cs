using Microsoft.EntityFrameworkCore;
using Social.Net.Core;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Directory;
using Social.Net.Data;
using Social.Net.Services.Caching;
using Social.Net.Services.Directory.Caching;

namespace Social.Net.Services.Directory;

[ScopedDependency(typeof(IStateProvinceService))]
public class StateProvinceService(IRepository<StateProvince> stateProvinceRepository, ICacheService cacheService) : IStateProvinceService
{
    public async Task<StateProvince?> GetStateProvinceByIdAsync(int stateProvinceId)
    {
        if (stateProvinceId <= 0)
        {
            return null;
        }
        
        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetStateProvinceByIdCacheKey, 
            stateProvinceId);
        return await cacheService.GetAsync(cacheKey,
            async () => await stateProvinceRepository.GetByIdAsync(stateProvinceId));
    }

    public async Task<IList<StateProvince>> GetAllStateProvincesAsync()
    {
        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetAllStateProvincesCacheKey);
        return await cacheService.GetAsync(cacheKey, async () => await stateProvinceRepository.GetAllAsync());
    }

    public async Task<IList<StateProvince>> GetStateProvincesByCountryIdAsync(int countryId)
    {
        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetStateProvincesByCountryIdCacheKey,
            countryId);

        return await cacheService.GetAsync<StateProvince>(cacheKey, async () =>
        {
            var stateProvince = await stateProvinceRepository.QueryBuilder
                .Where(sp => sp.CountryId == countryId)
                .OrderBy(sp => sp.DisplayOrder)
                .ToListAsync();

            return stateProvince;
        });
    }

    public Task<PagedList<StateProvince>> SearchStateProvincesAsync(int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = stateProvinceRepository.QueryBuilder;

        if (countryId > 0)
        {
            query = query.Where(sp => sp.CountryId == countryId);
        }
        
        query = query.OrderBy(sp => sp.DisplayOrder);
        
        return query.ToPagedListAsync(pageIndex, pageSize);
    }

    public async Task InsertStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(stateProvince);

        await stateProvinceRepository.InsertAsync(stateProvince, deferInsert);
        
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.StateProvincePrefix);
        }
    }

    public async Task UpdateStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(stateProvince);

        await stateProvinceRepository.UpdateAsync(stateProvince, deferUpdate);
        
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.StateProvincePrefix);
        }
    }

    public async Task DeleteStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(stateProvince);

        await stateProvinceRepository.DeleteAsync(stateProvince, deferDelete);
        
        if (!deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.StateProvincePrefix);
        }
    }
}