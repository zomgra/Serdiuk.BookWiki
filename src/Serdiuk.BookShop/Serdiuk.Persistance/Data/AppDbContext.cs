using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serdiuk.BookShop.Domain.Configurations;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.Persistance.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BookModelConfiguration());
            builder.ApplyConfiguration(new AuthorModelConfiguration());
            builder.ApplyConfiguration(new ImageModelConfiguration());
            builder.ApplyConfiguration(new CommentModelConfiguration());

        }
    }
}
