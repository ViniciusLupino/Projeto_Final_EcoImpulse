using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class StatusPedido
    {
        [Key]
        public int IdStatusPedido { get; set; }
        [Required]
        public int StatusId { get; set; }
        [MaxLength(20), Required]
        public string? StatusNome { get; set; }

    }
}
