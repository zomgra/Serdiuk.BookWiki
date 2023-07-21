namespace Serdiuk.BookShop.Domain.Models.Requests.Comment
{
    public class CreateCommentRequest
    {
        public Guid BookId { get; set; }
        public string Content { get; set; }
    }
}
