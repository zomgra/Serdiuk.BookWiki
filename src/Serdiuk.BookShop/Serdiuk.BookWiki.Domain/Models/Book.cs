namespace Serdiuk.BookShop.Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Cover { get; set; }
        public List<Image> Images { get; set; }
        public BookStatus Status { get; set; }
        public List<Author> Authors { get; set; }
        public double Rating { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
