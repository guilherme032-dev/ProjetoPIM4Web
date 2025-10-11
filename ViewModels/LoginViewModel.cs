using System.ComponentModel.DataAnnotations;

namespace ProjetoPIM4Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O ID Empresarial é obrigatório.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O ID Empresarial deve ter exatamente 4 caracteres.")]
        [Display(Name = "ID Empresarial")]
        public string EnterpriseId { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }
}
