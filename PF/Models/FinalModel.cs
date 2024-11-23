using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class FinalModel
	{
		[Required]
		[MaxLength(30)]
		public string? NomeRemetente { get; set; }
		[Required]
		[EmailAddress]
		public string? EmailRemetente { get; set; }
		[Required]
		public string? TelefoneRemetente { get; set; }
		[Required]
		[MaxLength(200)]
		public string? EnderecoRemetente { get; set; }
		[Required]
		[MaxLength(30)]
		public string? MetodoDePagamento { get; set; }
	}
}
