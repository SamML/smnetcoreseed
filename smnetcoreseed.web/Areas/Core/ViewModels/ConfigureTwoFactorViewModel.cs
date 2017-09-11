using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace smnetcoreseed.web.Areas.Core.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}