using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Auth;

public class LoginResponseDto
{
    public LoginResponseDto(
                Guid id, string token, DateTime expiration, 
                string firstName, string lastName, 
                GenderEnum? gender, DateTime birthDate, 
                string phoneNumber,Guid? districtId, 
                string? districtName,string? regionName,
                IList<string> roles)
    {
        Id = id;
        Token = token;
        Expiration = expiration;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        DistrictId = districtId;
        DistrictName = districtName;
        RegionNamee = regionName;
        Roles = roles;
    }

    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public GenderEnum? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid? DistrictId { get; set; }  
    public string? DistrictName {  get; set; }
    public string? RegionNamee { get; set; }
    public IList<string>? Roles { get; set;}
}
