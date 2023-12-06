using NullGuard;

namespace UzWorks.Core.Entities;

public class BaseEntity : IHasIsDeletedProperty
{
    public Guid Id { get; set; }

    [AllowNull]
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [AllowNull]
    public DateTime UpdateDate { get; set; }

    [AllowNull]
    public Guid? CreatedBy { get; set; }

    [AllowNull]
    public Guid? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
}
