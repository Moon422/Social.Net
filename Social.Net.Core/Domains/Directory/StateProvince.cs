using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class StateProvince : BaseEntity
{
    public string Name { get; set; }
    
    public string Abbreviation { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public int CountryId { get; set; }
    public Country Country { get; set; }
}