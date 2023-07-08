using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.Configurations
{
    public class AuthorModelConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Books)
                .WithMany(x => x.Authors);

        }
    }
}
