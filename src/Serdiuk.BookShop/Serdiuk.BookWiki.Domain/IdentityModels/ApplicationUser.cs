using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> LikedBooks { get; set; }
    }
}
