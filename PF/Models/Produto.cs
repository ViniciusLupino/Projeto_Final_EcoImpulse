using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

namespace PF.Models
{
	public class Produto
	{
		[Key]
		public int IdProduto { get; set; }
		[Required]
		[MaxLength(40)]
		public string ProdutoNome { get; set; }
		[Required]
		[MaxLength(256)]
		public string ProdutoDescricao { get; set; }
		[Required]
		public double ProdutoPreco { get; set; }
		[Required]
		public int CategoriaId { get; set; }
		public Categoria Categoria { get; set; }

		public string? Imagem { get; set; }

		public List<DetalhePedido> DetalhesPedidos { get; set; }
		public List<DetalheCarrinho> DetalhesCarrinho { get; set; }
		public Estoque Estoques { get; set; }
		[NotMapped]
		public string CategoriaNome { get; set; }
		[NotMapped]
		public int Quantidade { get; set; }
	}
}
