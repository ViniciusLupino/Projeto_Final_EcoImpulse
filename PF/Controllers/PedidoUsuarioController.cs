using Microsoft.AspNetCore.Mvc;

namespace PF.Controllers
{
    [Authorize]
    public class PedidoUsuarioController : Controller
    {
        private readonly IPedidoUsuarioRepositorio _pedidoUsuarioRepo;
        public PedidoUsuarioController(IPedidoUsuarioRepositorio pedidoUsuarioRepo)
        {
            _pedidoUsuarioRepo = pedidoUsuarioRepo;
        }
        public async Task<IActionResult> PedidosUsuarios()
        {
            var pedidos = await _pedidoUsuarioRepo.PedidosUsuarios();
            return View(pedidos);
        }
    }
}
