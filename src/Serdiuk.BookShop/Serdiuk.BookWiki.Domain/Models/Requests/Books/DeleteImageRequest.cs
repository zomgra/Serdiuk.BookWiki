namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class DeleteImageRequest
    {
        public Guid BookId { get; set; }
        public Guid ImageId { get; set; }
    }
}
