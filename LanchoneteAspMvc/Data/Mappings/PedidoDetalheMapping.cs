using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanchoneteAspMvc.Data.Mappings
{
    public class PedidoDetalheMapping : IEntityTypeConfiguration<PedidoDetalhe>
    {
        public void Configure(EntityTypeBuilder<PedidoDetalhe> builder)
        {
            builder.ToTable("PedidosDetalhes");
            builder.HasKey(p =>  p.Id);
            builder.Property(p => p.Preco)
                .HasConversion<decimal>()
                .HasPrecision(10,2);
        }
    }
}
