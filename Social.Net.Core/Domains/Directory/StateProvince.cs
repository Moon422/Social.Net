using System.ComponentModel.DataAnnotations;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class StateProvince : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    public string Name { get; set; }
    
    [MinLength(2)]
    [MaxLength(3)]
    public string Abbreviation { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public int CountryId { get; set; }
    public Country Country { get; set; }
}