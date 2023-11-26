using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using UzWorks.Identity.Constants;
using UzWorks.Identity.Models;

namespace UzWorks.Identity.ClaimsPrincipalFactory;

public class UzWorksClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
{

    public UzWorksClaimsPrincipalFactory(
        UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor
        ) : base(userManager, optionsAccessor) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));
        identity.AddClaim(new Claim(ClaimNames.UserName, user.UserName));
        identity.AddClaim(new Claim(ClaimNames.Email, user.Email));
        identity.AddClaim(new Claim(ClaimNames.UserId, user.Id));
        identity.AddClaim(new Claim(ClaimNames.FirstName, user.FirstName));
        identity.AddClaim(new Claim(ClaimNames.LastName, user.LastName));

        return identity;
    }
}
