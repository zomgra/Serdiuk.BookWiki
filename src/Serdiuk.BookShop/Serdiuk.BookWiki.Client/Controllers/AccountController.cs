using Microsoft.AspNetCore.Mvc;
using Serdiuk.BookShop.Domain.Models.Identity.DTO;

namespace Serdiuk.BookWiki.Client.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            var request = new LoginRequestDto();
            return View(request);
        }
        public IActionResult Register()
        {
            var request = new RegisterRequestDto();
            return View(request);
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
