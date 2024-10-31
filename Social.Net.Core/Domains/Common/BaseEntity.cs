namespace Social.Net.Core.Domains.Common;

public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    DateTime DeletedOn { get; set; }
}

public interface ICreationAuditable
{
    DateTime CreatedOn { get; set; }
}

public interface IModificationAuditable
{
    DateTime? ModifiedOn { get; set; }
}

public abstract class BaseEntity
{
    public int Id { get; set; }
}