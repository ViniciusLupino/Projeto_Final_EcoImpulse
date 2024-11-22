namespace PF.Repositories
{
    public interface IPedidoUsuarioRepositorio
    {
        Task<IEnumerable<Pedido>> PedidosUsuarios();
    }
}