using System.ComponentModel.DataAnnotations;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Users;

public class RefreshToken : BaseEntity
{
    [Required]
    public string Token { get; set; } = Guid.NewGuid().ToString("N");

    [Required]
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);

    [Required]
    public bool Active { get; set; } = true;

    [Required]
    public int ProfileId { get; set; }
}