using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Jobs;

public class JobDto
{
    [Required(ErrorMessage = "This field is required.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public uint Salary { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public string Gender { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string WorkingTime { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string WorkingSchedule { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public DateTime Deadline { get; set; }
    
    public string TelegramLink { get; set; } = string.Empty;
    
    public string InstagramLink { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string TgUserName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string Benefit { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public string Requirement { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "This field is required.")]
    public int MinAge { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public int MaxAge { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public double Latitude { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public double Longitude { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public Guid CategoryId { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    public Guid DistrictId { get; set; }
}
