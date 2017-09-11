using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using smnetcoreseed.core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class ManageUserByIdRequirement : IAuthorizationRequirement
    {
    }

    public class ManageUserByIdHandler : AuthorizationHandler<ManageUserByIdRequirement, string>
    {
        private UserManager<ApplicationUser> usermanager;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageUserByIdRequirement requirement, string userId)
        {
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageUsers) || GetIsSameUser(context.User, userId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool GetIsSameUser(ClaimsPrincipal user, string userId)
        {
            return usermanager.GetUserId(user) == userId;
        }
    }
}