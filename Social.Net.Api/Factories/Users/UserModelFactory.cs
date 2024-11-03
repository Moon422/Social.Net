using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Social.Net.Api.Factories.Directory;
using Social.Net.Api.Models.Directory;
using Social.Net.Api.Models.Users;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Directory;
using Social.Net.Core.Domains.Users;
using Social.Net.Services.Directory;
using Social.Net.Services.Users;
using Profile = Social.Net.Core.Domains.Users.Profile;

namespace Social.Net.Api.Factories.Users;

[ScopedDependency(typeof(IUserModelFactory))]
public class UserModelFactory(IHttpContextAccessor httpContextAccessor,
    IRefreshTokenService refreshTokenService,
    IAddressService addressService,
    IAddressModelFactory addressModelFactory,
    IConfiguration configuration,
    IMapper mapper) : IUserModelFactory
{
    private string CreateToken(Profile profile)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, profile.UserName),
            new Claim(ClaimTypes.Email, profile.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Value!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
    private async Task<RefreshToken> GenerateRefreshTokenAsync(Profile profile)
    {
        var refreshToken = new RefreshToken()
        {
            ProfileId = profile.Id
        };
        await refreshTokenService.InsertRefreshTokenAsync(refreshToken);

        return refreshToken;
    }
    
    public async Task<LoginResponseModel> PrepareLoginResponseModelAsync(LoginResponseModel model, Profile profile)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(profile);
        
        var refreshToken = await GenerateRefreshTokenAsync(profile);
        httpContextAccessor.HttpContext!.Response.Cookies.Append(
            "refresh-token",
            refreshToken.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            }
        );
        
        var token = CreateToken(profile);
        model.JwtToken = token;
        
        var profileModel = mapper.Map<ProfileModel>(profile);
        profileModel = await PrepareProfileModelAsync(profileModel, profile);
        model.Profile = profileModel;
        
        return model;
    }

    public async Task<ProfileModel> PrepareProfileModelAsync(ProfileModel model, Profile profile)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(profile);

        if ((await addressService.GetAddressByIdAsync(profile.PresentAddressId)) is { } presentAddress)
        {
            model.PresentAddress = mapper.Map<AddressModel>(presentAddress);
            model.PresentAddress = await addressModelFactory.PrepareAddressModelAsync(model.PresentAddress, presentAddress);
        }

        if ((await addressService.GetAddressByIdAsync(profile.PermanentAddressId ?? 0)) is { } permanentAddress)
        {
            model.PermanentAddress = mapper.Map<AddressModel>(permanentAddress);
            model.PermanentAddress = await addressModelFactory.PrepareAddressModelAsync(model.PermanentAddress, permanentAddress);
        }

        return model;
    }
}