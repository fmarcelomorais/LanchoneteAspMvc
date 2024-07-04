using LanchoneteAspMvc.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Controllers
{
    public class AccountController : Controller
    {
       private readonly UserManager<IdentityUser> _userManager;
       private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            var login = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar login");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registerVM)
        {
            if (!ModelState.IsValid) 
            { 
                return View(registerVM);
            }

            var user = new IdentityUser()
            {
                UserName = registerVM.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Member").Wait();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                this.ModelState.AddModelError("Registro", "Falha ao realizar Cadastro");
            }

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
