namespace UzWorks.Core.Checkers;

public class PhoneNumberService
{
    public bool IsFormValid(string phoneNumber)
    {
        if (phoneNumber.StartsWith("998") && phoneNumber.Length == 12 && phoneNumber.All(char.IsDigit))
            return true;

        return false;
    }
}
