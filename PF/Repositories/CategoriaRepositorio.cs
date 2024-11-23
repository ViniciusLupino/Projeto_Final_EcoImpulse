using System.Data.Entity;

namespace PF.Repositories
{
	public interface ICategoriaRepositorio
	{
		Task AddCategoria(Categoria categoria);
		Task UpdateCategoria(Categoria categoria);
		Task<Categoria?> GetCategoriaById(int id);
		Task DeleteCategoria(Categoria categoria);
		Task<IEnumerable<Categoria>> GetAllCategorias();

	}
	public class CategoriaRepositorio : ICategoriaRepositorio
	{
		private readonly ApplicationDbContext _context;
		public CategoriaRepositorio(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddCategoria(Categoria categoria)
		{
			_context.Categorias.Add(categoria);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCategoria(Categoria categoria)
		{
			_context.Categorias.Update(categoria);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCategoria(Categoria categoria)
		{
			_context.Categorias.Remove(categoria);
			await _context.SaveChangesAsync();
		}

		public async Task<Categoria?> GetCategoriaById(int id)
		{
			return await _context.Categorias.FindAsync(id);
		}

		public async Task<IEnumerable<Categoria>> GetAllCategorias()
		{
			return _context.Categorias.ToList();
		}
	}
}
