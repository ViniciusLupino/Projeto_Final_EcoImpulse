namespace PF.Repositories
{
	public interface IProdutoRepositorio
	{
		Task AddProduto(Produto produto);
		Task DeleteProduto(Produto produto);
		Task<Produto?> GetProdutoById(int id);
		Task<IEnumerable<Produto>> GetProdutos();
		Task UpdateProduto(Produto produto);
	}
	public class ProdutoRepositorio : IProdutoRepositorio
	{
		private readonly ApplicationDbContext _context;

        public ProdutoRepositorio(ApplicationDbContext context)
        {
			_context = context;
        }

        public async Task AddProduto(Produto produto)
		{
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProduto(Produto produto)
		{
			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();
		}

		public async Task<Produto?> GetProdutoById(int id) => await _context.Produtos.FindAsync(id);

		public async Task<IEnumerable<Produto>> GetProdutos() => await _context.Produtos.Include(a => a.Categoria).ToListAsync();

		public async Task UpdateProduto(Produto produto)
		{
			_context.Produtos.Update(produto);
			await _context.SaveChangesAsync();
		}
	}
}
