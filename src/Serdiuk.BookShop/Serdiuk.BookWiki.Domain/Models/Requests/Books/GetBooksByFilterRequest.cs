namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class GetBooksByFilterRequest
    {
        public BookStatus Status { get; set; }
        public int Page { get; set; } = 1;
    }
}
