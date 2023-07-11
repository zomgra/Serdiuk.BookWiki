using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookWiki.Client.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var enumValues = Enum.GetValues(typeof(BookStatus));

            var items = enumValues
                .Cast<BookStatus>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                })
                .ToList();

            ViewBag.Status = items;
            return View();
        }
        public IActionResult Get(Guid id)
        {
            return View(id);
        }
    }
}
