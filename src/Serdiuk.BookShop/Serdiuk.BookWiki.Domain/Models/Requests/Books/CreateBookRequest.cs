namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class CreateBookRequest
    {
        public Guid[] Authors { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
