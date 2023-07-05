using Microsoft.AspNetCore.Mvc;

namespace Serdiuk.BookWiki.Client.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(6);
        }
        public IActionResult AuthorCreate()
        {
            return View();
        }
    }
}
