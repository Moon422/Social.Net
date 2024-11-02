using Social.Net.Core.Caching;

namespace Social.Net.Services.Directory.Caching;

public static class DirectoryCacheKeyConstants
{
    public const string CountryPrefix = "Social.Net.Directory.Country.";
    public const string StateProvincePrefix = "Social.Net.Directory.StateProvince.";
    public const string AddressPrefix = "Social.Net.Directory.Address.";

    public static CacheKey GetCountryByIdCachekey => new("Social.Net.Directory.Country.GetCountryById.{0}", 
        CountryPrefix);
    
    public static CacheKey GetAllCountriesCachekey => new("Social.Net.Directory.Country.GetAllCountries", 
        CountryPrefix);

    public static CacheKey GetStateProvinceByIdCacheKey => 
        new("Social.Net.Directory.StateProvince.GetStateProvinceById.{0}", StateProvincePrefix);

    public static CacheKey GetStateProvincesByCountryIdCacheKey =>
        new("Social.Net.Directory.StateProvince.GetStateProvincesByCountryId.{0}", StateProvincePrefix);
    
    public static CacheKey GetAllStateProvincesCacheKey => 
        new("Social.Net.Directory.StateProvince.GetAllStateProvinces", StateProvincePrefix);

    public static CacheKey GetAddressByIdCacheKey => new("Social.Net.Directory.Address.{0}", AddressPrefix);
}