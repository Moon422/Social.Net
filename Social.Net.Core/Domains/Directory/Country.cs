using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class Country : BaseEntity
{
    public string Name { get; set; }
    
    public string TwoLetterIsoCode { get; set; }

    public string ThreeLetterIsoCode { get; set; }
    
    public int NumericIsoCode { get; set; }
    
    public bool Published { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public IList<StateProvince> StateProvinces { get; set; }
}