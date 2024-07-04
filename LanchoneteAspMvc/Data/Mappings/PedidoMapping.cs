using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanchoneteAspMvc.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Sobrenome).HasMaxLength(200);
            builder.Property(p => p.Telefone).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Endereco).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Estado).HasMaxLength(2).IsRequired();
            builder.Property(p => p.PedidoTotal).HasPrecision(10,2).HasConversion<decimal>();
            builder.Property(p => p.PedidoEnviado).HasDefaultValueSql("NOW()");
        }
    }
}
