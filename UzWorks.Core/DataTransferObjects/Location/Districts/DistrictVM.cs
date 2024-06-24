using UzWorks.Core.DataTransferObjects.Location.Regions;

namespace UzWorks.Core.DataTransferObjects.Location.Districts;

public class DistrictVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public RegionVM Region { get; set; }

}
