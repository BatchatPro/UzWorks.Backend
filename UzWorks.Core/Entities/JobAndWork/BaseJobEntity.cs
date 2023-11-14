using NullGuard;

namespace UzWorks.Core.Entities.JobAndWork;

public class BaseJobEntity : BaseEntity
{
    public uint Salary {  get; set; }
    public string Gender { get; set; }
    public string WorkingTime { get; set; }
    public string WorkingSchedule { get; set; }
    public string Status { get; set; }
    public DateTime Deadline { get; set; }
    
    [AllowNull]
    public string TelegramLink { get; set; }
    
    [AllowNull]
    public string InstagramLink { get; set; }
    public string TgUserName { get; set; }
    public string PhoneNumber { get; set; }
}
