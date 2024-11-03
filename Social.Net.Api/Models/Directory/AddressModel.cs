using System.ComponentModel.DataAnnotations;

namespace Social.Net.Api.Models.Directory;

public record AddressModel : BaseEntityModel
{
    [MinLength(1)]
    [MaxLength(512)]
    [Required]
    public string AddressLine1 { get; set; }
    
    [MaxLength(512)]
    public string? AddressLine2 { get; set; }
    
    [MinLength(1)]
    [MaxLength(256)]
    [Required]
    public string City { get; set; }
    
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required]
    public int StateProvinceId { get; set; }
}