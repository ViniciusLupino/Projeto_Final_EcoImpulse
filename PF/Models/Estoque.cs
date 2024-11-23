using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class Estoque
	{
		[Key]
		public int IdEstoque { get; set; }
		public int ProdutoId { get; set; }
		public int QuantidadeEstoque { get; set; }
		public Produto? Produtos { get; set; }
	}
}
