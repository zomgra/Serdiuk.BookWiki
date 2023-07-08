using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookWiki.Client.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BookController : Controller
    {
        public IActionResult Create()
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
    }
}
