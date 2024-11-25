using Microsoft.AspNetCore.Mvc.Rendering;
using PF.Shared;

namespace PF.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]

public class ProdutoController : Controller
{
	private readonly IProdutoRepositorio _produtoRepo;
	private readonly ICategoriaRepositorio _categoriaRepo;
	private readonly IFileService _fileService;

	public ProdutoController(IProdutoRepositorio produtoRepo, ICategoriaRepositorio categoriaRepo, IFileService fileService)
	{
		_produtoRepo = produtoRepo;
		_categoriaRepo = categoriaRepo;
		_fileService = fileService;
	}

	public async Task<IActionResult> Index()
	{
		var produtos = await _produtoRepo.GetProdutos();
		return View(produtos);
	}

	public async Task<IActionResult> AddProduto()
	{
		var categoriaSelectList = (await _categoriaRepo.GetAllCategorias()).Select(categoria => new SelectListItem
		{
			Text = categoria.CategoriaNome,
			Value = categoria.IdCategoria.ToString()
		});
		ProdutoDataTransferObj produtoAdd = new()
		{
			CategoriaList = categoriaSelectList
		};
		return View(produtoAdd);
	}

	[HttpPost]
	public async Task<IActionResult> AddProduto(ProdutoDataTransferObj produtoAdd)
	{
		var categoriaSelectList = (await _categoriaRepo.GetAllCategorias()).Select(categoria => new SelectListItem
		{
			Text = categoria.CategoriaNome,
			Value = categoria.IdCategoria.ToString()
		});
		produtoAdd.CategoriaList = categoriaSelectList;
		if (!ModelState.IsValid)
		{
			return View(produtoAdd);
		}
		try
		{
			if (produtoAdd.ArquivoImagem != null)
			{
				if (produtoAdd.ArquivoImagem.Length > 1 * 1024 * 1024)
				{
					throw new InvalidOperationException("O tamanho do arquivo não pode exceder 1 MB");
				}
				string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
				string nomeImagem = await _fileService.SalvarArquivo(produtoAdd.ArquivoImagem, allowedExtensions);
				produtoAdd.Imagem = nomeImagem;
			}
			Produto produto = new()
			{
				IdProduto = produtoAdd.ProdutoId,
				ProdutoNome = produtoAdd.ProdutoNome,
				ProdutoDescricao = produtoAdd.ProdutoDescricao,
				Imagem = produtoAdd.Imagem,
				CategoriaId = produtoAdd.CategoriaId,
				ProdutoPreco = produtoAdd.Preco
			};
			await _produtoRepo.AddProduto(produto);
			TempData["successMessage"] = "Produto adiconado com sucesso";
			return RedirectToAction(nameof(Index));
		}
		catch (InvalidOperationException ex)
		{
			TempData["errorMessage"] = ex.Message;
			return View(produtoAdd);
		}
		catch (FileNotFoundException ex)
		{
			TempData["errorMessage"] = ex.Message;
			return View(produtoAdd);
		}
		catch (Exception ex)
		{
			TempData["errorMessage"] = "Produto não pôde ser adicionado";
			return View(produtoAdd);
		}
	}

	public async Task<IActionResult> UpdateProduto(int id)
	{
		var produto = await _produtoRepo.GetProdutoById(id);
		if (produto == null)
		{
			TempData["errorMessage"] = "Produto não encontrado";
			return RedirectToAction(nameof(Index));
		}
		var categoriaSelectList = (await _categoriaRepo.GetAllCategorias()).Select(categoria => new SelectListItem
		{
			Text = categoria.CategoriaNome,
			Value = categoria.IdCategoria.ToString(),
			Selected = categoria.IdCategoria == produto.CategoriaId
		});
		ProdutoDataTransferObj produtoUpdate = new()
		{
			ProdutoId = produto.IdProduto,
			CategoriaList = categoriaSelectList,
			ProdutoNome = produto.ProdutoNome,
			ProdutoDescricao = produto.ProdutoDescricao,
			CategoriaId = produto.CategoriaId,
			Preco = produto.ProdutoPreco,
			Imagem = produto.Imagem
		};
		return View(produtoUpdate);

	}

	[HttpPost]
	public async Task<IActionResult> UpdateProduto(ProdutoDataTransferObj produtoToUpdate)
	{
		var categoriaSelectList = (await _categoriaRepo.GetAllCategorias()).Select(categoria => new SelectListItem
		{
			Value = categoria.IdCategoria.ToString(),
			Text = categoria.CategoriaNome,
			Selected = categoria.IdCategoria == produtoToUpdate.CategoriaId
		});
		produtoToUpdate.CategoriaList = categoriaSelectList;
		if (!ModelState.IsValid)
		{
			return View(produtoToUpdate);
		}
		try
		{
			string imagemAntiga = "";
			if (produtoToUpdate.ArquivoImagem != null)
			{
				if (produtoToUpdate.ArquivoImagem.Length > 1 * 1024 * 1024)
				{
					throw new InvalidOperationException("A imagem não pode exceder 1 MB");
				}
				string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
				string nomeImagem = await _fileService.SalvarArquivo(produtoToUpdate.ArquivoImagem, allowedExtensions);
				imagemAntiga = produtoToUpdate.Imagem;
				produtoToUpdate.Imagem = nomeImagem;
			}

			Produto produto = new()
			{
				IdProduto = produtoToUpdate.ProdutoId,
				ProdutoNome = produtoToUpdate.ProdutoNome,
				ProdutoDescricao = produtoToUpdate.ProdutoDescricao,
				CategoriaId = produtoToUpdate.CategoriaId,
				ProdutoPreco = produtoToUpdate.Preco,
				Imagem = produtoToUpdate.Imagem
			};

			await _produtoRepo.UpdateProduto(produto);
		
			if(!string.IsNullOrEmpty(imagemAntiga))
			{
				_fileService.DeleteArquivo(imagemAntiga);
			}
			TempData["successMessage"] = "Produto atualizado com sucesso";
			return RedirectToAction(nameof(Index));

		}
		catch(InvalidOperationException ex)
		{
			TempData["errorMessage"] = ex.Message;
			return View(produtoToUpdate);
		}
		catch (FileNotFoundException ex)
		{
			TempData["errorMessage"] = ex.Message;
			return View(produtoToUpdate);
		}
		catch (Exception ex)
		{
			TempData["errorMessage"] = "Produto não pôde ser atualizado";
			return View(produtoToUpdate);
		}
	}

    public async Task<IActionResult> DeleteProduto(int id)
    {
        try
        {
            var produto = await _produtoRepo.GetProdutoById(id);
            if (produto == null)
            {
                TempData["errorMessage"] = $"produto with the id: {id} does not found";
            }
            else
            {
                await _produtoRepo.DeleteProduto(produto);
                if (!string.IsNullOrWhiteSpace(produto.Imagem))
                {
                    _fileService.DeleteArquivo(produto.Imagem);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }
}
