namespace PF.Controllers
{
    [Authorize]                                              
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoRepositorio _carrinhoRepo;

        public CarrinhoController(ICarrinhoRepositorio carrinhoRepo)
        {
            _carrinhoRepo = carrinhoRepo;
        }
        public async Task<IActionResult> AddItem(int produtoId, int qtd = 1, int redirect = 0)
        {
            var contagemCarrinho = await _carrinhoRepo.AddItem(produtoId, qtd);
            if (redirect == 0)
            {
                return Ok(contagemCarrinho);
            }
            return RedirectToAction("GetUserCarrinho");

        }
        public async Task<IActionResult> RemoverItem(int itemId)
        {
            var contagemCarrinho = await _carrinhoRepo.RemoverItem(itemId);
            return RedirectToAction("GetUserCarrinho");
        }
        public async Task<IActionResult> GetUserCarrinho()
        {
            var carrinho = await _carrinhoRepo.GetUserCarrinho();
            return View(carrinho);
        }
        public async Task<IActionResult> GetTotalItensCarrinho()
        {
            int carrinhoItem = await _carrinhoRepo.GetCarrinhoItemCount();
            return Ok(carrinhoItem);
        }
        public IActionResult Final()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Final(FinalModel finalModel)
        {
            if (!ModelState.IsValid)
            {
                return View(finalModel);
            }
            bool estaFinalizado = await _carrinhoRepo.Finalizar(finalModel);
            if (!estaFinalizado)
            {
                return RedirectToAction(nameof(Falha));
            }
            return RedirectToAction(nameof(Sucesso));
        }

		public IActionResult Sucesso()
		{
			return View();
		}

		public IActionResult Falha()
		{
			return View();
		}
	}
}
