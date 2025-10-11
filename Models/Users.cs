using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoPIM4Web.Models
{
    public class Users : IdentityUser
    {
        public string Fullname { get; set; }

        [Required(ErrorMessage = "O ID Empresarial é obrigatório.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O ID Empresarial deve ter exatamente 4 caracteres.")]
        [Display(Name = "ID Empresarial")]
        public string EnterpriseId { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }
    }
}