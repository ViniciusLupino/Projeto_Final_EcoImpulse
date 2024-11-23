using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class EstoqueDisplayModel
	{
		[Key]
		public int IdEstoqueDM {  get; set; }
		public int ProdutoId { get; set; }
		public int Quantidade { get; set; }
		public string? ProdutoNome { get; set; }
	}
}
