using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smnetcoreseed.web.Areas.Core
{
    //[Authorize]
    [Area("Core")]
    [Route("core")]
    public class CoreController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        virtual public IActionResult Index()
        {
            // Main view of the account controller/Area
            return View();
        }
    }
}