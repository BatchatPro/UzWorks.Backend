using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Jobs;

public class JobEM : JobDto
{
    [Required(ErrorMessage = "This field is required.")]
    public Guid Id { get; set; }
}
