using smnetcoreseed.core.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smnetcoreseed.core.Interfaces.Identity
{
    public interface ICoreAccountManager
    {
        Task<bool> CheckPasswordAsync(CoreIdentityUser user, string password);

        Task<Tuple<bool, string[]>> CreateRoleAsync(CoreIdentityRole role, IEnumerable<string> claims);

        Task<Tuple<bool, string[]>> CreateUserAsync(CoreIdentityUser user, IEnumerable<string> roles, string password);

        Task<Tuple<bool, string[]>> DeleteRoleAsync(CoreIdentityRole role);

        Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName);

        Task<Tuple<bool, string[]>> DeleteUserAsync(CoreIdentityUser user);

        Task<Tuple<bool, string[]>> DeleteUserAsync(string userId);

        Task<CoreIdentityRole> GetRoleByIdAsync(string roleId);

        Task<CoreIdentityRole> GetRoleByNameAsync(string roleName);

        Task<CoreIdentityRole> GetRoleLoadRelatedAsync(string roleName);

        Task<List<CoreIdentityRole>> GetRolesLoadRelatedAsync(int page, int pageSize);

        Task<Tuple<CoreIdentityUser, string[]>> GetUserAndRolesAsync(string userId);

        Task<CoreIdentityUser> GetUserByEmailAsync(string email);

        Task<CoreIdentityUser> GetUserByIdAsync(string userId);

        Task<CoreIdentityUser> GetUserByUserNameAsync(string userName);

        Task<IList<string>> GetUserRolesAsync(CoreIdentityUser user);

        Task<List<Tuple<CoreIdentityUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize);

        Task<Tuple<bool, string[]>> ResetPasswordAsync(CoreIdentityUser user, string newPassword);

        Task<Tuple<bool, string[]>> UpdatePasswordAsync(CoreIdentityUser user, string currentPassword, string newPassword);

        Task<Tuple<bool, string[]>> UpdateRoleAsync(CoreIdentityRole role, IEnumerable<string> claims);

        Task<Tuple<bool, string[]>> UpdateUserAsync(CoreIdentityUser user);

        Task<Tuple<bool, string[]>> UpdateUserAsync(CoreIdentityUser user, IEnumerable<string> roles);
    }
}