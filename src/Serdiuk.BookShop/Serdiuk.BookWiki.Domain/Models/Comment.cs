using Serdiuk.BookShop.Domain.IdentityModels;

namespace Serdiuk.BookShop.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public ICollection<ApplicationUser> UserLiked { get; set; }
        public int Likes
        {
            get
            {
                return UserLiked.Count;
            }
            private set
            {
                Likes = value;
            }
        }
        public Book? Book { get; set; }
        public string? WriterUsername { get; set; }
        public ApplicationUser Writer { get; set; }
    }
}
