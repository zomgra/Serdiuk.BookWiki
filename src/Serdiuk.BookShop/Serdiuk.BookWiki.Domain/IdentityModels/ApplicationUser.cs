using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public List<Comment> Comments { get; set; } = new();
        public virtual ICollection<Book> LikedBooks { get; set; } = new List<Book>();
    }
}
