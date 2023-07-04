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

            builder.Property(x => x.Name);
            builder.Property(x => x.Description);

            builder.HasMany(x=>x.Images);
            builder.HasMany(x=>x.Authors).WithMany(x=>x.Books);
            builder.HasMany(x => x.Comments).WithOne(x => x.Book);
        }
    }
}
