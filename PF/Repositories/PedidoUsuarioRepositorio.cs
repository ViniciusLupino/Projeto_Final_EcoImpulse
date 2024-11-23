using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PF.Models;
using SQLitePCL;

namespace PF.Repositories
{
	public class PedidoUsuarioRepositorio : IPedidoUsuarioRepositorio
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PedidoUsuarioRepositorio(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Pedido>? GetPedidoById(int id)
		{
			return await _context.Pedidos.FindAsync(id);
		}

		public async Task<IEnumerable<StatusPedido>> GetPedidosStatus()
		{
			return await _context.StatusPedidos.ToListAsync();
		}

		public async Task MudarStatusDePagamento(int pedidoId)
		{
			var pedido = await _context.Pedidos.FindAsync(pedidoId);
			if(pedido == null)
			{
				throw new InvalidOperationException($"Pedido com ID: {pedidoId} não pôde ser encontrado");
			};
			pedido.EstaPago = !pedido.EstaPago;
			await _context.SaveChangesAsync();

		}

		public async Task MudarStatusPedido(AtuallizarPedidoModel atualizarPedidoModel)
		{
			var pedido = await _context.Pedidos.FindAsync(atualizarPedidoModel.PedidoId);
			if (pedido == null)
			{
				throw new InvalidOperationException($"Pedido com ID: {atualizarPedidoModel.PedidoId} não pôde ser encontrado");
			};
			pedido.StatusPedidoId = atualizarPedidoModel.StatusPedidoId;
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Pedido>> PedidosUsuarios(bool getAll = false)
		{
			var pedidos = _context.Pedidos
				.Include(x => x.StatusPedido)
				.Include(x => x.DetalhesPedidos)
				.ThenInclude(x => x.Produto)
				.ThenInclude(x => x.Categoria).AsQueryable();

			if (!getAll)
			{
				var userId = GetUserId();
				if (string.IsNullOrEmpty(userId))
				{
					throw new Exception("Usuário não está logado");
				}
				pedidos = pedidos.Where(a => a.UserId == userId);
				return await pedidos.ToListAsync();
			}
			return await pedidos.ToListAsync();

		}

		private string GetUserId()
		{
			var principal = _httpContextAccessor.HttpContext.User;
			string userId = _userManager.GetUserId(principal);
			return userId;
		}

	}
}
