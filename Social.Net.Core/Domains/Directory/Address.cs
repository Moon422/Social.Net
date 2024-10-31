using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Directory;

public class Address : BaseEntity
{
    public string AddressLine1 { get; set; }
    
    public string AddressLine2 { get; set; }
    
    public string City { get; set; }

    public int StateProvinceId { get; set; }
    public StateProvince StateProvince { get; set; }
}