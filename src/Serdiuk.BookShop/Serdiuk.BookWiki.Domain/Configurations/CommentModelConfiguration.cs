using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serdiuk.BookShop.Domain.Models;

namespace Serdiuk.BookShop.Domain.Configurations
{
    public class CommentModelConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.WriterUsername);
            builder.Property(x => x.Likes);
            builder.Property(x => x.Content);

            builder.HasOne(x => x.Book).WithMany(x=>x.Comments);
        }
    }
}
