using System.ComponentModel.DataAnnotations;
using Social.Net.Api.Models.Directory;

namespace Social.Net.Api.Models.Users;

public record ProfileModel : BaseModel
{
    [MaxLength(128)]
    [MinLength(1)]
    public string FirstName { get; set; }
    
    [MaxLength(128)]
    [MinLength(1)]
    public string LastName { get; set; }
    
    [MaxLength(128)]
    [MinLength(1)]
    public string UserName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public AddressModel PresentAddress { get; set; }
    
    public AddressModel? PermanentAddress { get; set; }
    
    public bool IsActive { get; set; }
    
    public bool RequireAuthentication { get; set; }
    
    public bool RequireEmailVerification { get; set; }
    
    public bool IsDeleted { get; set; }
}