namespace Serdiuk.BookShop.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookInfoViewModel> Books { get; set; }
    }
}
