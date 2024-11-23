namespace PF.Repositories
{
    public interface IPedidoUsuarioRepositorio
    {
        Task<IEnumerable<Pedido>> PedidosUsuarios(bool getAll = false);
        Task MudarStatusPedido(AtuallizarPedidoModel atualizarPedidoModel);
        Task MudarStatusDePagamento(int pedidoId);
        Task<Pedido>? GetPedidoById(int id);
        Task<IEnumerable<StatusPedido>> GetPedidosStatus();
    }
}