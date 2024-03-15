namespace UzWorks.Core.DataTransferObjects.FAQs;

public class FAQVM
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public DateTime CreateDate { get; set; }
}
