using NullGuard;

namespace UzWorks.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }

    [AllowNull]
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [AllowNull]
    public DateTime UpdateDate { get; set; }

    [AllowNull]
    public string? CreatedBy { get; set; }

    [AllowNull]
    public string? UpdatedBy { get; set; }

    [AllowNull]
    public bool? IsDeleted { get; set; } = false;
}
