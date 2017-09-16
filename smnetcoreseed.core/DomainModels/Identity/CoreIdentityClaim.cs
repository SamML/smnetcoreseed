using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace smnetcoreseed.core.DomainModels
{
    public class CoreIdentityClaim : Claim
    {
        public CoreIdentityClaim(string type, string value) : base(type, value)
        {
        }

        [Key]
        public string ClaimId { get; set; }
    }
}