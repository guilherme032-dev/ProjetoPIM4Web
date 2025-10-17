using System.ComponentModel.DataAnnotations;

namespace ProjetoPIM4.Models
{
    public class ProductService
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto/serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto/serviço não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A categoria do produto/serviço é obrigatória.")]
        [StringLength(100, ErrorMessage = "A categoria não pode exceder 100 caracteres.")]
        public string Category { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

