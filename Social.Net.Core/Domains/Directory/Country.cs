using System.ComponentModel.DataAnnotations;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class Country : BaseEntity
{
    [MaxLength(64)]
    [MinLength(1)]
    public string Name { get; set; }
    
    [MaxLength(2)]
    public string TwoLetterIsoCode { get; set; }

    [MaxLength(3)]
    public string ThreeLetterIsoCode { get; set; }
    
    public int NumericIsoCode { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }
}