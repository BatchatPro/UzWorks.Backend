using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Location.Districts;

public class DistrictEM
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid RegionId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}
