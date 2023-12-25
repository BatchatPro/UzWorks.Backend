﻿using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.JobCategories;

public class JobCategoryDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
