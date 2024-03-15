namespace UzWorks.Core.Entities.Feedbacks;

public class FeedBack : BaseEntity
{
    public string Message { get; set; }
    public string FullName { get; set; }
    public DateTime DueDate { get; set; }
}
