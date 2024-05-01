using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.FAQs;

public class FAQDto
{
    [Required]
    public string Question { get; set; }
    
    [Required]
    public string Answer { get; set; }
}
