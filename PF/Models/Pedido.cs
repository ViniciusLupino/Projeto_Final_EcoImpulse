using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [Required]
        public int StatusPedidoId { get; set; }
        public StatusPedido StatusPedido { get; set; }

        public bool Deletado { get; set; } = false;

        public List<DetalhePedido> DetalhesPedidos { get; set; }

    }
}
