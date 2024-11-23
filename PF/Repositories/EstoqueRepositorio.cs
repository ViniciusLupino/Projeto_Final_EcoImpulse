using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace PF.Repositories
{
	public class EstoqueRepositorio : IEstoqueRepositorio
	{
		private readonly ApplicationDbContext _context;
		public EstoqueRepositorio(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Estoque?> GetEstoqueByProdutoId(int produtoId) => await _context.Estoques.FirstOrDefaultAsync(s => s.ProdutoId == produtoId);

		public async Task GerenciarEstoque(EstoqueDataTransferObj estoqueData)
		{
			var estoqueExiste = await GetEstoqueByProdutoId(estoqueData.ProdutoId);
			if (estoqueExiste is null)
			{
				var estoque = new Estoque
				{
					ProdutoId = estoqueData.ProdutoId,
					QuantidadeEstoque = estoqueData.Estoque
				};
				_context.Add(estoque);
			}
			else
			{
				estoqueExiste.QuantidadeEstoque = estoqueData.Estoque;
			}
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<EstoqueDisplayModel>> GetEstoques(string sTerm = "")
		{
			var estoques = await (from produto in _context.Produtos
								  join estoque in _context.Estoques
								  on produto.IdProduto equals estoque.ProdutoId
								  into produto_estoque
								  from produtoEstoque in produto_estoque.DefaultIfEmpty()
								  where string.IsNullOrWhiteSpace(sTerm) ||
								  produto.ProdutoNome.ToLower().Contains(sTerm.ToLower())
								  select new EstoqueDisplayModel
								  {
									  ProdutoId = produto.IdProduto,
									  ProdutoNome = produto.ProdutoNome,
									  Quantidade = produtoEstoque == null ? 0 : produtoEstoque.QuantidadeEstoque
								  }).ToListAsync();
			return estoques;


		}

	}

	public interface IEstoqueRepositorio
	{
		
			Task<IEnumerable<EstoqueDisplayModel>> GetEstoques(string sTerm = "");
			Task<Estoque?> GetEstoqueByProdutoId(int produtoId);
			Task GerenciarEstoque(EstoqueDataTransferObj estoqueData);
		
	}
}
