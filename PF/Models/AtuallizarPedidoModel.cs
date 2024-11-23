using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PF.Models
{
	public class AtuallizarPedidoModel
	{
		public int PedidoId { get; set; }
		[Required]
		public int StatusPedidoId { get; set; }
		public IEnumerable<SelectListItem>? PedidoStatusList { get; set; }
	}
}
