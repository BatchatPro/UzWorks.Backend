using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Experiences;

public class ExperienceDto
{
    [Required]
    public string CompanyName { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public string Description { get; set; }
}
