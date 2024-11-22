using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class DetalhePedido
    {
        [Key]
        public int IdDetalhePedido { get; set; }
        [Required]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public double PrecoUnitario { get; set; }
    }
}
