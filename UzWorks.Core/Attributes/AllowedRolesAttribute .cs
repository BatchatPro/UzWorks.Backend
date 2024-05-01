using System.ComponentModel.DataAnnotations;
using UzWorks.Core.Constants;

namespace UzWorks.Core.Attributes;

public class AllowedRolesAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var role = value as string;
        if (role !=null && (role == RoleNames.Employee || role == RoleNames.Employer))
            return ValidationResult.Success;

        return new ValidationResult($"Invalid role. Allowed values are '{RoleNames.Employee}' and '{RoleNames.Employer}'.");
    }
}
