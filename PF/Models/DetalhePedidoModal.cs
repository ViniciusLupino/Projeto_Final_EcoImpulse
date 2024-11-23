namespace PF.Models
{
	public class DetalhePedidoModal
	{
		public string IdDiv { get; set; }
		public IEnumerable<DetalhePedido> DetalhePedidos { get; set; }
	}
}
