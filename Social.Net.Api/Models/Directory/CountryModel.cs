using System.ComponentModel.DataAnnotations;

namespace Social.Net.Api.Models.Directory;

public record CountryModel : BaseEntityModel
{
    [MaxLength(64)]
    [MinLength(1)]
    [Required]
    public string Name { get; set; }
    
    [MaxLength(2)]
    [MinLength(2)]
    [Required]
    public string TwoLetterIsoCode { get; set; }

    [MaxLength(3)]
    [MinLength(3)]
    [Required]
    public string ThreeLetterIsoCode { get; set; }
    
    [Required]
    public int NumericIsoCode { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }
}