using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Pedido>> PedidosUsuarios()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Usuário não está logado");
            }
            var pedidos = await _context.Pedidos
                .Include(x => x.StatusPedido)
                .Include(x => x.DetalhesPedidos)
                .ThenInclude(x => x.Produto)
                .ThenInclude(x => x.Categoria)
                .Where(a => a.UserId == userId).ToListAsync();

            return pedidos;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

    }
}
