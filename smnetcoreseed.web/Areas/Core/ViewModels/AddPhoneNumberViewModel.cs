using System.ComponentModel.DataAnnotations;

namespace smnetcoreseed.web.Areas.Core.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}