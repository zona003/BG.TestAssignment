using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGNet.TestAssignment.DataAccess.EntityConfigurations
{
    internal class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Author)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
