using System.ComponentModel.DataAnnotations;

namespace Social.Net.Api.Models.Directory;

public record StateProvinceModel : BaseEntityModel
{
    [MinLength(1)]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }
    
    [MinLength(2)]
    [MaxLength(3)]
    [Required]
    public string Abbreviation { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }

    [Required]
    public int CountryId { get; set; }
}