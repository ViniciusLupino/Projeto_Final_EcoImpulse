using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class DetalheCarrinho
    {
        [Key]
        public int IdDetalheCarrinho { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        [Required]
        public double PrecoUnitario { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
