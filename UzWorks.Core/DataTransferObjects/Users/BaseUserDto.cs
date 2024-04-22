﻿using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Users;

public class BaseUserDto
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public Gender? Gender { get; set; }
    public string? MobileId { get; set; }
    public DateTime BirthDate { get; set; }
}

