using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class EstoqueDataTransferObj
	{
		public int ProdutoId { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Estoque não pode assumir valores negativos")]
		public int Estoque { get; set; }
	}
}
