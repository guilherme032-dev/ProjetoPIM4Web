using System.ComponentModel.DataAnnotations;

namespace ProjetoPIM4Web.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "É Preciso do Email.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
