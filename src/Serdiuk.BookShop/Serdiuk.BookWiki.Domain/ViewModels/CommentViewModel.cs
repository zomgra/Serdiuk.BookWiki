namespace Serdiuk.BookShop.Domain.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string WriterUsername { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
    }
}
