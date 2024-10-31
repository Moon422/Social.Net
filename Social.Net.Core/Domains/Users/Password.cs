using Social.Net.Core.Domains.Common;

namespace Social.Net.Core.Domains.Users;

public class Password : BaseEntity
{
    public string Hash { get; set; }

    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
}