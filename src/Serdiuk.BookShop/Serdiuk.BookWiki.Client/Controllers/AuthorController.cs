using Microsoft.AspNetCore.Mvc;

namespace Serdiuk.BookWiki.Client.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index(Guid id)
        {
            return View(id);
        }
    }
}
