using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.FAQs;

public class FAQEM
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Question { get; set; }
    
    [Required]
    public string Answer { get; set; }
}
