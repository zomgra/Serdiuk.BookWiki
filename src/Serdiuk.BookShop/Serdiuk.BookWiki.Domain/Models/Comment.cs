namespace Serdiuk.BookShop.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public int Likes { get; set; }
        public Book? Book { get; set; }
        public string? WriterUsername { get; set; }
    }
}
