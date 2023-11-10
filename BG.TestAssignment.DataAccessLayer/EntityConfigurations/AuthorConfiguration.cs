using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BGNet.TestAssignment.DataAccess.EntityConfigurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasMany(p => p.Books)
                .WithMany(c => c.Authors)
                .UsingEntity(j => j.ToTable("AuthorBooks"));

        }
    }
}
