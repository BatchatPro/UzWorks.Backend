namespace UzWorks.Core.DataTransferObjects.Jobs;

public class JobVM
{
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public uint Salary { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string WorkingTime {  get; set; } = string.Empty;
    public string WorkingSchedule { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public string TgLink { get; set; } = string.Empty;
    public string InstagramLink { get; set; } = string.Empty;   
    public string TgUserName { get; set; } = string.Empty; 
    public string PhoneNumber { get; set; } = string.Empty;
    public string Benefit { get; set; } = string.Empty;
    public string Requirement { get; set; } = string.Empty;
    public int MinAge {  get; set; }
    public int MaxAge { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? DistrictId { get; set; }
}
