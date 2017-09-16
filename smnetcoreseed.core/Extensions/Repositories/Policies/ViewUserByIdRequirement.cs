using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using smnetcoreseed.core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class ViewUserByIdRequirement : IAuthorizationRequirement
    {
    }

    public class ViewUserByIdHandler : AuthorizationHandler<ViewUserByIdRequirement, string>
    {
        private UserManager<smnetcoreseed.core.Models.ApplicationUser> usermanager;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewUserByIdRequirement requirement, string targetUserId)
        {
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewUsers) || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            return usermanager.GetUserId(user) == targetUserId;
        }
    }
}