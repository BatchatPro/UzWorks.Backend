﻿using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Users;

public class UserVM
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? MobileId { get; set; }
    public DateTime BirthDate { get; set; }
}
