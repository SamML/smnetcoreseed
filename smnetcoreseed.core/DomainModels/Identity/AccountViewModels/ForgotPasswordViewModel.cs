using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.core.DomainModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}