using System.ComponentModel.DataAnnotations;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Workers;

public class WorkerDto
{
    [Required]
    public DateTime Deadline { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public uint Salary { get; set; } = uint.MinValue;
    [Required]
    public GenderEnum Gender { get; set; } = GenderEnum.Unknown;
    [Required]
    public string WorkingTime { get; set; } = string.Empty;
    [Required]
    public string WorkingSchedule { get; set; } = string.Empty;
    public string TelegramLink { get; set; } = string.Empty;
    public string InstagramLink { get; set; } = string.Empty;
    [Required]
    public string TgUserName { get; set; } = string.Empty;
    [Required]
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty; 
    [Required]
    public Guid DistrictId { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
}
