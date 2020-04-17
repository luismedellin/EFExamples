using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Library.CodeFirst.Models;

namespace Library.CodeFirst.ModelConfigurations
{
    internal class GenreConfiguration: EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            ToTable("Genre");

            Property(g => g.Description)
                .HasColumnAnnotation(
                    "IX_Description",
                    new IndexAnnotation(new IndexAttribute("IX_Description") {IsUnique = true})
                );

            HasMany(g => g.Books)
                .WithRequired(b => b.Genre)
                .HasForeignKey(a => new {a.GenreId})
                .WillCascadeOnDelete(false);
        }
    }
}
