using assignement_3.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace assignement_3.PL.Views.Shared
{
    public class checks_role
    {
        private SignInManager<AppUser> SignInManager { get; }
        private UserManager<AppUser> UserManager { get; }
        private RoleManager<IdentityRole> RoleManager { get; }
        private readonly HttpContext _httpContext;
        public checks_role(SignInManager<AppUser> SignInManager, UserManager<AppUser> UserManager, RoleManager<IdentityRole> RoleManager, IHttpContextAccessor httpContextAccessor)
        {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            this.RoleManager = RoleManager;
            _httpContext = httpContextAccessor.HttpContext;
        }



        public async Task<IList<Claim>> check()
        {
            
          
                var user = await UserManager.FindByNameAsync(_httpContext.User.Identity.Name);
            IList<string> roles = new List<string>();
            if (user is not null)
            {
                await SignInManager.RefreshSignInAsync(user);
                roles = await UserManager.GetRolesAsync(user);
            }

            IdentityRole? getrole;
            IList<Claim> role_claims = new List<Claim>();
            if (roles.Count != 0)
            {
                getrole = await RoleManager.FindByNameAsync(roles.First());
                role_claims = await RoleManager.GetClaimsAsync(getrole);
            }

            return role_claims;
        }
    }
}
