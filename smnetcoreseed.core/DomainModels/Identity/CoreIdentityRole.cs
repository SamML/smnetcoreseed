using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace smnetcoreseed.core.DomainModels
{
    public class CoreIdentityRole : IdentityRole
    {
        public string RoleId { get; set; }
        public CoreIdentityClaim[] Claims { get; set; }
        public string Users { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        

        public CoreIdentityRole() :base()
        {
        }

      
        public CoreIdentityRole(string roleName) : base(roleName)
        {
        }

     
        public CoreIdentityRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }

       
    }
}