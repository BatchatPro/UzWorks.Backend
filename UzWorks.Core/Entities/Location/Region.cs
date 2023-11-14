using NullGuard;

namespace UzWorks.Core.Entities.Location;

public class Region : BaseEntity
{
    public string Name { get; set; }
    
    [AllowNull]
    public List<District>? Districts { get; set; }

}
