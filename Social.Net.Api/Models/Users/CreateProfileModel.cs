using System.ComponentModel.DataAnnotations;
using Social.Net.Api.Models.Common;
using Social.Net.Api.Models.Directory;
using Social.Net.Api.Validators.Users;

namespace Social.Net.Api.Models.Users;

public record CreateProfileModel : BaseModel
{
    [MaxLength(128)]
    [MinLength(1)]
    [Required]
    public string FirstName { get; set; } 

    [MaxLength(128)]
    [MinLength(1)]
    [Required]
    public string LastName { get; set; } 
    
    [EmailAddress]
    public string Email { get; set; } 
    
    [MaxLength(128)]
    [MinLength(1)]
    [Required]
    public string UserName { get; set; } 
    
    [Password]
    public string Password { get; set; }

    public AddressModel PresentAddress { get; set; }

    public IList<DropdownItemModel> AvailableCountries { get; set; } = new List<DropdownItemModel>();
}