using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.core.DomainModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}