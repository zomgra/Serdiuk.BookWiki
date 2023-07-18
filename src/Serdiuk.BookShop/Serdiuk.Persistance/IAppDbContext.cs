using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.Persistance
{
    public interface IAppDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
