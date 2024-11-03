using Social.Net.Api.Models.Users;
using Social.Net.Core.Domains.Users;

namespace Social.Net.Api.Factories.Users;

public interface IUserModelFactory
{
    Task<LoginResponseModel> PrepareLoginResponseModelAsync(LoginResponseModel model, Profile profile);

    Task<ProfileModel> PrepareProfileModelAsync(ProfileModel model, Profile profile);
}