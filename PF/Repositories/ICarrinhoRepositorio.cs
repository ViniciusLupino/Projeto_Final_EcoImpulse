namespace PF.Repositories
{
    public interface ICarrinhoRepositorio
    {
        Task<int> AddItem(int produtoId, int qtd);
        Task<int> RemoverItem(int itemId);
        Task<Carrinho> GetUserCarrinho();
        Task<int> GetCarrinhoItemCount(string userId = "");
        Task<bool> Finalizar();
    }
}