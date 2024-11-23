using System.Runtime.InteropServices;

namespace PF.Controllers
{
	[Authorize(Roles = nameof(Roles.Admin))]
	public class EstoqueController : Controller
	{
		private readonly IEstoqueRepositorio _estoqueRepo;
		public EstoqueController(IEstoqueRepositorio estoqueRepo)
		{
			_estoqueRepo = estoqueRepo;
		}
		public async Task<IActionResult> Index(string sTerm = "")
		{
			var estoques = await _estoqueRepo.GetEstoques(sTerm);
			return View(estoques);
		}

		public async Task<IActionResult> ManejarEstoque(int produtoId)
		{
			var estoqueExiste = await _estoqueRepo.GetEstoqueByProdutoId(produtoId);
			var estoque = new EstoqueDataTransferObj
			{
				ProdutoId = produtoId,
				Estoque = estoqueExiste != null ?  estoqueExiste.QuantidadeEstoque : 0 
			};
			return View(estoque);
		}

		[HttpPost]
		public async Task<IActionResult> GerenciarEstoque(EstoqueDataTransferObj estoqueQtd)
		{
		
			if (!ModelState.IsValid)
			{
				return View(estoqueQtd);
			}
			try
			{
				await _estoqueRepo.GerenciarEstoque(estoqueQtd);
				TempData["successMessage"] = "Estoque atualizado com sucesso";
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = "Algo deu errado";
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
