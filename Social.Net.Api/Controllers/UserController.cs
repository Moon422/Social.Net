using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Social.Net.Api.Models.Users;
using Social.Net.Core.Domains.Directory;
using Social.Net.Core.Domains.Users;
using Social.Net.Data;
using Social.Net.Services.Directory;
using Social.Net.Services.Users;
using Profile = Social.Net.Core.Domains.Users.Profile;

namespace Social.Net.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, 
    IAddressService addressService, 
    IStateProvinceService stateProvinceService, 
    ITransactionManager transactionManager,
    IMapper mapper,
    IConfiguration configuration) : ControllerBase
{
    private string CreateToken(Profile profile)
    {
        List<Claim> claims = new List<Claim>
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
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel request)
    {
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateProfileModel model)
    {
        var stateProvince = await stateProvinceService
            .GetStateProvinceByIdAsync(model.PresentAddress.StateProvinceId);
        if (stateProvince is null)
        {
            ModelState.AddModelError("Model.PresentAddress.StateProvinceId",
                $"StateProvince {model.PresentAddress.StateProvinceId} does not exist");

            return BadRequest(ModelState);
        }

        await transactionManager.RunTransactionAsync(async () =>
        {
            var presentAddress = mapper.Map<Address>(model.PresentAddress);
            await addressService.InserAddressAsync(presentAddress, deferInsert: true);

            var profile = mapper.Map<Profile>(model);
            profile.PresentAddressId = presentAddress.Id;
            await userService.InsertProfileAsync(profile, deferInsert: true);

            var password = new Password()
            {
                Hash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                ProfileId = profile.Id
            };
            await userService.InsertPasswordAsync(password, deferInsert: true);
        });

        return Ok();
    }
}