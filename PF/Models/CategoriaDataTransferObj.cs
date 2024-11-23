using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class CategoriaDataTransferObj
	{
		public int IdCategoria { get; set; }
		[Required]
		[MaxLength(40)]
		public string CategoriaNome { get; set; }
	}
}
