using LanchoneteAspMvc.Areas.Admin.Repositories;
using LanchoneteAspMvc.Controllers;
using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LanchoneteContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<LanchoneteContext>()
                .AddDefaultTokenProviders();

            ////services.Configure<IdentityOptions>(options =>
            ////{
            ////    // Default password settings
            ////    options.Password.RequireNonAlphanumeric = false;
            ////    options.Password.RequireDigit = false;
            ////    options.Password.RequireLowercase = false;
            ////    options.Password.RequiredLength = 6;
            ////    options.Password.RequireUppercase = false;
            ////    options.Password.RequiredUniqueChars = 1;

            ////});
            

            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped(carinho => Carrinho.RetornaCarrinho(carinho));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin")
                    );
            });

            return services;
        }
    }
}
