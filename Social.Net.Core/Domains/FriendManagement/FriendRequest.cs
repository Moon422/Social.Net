using Social.Net.Core.Domains.Common;
using Social.Net.Core.Domains.Users;

namespace Social.Net.Core.Domains.FriendManagement;

public class FriendRequest : BaseEntity, ICreationAuditable
{
    public int SenderProfileId { get; set; }
    
    public int ReceiverProfileId { get; set; }

    public int FriendRequestStatusId { get; set; }
    
    public DateTime AcceptedOn { get; set; }

    public DateTime CreatedOn { get; set; }
}