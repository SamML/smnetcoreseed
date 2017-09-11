using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace smnetcoreseed.core.DomainModels
{
    public class CoreIdentityRole : IdentityRole
    {
        public string RoleId { get; set; }
        public Claim[] Claims { get; set; }
        public string[] Users { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        
        
        
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRole"/>.
        /// </summary>
        /// <remarks>
        /// The Id property is initialized to from a new GUID string value.
        /// </remarks>
        public CoreIdentityRole() :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRole"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <remarks>
        /// The Id property is initialized to from a new GUID string value.
        /// </remarks>
        public CoreIdentityRole(string roleName) : base(roleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRole"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="description">Description of the role.</param>
        /// <remarks>
        /// The Id property is initialized to from a new GUID string value.
        /// </remarks>
        public CoreIdentityRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }

        /// <summary>
        /// Gets or sets the description for this role.
        /// </summary>
    }
}