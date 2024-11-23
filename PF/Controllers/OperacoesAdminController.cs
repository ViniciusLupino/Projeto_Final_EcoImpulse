using Microsoft.AspNetCore.Mvc.Rendering;

namespace PF.Controllers;
[Authorize(Roles = nameof(Roles.Admin))]
public class OperacoesAdminController : Controller
{
	private readonly IPedidoUsuarioRepositorio _pedidoUsuarioRepositorio;
	public OperacoesAdminController(IPedidoUsuarioRepositorio pedidoUsuarioRepositorio)
	{
		_pedidoUsuarioRepositorio = pedidoUsuarioRepositorio;
	}
	public async Task<IActionResult> TodosOsPedidos()
	{
		var pedidos = await _pedidoUsuarioRepositorio.PedidosUsuarios(true);
		return View(pedidos);
	}

	public async Task<IActionResult> MudarStatusPagamento(int pedidoId)
	{
		try
		{
			await _pedidoUsuarioRepositorio.MudarStatusDePagamento(pedidoId);
		}
		catch (Exception ex)
		{

		}
		return RedirectToAction(nameof(TodosOsPedidos));
	}

	public async Task<IActionResult> UpdateStatusPagamento(int pedidoId)
	{
		var pedido = await _pedidoUsuarioRepositorio.GetPedidoById(pedidoId);
		if (pedido == null)
		{
			throw new InvalidOperationException($"Pedido de ID: {pedidoId} não pôde ser encontrado");
		}
		var pedidoStatusLista = (await _pedidoUsuarioRepositorio.GetPedidosStatus()).Select(pedidoStatus =>
		{
			return new SelectListItem
			{
				Value = pedidoStatus.StatusId.ToString(),
				Text = pedidoStatus.StatusNome,
				Selected = pedido.StatusPedidoId == pedidoStatus.StatusId
			};
		});
		var data = new AtuallizarPedidoModel
		{
			PedidoId = pedidoId,
			StatusPedidoId = pedido.StatusPedidoId,
			PedidoStatusList = pedidoStatusLista
		};
		return View(data);
	}

	[HttpPost]
	public async Task<IActionResult> UpdateStatusPagamento(AtuallizarPedidoModel data)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				data.PedidoStatusList = (await _pedidoUsuarioRepositorio.GetPedidosStatus()).Select(pedidoStatus =>
				{
					return new SelectListItem
					{
						Value = pedidoStatus.StatusId.ToString(),
						Text = pedidoStatus.StatusNome,
						Selected = pedidoStatus.StatusId == data.StatusPedidoId
					};
				});
				return View(data);
			}
			await _pedidoUsuarioRepositorio.MudarStatusPedido(data);
			TempData["msg"] = "Pedido atualizado com sucesso";
		}   
		catch(Exception ex)
		{
			TempData["msg"] = "Algo deu errado";
		}
		return RedirectToAction(nameof(UpdateStatusPagamento), new {pedidoId = data.PedidoId});
	}
}
