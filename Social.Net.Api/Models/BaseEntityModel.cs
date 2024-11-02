using Social.Net.Core.Domains.Common;

namespace Social.Net.Api.Models;

public abstract record BaseEntityModel : BaseModel
{
    public int Id { get; set; }
}