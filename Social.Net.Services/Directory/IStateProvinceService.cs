using Social.Net.Core;
using Social.Net.Core.Domains.Directory;

namespace Social.Net.Services.Directory;

public interface IStateProvinceService
{
    Task<StateProvince?> GetStateProvinceByIdAsync(int stateProvinceId);
    
    Task<IList<StateProvince>> GetAllStateProvincesAsync();
    
    Task<IList<StateProvince>> GetStateProvincesByCountryIdAsync(int countryId);

    Task<PagedList<StateProvince>> SearchStateProvincesAsync(int countryId = 0, int pageIndex = 0, int pageSize = int.MaxValue);
    
    Task InsertStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferInsert = false);

    Task UpdateStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferUpdate = false);
    
    Task DeleteStateProvinceAsync(StateProvince stateProvince, bool deferCacheClear = false, bool deferDelete = false); 
}