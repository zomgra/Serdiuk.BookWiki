using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookWiki.Client.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthorController : Controller
    {
        public IActionResult Create()
        {
           

            return View();
        }
    }
}
