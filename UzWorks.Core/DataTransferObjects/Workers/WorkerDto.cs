namespace UzWorks.Core.DataTransferObjects.Workers;

public class WorkerDto
{
    public DateTime Deadline { get; set; }
    public DateTime BirthDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public uint Salary { get; set; } = uint.MinValue;
    public string Gender { get; set; } = string.Empty;
    public string WorkingTime { get; set; } = string.Empty;
    public string WorkingSchedule { get; set; } = string.Empty;
    public string TgLink { get; set; } = string.Empty;
    public string InstagramLink { get; set; } = string.Empty;
    public string TgUserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty; 
    public Guid? DistrictId { get; set; }
    public Guid? CategoryId { get; set; }
}
