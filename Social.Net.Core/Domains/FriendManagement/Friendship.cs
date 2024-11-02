using Social.Net.Core.Domains.Common;
using Social.Net.Core.Domains.Users;

namespace Social.Net.Core.Domains.FriendManagement;

public class Friendship : BaseEntity
{
    public int ProfileId1 { get; set; }
    
    public int ProfileId2 { get; set; }

    public DateTime FriendListedOn { get; set; }
}