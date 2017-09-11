using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.web.Areas.Core.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}