using Social.Net.Api.Models.Directory;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Directory;
using Social.Net.Services.Directory;

namespace Social.Net.Api.Factories.Directory;

public interface IAddressModelFactory
{
    Task<AddressModel> PrepareAddressModelAsync(AddressModel model, Address address);
}

[ScopedDependency(typeof(IAddressModelFactory))]
public class AddressModelFactory(IStateProvinceService stateProvinceService,
    ICountryService countryService) : IAddressModelFactory
{
    public async Task<AddressModel> PrepareAddressModelAsync(AddressModel model, Address address)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(address);

        var stateProvince = await stateProvinceService.GetStateProvinceByIdAsync(address.StateProvinceId);
        model.StateProvince = stateProvince?.Name;
        model.Country = (await countryService.GetCountryByIdAsync(stateProvince?.CountryId ?? 0))?.Name;
        
        return model;
    }
}