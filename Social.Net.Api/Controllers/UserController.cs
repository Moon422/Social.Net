using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Social.Net.Api.Factories.Users;
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
    IUserModelFactory userModelFactory,
    ITransactionManager transactionManager,
    IMapper mapper,
    IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var profile = await userService.GetProfileByEmailAsync(model.Email);
        if (profile is null)
        {
            ModelState.AddModelError("message", "Invalid login credentials");
            return BadRequest(ModelState);
        }

        var password = await userService.GetPasswordByProfileIdAsync(profile.Id);
        if (password is null || !BCrypt.Net.BCrypt.Verify(model.Password, password.Hash))
        {
            ModelState.AddModelError("message", "Invalid login credentials");
            return BadRequest(ModelState);
        }

        var loginResponse = await userModelFactory.PrepareLoginResponseModelAsync(new LoginResponseModel(), profile);
        return Ok(loginResponse);
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

        if ((await userService.GetProfileByEmailAsync(model.Email)) is not null)
        {
            ModelState.AddModelError("message", "User with email already exists.");
            return BadRequest(ModelState);
        }
        
        if ((await userService.GetProfileByUsernameAsync(model.UserName)) is not null)
        {
            ModelState.AddModelError("message", "User with username already exists.");
            return BadRequest(ModelState);
        }

        try
        {
            var (profile, presentAddress) = await transactionManager.RunTransactionAsync(async () =>
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

                return (profile, presentAddress);
            });

            var loginResponse = await userModelFactory.PrepareLoginResponseModelAsync(new LoginResponseModel(), profile);
            return Ok(loginResponse);
        }
        catch
        {
            return BadRequest("Failed to create account! Please try again.");
        }
    }
}