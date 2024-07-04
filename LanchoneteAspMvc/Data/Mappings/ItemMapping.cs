using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LanchoneteAspMvc.Data.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Itens");
            builder.HasKey(x => x.Id);
            builder.Property(car => car.CarrinhoId).HasMaxLength(200);
        }
    }
}
