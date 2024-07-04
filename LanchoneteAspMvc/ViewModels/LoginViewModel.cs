using System.ComponentModel.DataAnnotations;

namespace LanchoneteAspMvc.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informa o nome de usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
