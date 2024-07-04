using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.HasKey(c => c.Id);
            builder.Property(l => l.Nome)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(l => l.Descricao)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
