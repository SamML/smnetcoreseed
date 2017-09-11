using Microsoft.AspNetCore.Identity;
using System;

namespace smnetcoreseed.core.DomainModels
{
    public class CoreIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
        public CoreIdentityRole[] Roles { get; set; }
    }
}