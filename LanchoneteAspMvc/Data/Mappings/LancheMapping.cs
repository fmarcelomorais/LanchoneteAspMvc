using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.Data.Mappings
{
    public class LancheMapping : IEntityTypeConfiguration<Lanche>
    {
        public void Configure(EntityTypeBuilder<Lanche> builder)
        {
            builder.ToTable("Lanches");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Nome)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(l => l.Preco)
                .HasPrecision(10, 2)
                .HasConversion<decimal>()
                .IsRequired();
            builder.Property(l => l.DescricaoCurta)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(l => l.DescricaoLonga)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
