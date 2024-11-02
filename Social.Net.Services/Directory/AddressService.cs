using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Directory;
using Social.Net.Data;
using Social.Net.Services.Caching;
using Social.Net.Services.Directory.Caching;

namespace Social.Net.Services.Directory;

[ScopedDependency(typeof(IAddressService))]
public class AddressService(IRepository<Address> addressRepository, ICacheService cacheService) : IAddressService
{
    public async Task<Address?> GetAddressByIdAsync(int addressId)
    {
        if (addressId <= 0)
        {
            return null;
        }

        var cacheKey = cacheService.PrepareCacheKey(DirectoryCacheKeyConstants.GetAddressByIdCacheKey, addressId);
        return await cacheService.GetAsync(cacheKey, async () => await addressRepository.GetByIdAsync(addressId));
    }

    public async Task InserAddressAsync(Address address, bool deferCacheClear = false, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(address);

        await addressRepository.InsertAsync(address, deferInsert);
        
        if (deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.AddressPrefix);
        }
    }

    public async Task UpdateAddressAsync(Address address, bool deferCacheClear = false, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(address);

        await addressRepository.UpdateAsync(address, deferUpdate);
        
        if (deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.AddressPrefix);
        }
    }

    public async Task DeleteAddressAsync(Address address, bool deferCacheClear = false, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(address);

        await addressRepository.DeleteAsync(address, deferDelete);
        
        if (deferCacheClear)
        {
            cacheService.RemoveByPrefix(DirectoryCacheKeyConstants.AddressPrefix);
        }
    }
}