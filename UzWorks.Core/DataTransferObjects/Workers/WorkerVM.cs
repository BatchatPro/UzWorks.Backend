namespace UzWorks.Core.DataTransferObjects.Workers;
 
public class WorkerVM
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsTop { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public uint Salary { get; set; } = uint.MinValue;
    public string Gender { get; set; } = string.Empty;
    public string WorkingTime { get; set; } = string.Empty;
    public string WorkingSchedule { get; set; } = string.Empty;
    public string TelegramLink { get; set; } = string.Empty;
    public string InstagramLink { get; set; } = string.Empty;
    public string TgUserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}
