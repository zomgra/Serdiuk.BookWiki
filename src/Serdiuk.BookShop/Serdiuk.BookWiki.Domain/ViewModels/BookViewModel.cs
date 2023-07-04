using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Cover { get; set; }
        public List<byte[]> Images { get; set; }
        public double Rating { get; set; }
        public BookStatus Status { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
