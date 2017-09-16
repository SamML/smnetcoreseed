using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.core.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}