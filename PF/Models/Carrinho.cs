using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class Carrinho
    {
        [Key]
        public int IdCarrinho { get; set; }
        [Required]
        public string UserId { get; set; }

        public bool Deletado { get; set; } = false;
        public ICollection<DetalheCarrinho> CarrinhoDetalhes { get; set; }
    }
}
