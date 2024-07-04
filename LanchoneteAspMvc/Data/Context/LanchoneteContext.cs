using LanchoneteAspMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.Data.Context
{
    public class LanchoneteContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set;}
        public DbSet<Item> Itens { get; set;}
        public DbSet<Pedido> Pedidos { get; set;}
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set;}

        public LanchoneteContext(DbContextOptions<LanchoneteContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LanchoneteContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
