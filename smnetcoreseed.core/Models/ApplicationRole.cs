using Microsoft.AspNetCore.Identity;
using System;

namespace smnetcoreseed.core.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string RoleId { get; set; }
        public ApplicationIdentityClaim[] Claims { get; set; }
        public string Users { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }

        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ApplicationRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
    }
}