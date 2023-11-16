namespace UzWorks.Core.DataTransferObjects.Location.Districts;

public class DistrictVM
{
    public Guid? Id { get; set; }
    public Guid? RegionId { get; set; }
    public string Name { get; set; } = string.Empty;
}
