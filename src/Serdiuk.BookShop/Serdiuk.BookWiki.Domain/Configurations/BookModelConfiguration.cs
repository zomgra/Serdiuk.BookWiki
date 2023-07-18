using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.Configurations
{
    public class BookModelConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Images);
            builder.HasMany(x => x.Authors)
       .WithMany(x => x.Books);
            builder.HasOne(x => x.Cover);
            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Book);

            builder.HasMany(x => x.LikedUsers).WithMany(x => x.LikedBooks);
        }
    }
}
