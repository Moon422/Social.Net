using Social.Net.Core;
using Social.Net.Core.Domains.Directory;

namespace Social.Net.Services.Directory;

public interface ICountryService
{
    Task<Country?> GetCountryByIdAsync(int countryId);
    
    Task<IList<Country>> GetAllCountriesAsync();

    Task<PagedList<Country>> SearchCountriesAsync(int pageIndex = 0, int pageSize = int.MaxValue);
    
    Task InsertCountryAsync(Country country, bool deferCacheClear = false, bool deferInsert = false);

    Task UpdateCountryAsync(Country country, bool deferCacheClear = false, bool deferUpdate = false);
    
    Task DeleteCountryAsync(Country country, bool deferCacheClear = false, bool deferDelete = false); 
}