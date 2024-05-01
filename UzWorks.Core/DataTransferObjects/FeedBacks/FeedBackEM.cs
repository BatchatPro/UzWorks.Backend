using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.FeedBacks;

public class FeedBackEM
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Message { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
}
