using Social.Net.Api.Models.Directory;
using Social.Net.Core.Domains.Directory;

namespace Social.Net.Api;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<Customer, CustomerModel>();
        // CreateMap<PasswordModel, Password>();

        CreateMap<Address, AddressModel>()
            .ReverseMap();
    }
}