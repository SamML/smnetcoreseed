using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using smnetcoreseed.core.DomainModels;
using smnetcoreseed.core.Extensions.Identity;
using smnetcoreseed.core.Interfaces.Identity;
using System;
using System.Threading.Tasks;

namespace smnetcoreseed.core.Data.Identity
{
    public class CoreIdentityDbInitializer : IDatabaseInitializer
    {
        private readonly CoreIdentityDbContext _context;
        private readonly ICoreAccountManager _accountManager;
        private readonly ILogger _logger;

        public CoreIdentityDbInitializer(CoreIdentityDbContext context, ICoreAccountManager accountManager, ILogger<CoreIdentityDbInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await ensureRoleAsync(adminRoleName, "Default administrator", CoreApplicationPermissions.GetAllPermissionValues());
                await ensureRoleAsync(userRoleName, "Default user", new string[] { });

                await createUserAsync("admin", "admin@admin", "Inbuilt Administrator", "admin@admin.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await createUserAsync("user", "user@user", "Inbuilt Standard User", "user@user.com", "+1 (123) 000-0001", new string[] { userRoleName });
            }
        }

        private async Task ensureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                CoreIdentityRole applicationRole = new CoreIdentityRole(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private async Task<CoreIdentityUser> createUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            CoreIdentityUser applicationUser = new CoreIdentityUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");

            return applicationUser;
        }
    }
}