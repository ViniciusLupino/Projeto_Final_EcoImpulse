using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using System.Drawing.Text;
using System.Net.Quic;

namespace PF.Repositories
{
    public class CarrinhoRepositorio : ICarrinhoRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrinhoRepositorio(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int produtoId, int qtd)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("Usuário não está logado!");

                var cart = await GetCarrinho(userId);
                if (cart is null)
                {
                    cart = new Carrinho
                    {
                        UserId = userId
                    };
                    _db.Carrinhos.Add(cart);
                }
                _db.SaveChanges();

                var cartItem = _db.DetalheCarrinhos
                                  .FirstOrDefault(a => a.CarrinhoId == cart.IdCarrinho && a.ProdutoId == produtoId);
                if (cartItem is not null)
                {
                    cartItem.Quantidade += qtd;
                }
                else
                {
                    var produto = _db.Produtos.Find(produtoId);
                    cartItem = new DetalheCarrinho
                    {
                        ProdutoId = produtoId,
                        CarrinhoId = cart.IdCarrinho,
                        Quantidade = qtd,
                        PrecoUnitario = produto.ProdutoPreco
                    };
                    _db.DetalheCarrinhos.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {

            }
            var totalItems = await GetCarrinhoItemCount(userId);
            return totalItems;
        }

        public async Task<int> RemoverItem(int itemId)
        {
            string userId = GetUserId();

            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("Usuário não está logado!");

                var cart = await GetCarrinho(userId);
                if (cart is null)
                {
                    throw new Exception("Carrinho inválido!");
                }

                var cartItem = _db.DetalheCarrinhos
                                .FirstOrDefault(a => a.CarrinhoId == cart.IdCarrinho && a.ProdutoId == itemId);
                if (cartItem is null)
                {
                    throw new Exception("Carrinho vazio!");
                }
                else if (cartItem.Quantidade == 1)
                {
                    _db.DetalheCarrinhos.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantidade = cartItem.Quantidade - 1;
                }
                _db.SaveChanges();
                //transaction.Commit();

            }
            catch (Exception ex)
            {

            }
            var totalItems = await GetCarrinhoItemCount(userId);
            return totalItems;
        }

        public async Task<Carrinho> GetUserCarrinho()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Identificador de usuário inválido!");
            var carrinhoDeCompras = await _db.Carrinhos
                .Include(a => a.CarrinhoDetalhes)
                .ThenInclude(a => a.Produto)
                .ThenInclude(a => a.Categoria)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return carrinhoDeCompras;
        }

        private async Task<Carrinho> GetCarrinho(string userId)
        {
            var cart = await _db.Carrinhos.FirstOrDefaultAsync(c => c.UserId == userId);
            return cart;
        }

        public async Task<int> GetCarrinhoItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from cart in _db.Carrinhos
                              join cartDetail in _db.DetalheCarrinhos
                              on cart.IdCarrinho equals cartDetail.CarrinhoId
                              where cart.UserId == userId
                              select new { cartDetail.IdDetalheCarrinho }).ToListAsync();
            return data.Count;


        }

        public async Task<bool> Finalizar(FinalModel finalModel)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                // Mover dados do DetalheCarrinho para Pedido e DetalhePedido, então remover depois o Detalhe do Carrinho
                // Entry -> Pedido, detalhe do pedido
                // Remove data -> Detalhe do carrinho
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("Usuário não está logado");
                }
                var carrinho = await GetCarrinho(userId);
                if (carrinho is null)
                {
                    throw new Exception("Carrinho inválido");
                }
                var detalheCarrinho = _db.DetalheCarrinhos.Where(a => a.CarrinhoId == carrinho.IdCarrinho).ToList();
                if (detalheCarrinho.Count == 0)
                {
                    throw new Exception("Não há itens no carrinho");
                }
                var gravacaoPendente = _db.StatusPedidos.FirstOrDefault(s => s.StatusNome == "Pendente");
                if(gravacaoPendente is null)
                {
                    throw new Exception("O pedido não possui o status Pendente");
                }
                var pedido = new Pedido
                {
                    UserId = userId,
                    Data = DateTime.UtcNow,
                    NomeRemetente = finalModel.NomeRemetente,
                    EmailRemetente = finalModel.EmailRemetente,
                    TelefoneRemetente = finalModel.TelefoneRemetente,
                    MetodoDePagamento = finalModel.MetodoDePagamento,
                    EnderecoRemetente = finalModel.EnderecoRemetente,
                    EstaPago = false,
                    StatusPedidoId = gravacaoPendente.StatusId
                };
                _db.Pedidos.Add(pedido);
                _db.SaveChanges();
                foreach (var item in detalheCarrinho)
                {
                    var detalhePedido = new DetalhePedido
                    {
                        ProdutoId = item.ProdutoId,
                        PedidoId = pedido.IdPedido,
                        Quantidade = item.Quantidade,
                        PrecoUnitario = item.PrecoUnitario
                    };
                    _db.DetalhesPedidos.Add(detalhePedido);
                }
                _db.SaveChanges();

                _db.DetalheCarrinhos.RemoveRange(detalheCarrinho);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

    }
}
