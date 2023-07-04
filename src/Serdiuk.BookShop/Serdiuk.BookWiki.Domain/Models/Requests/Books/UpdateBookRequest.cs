namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class UpdateBookRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Author[] Authors { get; set; }
    }
}
