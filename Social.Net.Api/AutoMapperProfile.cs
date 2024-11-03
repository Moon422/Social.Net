using Social.Net.Api.Models.Directory;
using Social.Net.Api.Models.Users;
using Social.Net.Core.Domains.Directory;
using Social.Net.Core.Domains.Users;

namespace Social.Net.Api;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Address, AddressModel>()
            .ReverseMap();

        CreateMap<CreateProfileModel, Profile>();
    }
}