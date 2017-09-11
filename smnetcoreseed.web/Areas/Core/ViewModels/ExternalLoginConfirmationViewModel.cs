using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.web.Areas.Core.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}