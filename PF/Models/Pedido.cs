﻿using System.ComponentModel.DataAnnotations;

namespace PF.Models
{
	public class Pedido
	{
		[Key]
		public int IdPedido { get; set; }
		[Required]
		public string UserId { get; set; }
		public DateTime Data { get; set; } = DateTime.UtcNow;
		[Required]
		public int StatusPedidoId { get; set; }
		public StatusPedido StatusPedido { get; set; }

		public bool Deletado { get; set; } = false;
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
		public bool EstaPago { get; set; }
		public List<DetalhePedido> DetalhesPedidos { get; set; }

	}
}
