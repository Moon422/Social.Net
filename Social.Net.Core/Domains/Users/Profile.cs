using System.ComponentModel.DataAnnotations;
using Social.Net.Core.Domains.Common;
using Social.Net.Core.Domains.Directory;

namespace Social.Net.Core.Domains.Users;

public class Profile : BaseEntity, ICreationAuditable, IModificationAuditable, ISoftDeleted
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
    
    public int PresentAddressId { get; set; }
    public Address PresentAddress { get; set; }
    
    public int? PermanentAddressId { get; set; }
    public Address? PermanentAddress { get; set; }
    
    public bool IsActive { get; set; }
    
    public bool RequireAuthentication { get; set; }
    
    public bool RequireEmailVerification { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedOn { get; set; }
}

