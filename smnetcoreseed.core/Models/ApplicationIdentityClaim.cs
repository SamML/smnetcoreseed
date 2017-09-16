using Microsoft.AspNetCore.Identity;
using smnetcoreseed.core.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace smnetcoreseed.core.Models
{
    public class ApplicationIdentityClaim : Claim
    {

        public ApplicationIdentityClaim(string type, string value) : base(type, value)
        {
        }
        [Key]
        public string ClaimId { get; set; }
    }
}