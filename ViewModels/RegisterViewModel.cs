using System.ComponentModel.DataAnnotations;

namespace ProjetoPIM4Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres de comprimento.", MinimumLength = 5)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O ID Empresarial é obrigatório.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O ID Empresarial deve ter exatamente 4 caracteres.")]
        [Display(Name = "ID Empresarial")]
        public string EnterpriseId { get; set; }
    }
}
