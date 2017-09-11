using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace smnetcoreseed.web.Areas.Core.ViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}