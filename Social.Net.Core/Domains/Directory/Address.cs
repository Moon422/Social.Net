using System.ComponentModel.DataAnnotations;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class Address : BaseEntity
{
    [MinLength(1)]
    [MaxLength(512)]
    public string AddressLine1 { get; set; }
    
    [MaxLength(512)]
    public string? AddressLine2 { get; set; }
    
    [MinLength(1)]
    [MaxLength(256)]
    public string City { get; set; }
    
    [MaxLength(20)]
    public string PostalCode { get; set; }

    public int StateProvinceId { get; set; }
}