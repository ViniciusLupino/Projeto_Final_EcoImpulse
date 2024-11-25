namespace PF.Shared
{
	public interface IFileService
	{
		void DeleteArquivo(string nomeArquivo);
		Task<string> SalvarArquivo(IFormFile file, string[] allowedExtensions);
	}
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
			_webHostEnvironment = webHostEnvironment;
        }

		public async Task<string> SalvarArquivo(IFormFile file, string[] allowedExtensions)
		{
			var wwwPath = _webHostEnvironment.WebRootPath;
			var path = Path.Combine(wwwPath, "images");
			if(!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var extension = Path.GetExtension(file.FileName);
			if(!allowedExtensions.Contains(extension))
			{
				throw new InvalidOperationException($"Apenas arquivos {string.Join(",", allowedExtensions)} são permitidos");
			}
			string fileName = $"{Guid.NewGuid()}{extension}";
			string fileNameWithPath = Path.Combine(path, fileName);
			using var stream = new FileStream(fileNameWithPath, FileMode.Create);
			await file.CopyToAsync(stream);
			return fileName;
		}

		public void DeleteArquivo(string nomeArquivo)
		{
			var wwwPath = _webHostEnvironment.WebRootPath;
			var fileNameWithPath = Path.Combine(wwwPath, "images\\", nomeArquivo);
			if (!File.Exists(fileNameWithPath))
			{
				throw new FileNotFoundException(nomeArquivo);
			}
			File.Delete(fileNameWithPath);
		}
	}
}
