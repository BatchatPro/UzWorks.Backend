using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Workers;

public class WorkerEM : WorkerDto
{
    [Required]
    public Guid Id { get; set; }
}