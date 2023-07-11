namespace Serdiuk.BookShop.Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image? Cover { get; set; } = new();
        public List<Image>? Images { get; set; } = new();
        public BookStatus Status { get; set; }
        public ICollection<Author>? Authors { get; set; }
        public int Rating { get; set; }
        public List<Comment>? Comments { get; set; } = new();
    }
}
