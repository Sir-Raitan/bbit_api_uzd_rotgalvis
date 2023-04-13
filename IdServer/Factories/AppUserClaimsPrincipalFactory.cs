using IdentityModel;
using IdServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace IdServer.Factories
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public AppUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)  : base(userManager, optionsAccessor) 
        {
        
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);

            if (user.FirstName != null ) 
            {
                claimsIdentity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            }

            if (user.LastName != null)
            {
                claimsIdentity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            }

            if (user.ResidentId != null)
            {
                claimsIdentity.AddClaim(new Claim("resident_id", user.ResidentId));
            }

            return claimsIdentity;
        }
    }
}
