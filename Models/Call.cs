using ProjetoPIM4Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPIM4.Models
{
    public class Call
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do chamado é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título não pode exceder 200 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição do chamado é obrigatória.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A data de abertura do chamado é obrigatória.")]
        public DateTime OpenedDate { get; set; } = DateTime.Now;

        public DateTime? ClosedDate { get; set; }

        [Required(ErrorMessage = "O status do chamado é obrigatório.")]
        public string Status { get; set; } = "Aberto"; // Ex: Aberto, Em Andamento, Resolvido, Fechado

        public string Priority { get; set; } = "Baixa"; // Ex: Baixa, Média, Alta, Urgente

        // Chave estrangeira para o usuário que abriu o chamado
        [Required(ErrorMessage = "O usuário que abriu o chamado é obrigatório.")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        // Chave estrangeira para o produto/serviço relacionado ao chamado
        [Required(ErrorMessage = "O produto/serviço relacionado é obrigatório.")]
        public int ProductServiceId { get; set; }
        [ForeignKey("ProductServiceId")]
        public ProductService ProductService { get; set; }

    }
}
