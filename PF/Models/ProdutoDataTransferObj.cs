using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PF.Models
{
	public class ProdutoDataTransferObj
	{
		public int ProdutoId { get; set; }
		[Required]
		[MaxLength(40)]
		public string? ProdutoNome { get; set; }
		[Required]
		[MaxLength(80)]
		public string? ProdutoDescricao { get; set; }
		[Required]
		public double Preco { get; set; }
		public string? Imagem { get; set; }
		[Required]
		public int CategoriaId { get; set; }
		public IFormFile? ArquivoImagem { get; set; }
		public IEnumerable<SelectListItem>? CategoriaList { get; set; }
	}
}
