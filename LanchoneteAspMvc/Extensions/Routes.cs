namespace LanchoneteAspMvc.Extensions
{
    public static class Routes
    {
        public static WebApplication UseMapControllerRoutes(this WebApplication app)
        {
            
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );
      

            app.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action=Catalogo}/{categoria?}",
                defaults: new { controller = "Lanche", action = "Catalogo" }
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            return app;
        }

    }
}
