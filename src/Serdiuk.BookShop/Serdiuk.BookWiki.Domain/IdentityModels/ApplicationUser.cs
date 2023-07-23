using Microsoft.AspNetCore.Identity;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Comment> LikedComments { get; set; } = new List<Comment>();
        public virtual ICollection<Book> LikedBooks { get; set; } = new List<Book>();
    }
}
