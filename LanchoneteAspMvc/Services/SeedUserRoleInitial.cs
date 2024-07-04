using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace LanchoneteAspMvc.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public static void CriarPerfis(WebApplication app) 
        {
            var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
                service.SeedUser();
                service.SeedRoles();
            }

        }

        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public async void SeedUser()
        {
            if(_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                var username = "usuario@localhost";
                var email = "usuario@localhost";

                user.UserName = username;
                user.Email = email;
                user.NormalizedUserName = username.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult userResult = _userManager.CreateAsync(user, "Teste*2024").Result;

                if(userResult.Succeeded) 
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                var username = "admin@localhost";
                var email = "admin@localhost";

                user.UserName = username;
                user.Email = email;
                user.NormalizedUserName = username.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult userResult = _userManager.CreateAsync(user, "Teste*2024").Result;

                if (userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

        }



    }
}
