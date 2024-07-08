using LanchoneteAspMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImage _configureImage;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagensController(IOptions<ConfigurationImage> configureImage, IWebHostEnvironment webHostEnvironment)
        {
            _configureImage = configureImage.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }
            if(files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivo excedeu o limite.";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);
            var filePathName = new List<string>();

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, _configureImage.NomePastaImagensProduto);

            foreach(var file in files)
            {
                if(VerificaTipoImagemValida(file))
                {
                    var arquivoECaminho = string.Concat(filePath, "\\", file.FileName);
                    filePathName.Add(arquivoECaminho);

                    using(var stream = new FileStream(arquivoECaminho, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); 
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} imagem(ns) foram enviada(s) ao servidor.\nTamanho total: {size} bytes ";

            ViewBag.Arquivos = filePathName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();
            var userImagePath = Path.Combine(_webHostEnvironment.WebRootPath, _configureImage.NomePastaImagensProduto);

            DirectoryInfo dir = new DirectoryInfo(userImagePath);
            FileInfo[] files = dir.GetFiles();

            model.PathImageProdutos = _configureImage.NomePastaImagensProduto;
            if(files.Length == 0)
            {
                ViewData["Error"] = $"Nenhum arquivo encontrado em {userImagePath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
            string _imageDeleta = Path.Combine(_webHostEnvironment.WebRootPath, _configureImage.NomePastaImagensProduto+"\\",  fname);
            if(System.IO.File.Exists(_imageDeleta)) 
            {
                System.IO.File.Delete(_imageDeleta);
                ViewData["Deletado"] = $"Arquivo {_imageDeleta} deletado com sucesso.";
            }

            return View("Index");
        
        }

        private bool VerificaTipoImagemValida(IFormFile file)
        {
            
            if (file.FileName.Contains(".jpg")) return true;
            if (file.FileName.Contains(".jpeg")) return true;
            if (file.FileName.Contains(".png")) return true;
            if (file.FileName.Contains(".gif")) return true;
            return false;

            
        }
    }
}
