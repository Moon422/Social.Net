using Social.Net.Core.Domains.Directory;

namespace Social.Net.Services.Directory;

public interface IAddressService
{
    Task<Address?> GetAddressByIdAsync(int addressId);

    Task InserAddressAsync(Address address, bool deferCacheClear = false, bool deferInsert = false);
    
    Task UpdateAddressAsync(Address address, bool deferCacheClear = false, bool deferUpdate = false);
    
    Task DeleteAddressAsync(Address address, bool deferCacheClear = false, bool deferDelete = false);
}