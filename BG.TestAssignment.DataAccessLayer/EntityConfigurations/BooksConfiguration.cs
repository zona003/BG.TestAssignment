using BG.TestAssignment.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BG.TestAssignment.DataAccess.EntityConfigurations
{
    internal class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
