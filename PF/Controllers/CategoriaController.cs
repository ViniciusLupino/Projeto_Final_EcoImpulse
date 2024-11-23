namespace PF.Controllers
{
	[Authorize(Roles = nameof(Roles.Admin))]
	public class CategoriaController : Controller
	{
		private readonly ICategoriaRepositorio _categoriaRepo;

		public CategoriaController(ICategoriaRepositorio categoriaRepo)
		{
			_categoriaRepo = categoriaRepo;
		}

		public async Task<IActionResult> Index()
		{
			var categorias = await _categoriaRepo.GetAllCategorias();
			return View(categorias);
		}

		public IActionResult AddCategoria()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddCategoria(CategoriaDataTransferObj categoriaDataTransferObj)
		{
			if (!ModelState.IsValid)
			{
				return View(categoriaDataTransferObj);
			}
			try
			{
				var categoriaAdd = new Categoria
				{
					IdCategoria = categoriaDataTransferObj.IdCategoria,
					CategoriaNome = categoriaDataTransferObj.CategoriaNome
				};
				await _categoriaRepo.AddCategoria(categoriaAdd);
				TempData["successMessage"] = "Categoria adicionada com sucesso";
				return RedirectToAction(nameof(AddCategoria));
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = "Algo deu errado";
				return View(categoriaDataTransferObj);
			}
		}

		public async Task<IActionResult> UpdateCategoria(int id)
		{
			var categoria = await _categoriaRepo.GetCategoriaById(id);
			if (categoria is null)
			{
				throw new InvalidOperationException($"Categoria de id: {id} não pôde ser encontrada");
			};
			var categoriaUp = new CategoriaDataTransferObj
			{
				IdCategoria = categoria.IdCategoria,
				CategoriaNome = categoria.CategoriaNome
			};
			return View(categoriaUp);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCategoria(CategoriaDataTransferObj categoriaDataTransferObj)
		{
			if (!ModelState.IsValid)
			{
				return View(categoriaDataTransferObj);
			}
			try
			{
				var categoriaAdd = new Categoria
				{
					IdCategoria = categoriaDataTransferObj.IdCategoria,
					CategoriaNome = categoriaDataTransferObj.CategoriaNome
				};
				await _categoriaRepo.UpdateCategoria(categoriaAdd);
				TempData["successMessage"] = "Categoria adicionada com sucesso";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = "Algo deu errado";
				return View(categoriaDataTransferObj);
			}
		}

		public async Task<IActionResult> DeleteCategoria(int id)
		{
			var categoria = await _categoriaRepo.GetCategoriaById(id);
			if (categoria is null)
			{
				throw new InvalidOperationException($"Categoria de id: {id} não pôde ser encontrada");
			};
			await _categoriaRepo.DeleteCategoria(categoria);
			return RedirectToAction(nameof(Index));
		}
	}
}
