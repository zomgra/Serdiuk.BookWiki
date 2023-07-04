using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}
