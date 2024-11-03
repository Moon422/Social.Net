using Microsoft.AspNetCore.Mvc;
using Social.Net.Api.Models.Users;
using Social.Net.Core.Domains.Directory;
using Social.Net.Core.Domains.Users;
using Social.Net.Data;
using Social.Net.Services.Directory;
using Social.Net.Services.Users;

namespace Social.Net.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, 
    IAddressService addressService, 
    IStateProvinceService stateProvinceService, 
    ITransactionManager transactionManager) : ControllerBase
{
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
            var presentAddress = new Address()
            {
                AddressLine1 = model.PresentAddress.AddressLine1,
                AddressLine2 = model.PresentAddress.AddressLine2,
                City = model.PresentAddress.City,
                StateProvinceId = stateProvince.Id
            };
            await addressService.InserAddressAsync(presentAddress, deferInsert: true);

            var profile = new Profile()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PresentAddressId = presentAddress.Id
            };
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