using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.ViewModels
{
    public class BookInfoViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Cover { get; set; }
        public int Rating { get; set; }
        public BookStatus Status { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
        public int CommentsCount { get; set; }
        public bool YouLikeIt { get; set; }
    }
}
