﻿namespace UzWorks.Core.DataTransferObjects.Roles;

public class RoleDto
{
    public RoleDto(string name) 
    {
        Name = name;
    }
    public string Name { get; set; }
}
