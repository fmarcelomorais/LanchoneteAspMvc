using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.Data.Interfaces;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly ILancheRepository _context;
        private readonly ConfigurationImage _configureImage;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminLanchesController(ILancheRepository context, IOptions<ConfigurationImage> configureImage, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configureImage = configureImage.Value;
            _webHostEnvironment = webHostEnvironment;
        }

       
        //public async Task<IActionResult> Index()
        //{
        //    var lanches = await _context.GetAll();
        //    return View(lanches);
        //}

       
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var lanches =  _context.RetornaContext().Lanches.Include(l => l.Categoria).AsNoTracking().AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lanches = lanches.Where(l => l.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(lanches, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Get(id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // GET: Admin/AdminLanches/Create
        public async Task<IActionResult> Create()
        {
            var lista = _context.RetornaContext().Categorias.ToList();
            
            ViewData["CategoriaId"] = new SelectList(lista, "Id", "Nome");

            FileManagerModel model = new FileManagerModel();
            var userImagePath = Path.Combine(_webHostEnvironment.WebRootPath, _configureImage.NomePastaImagensProduto);

            DirectoryInfo dir = new DirectoryInfo(userImagePath);
            FileInfo[] files = dir.GetFiles();

            model.PathImageProdutos = _configureImage.NomePastaImagensProduto;
            if (files.Length == 0)
            {
                ViewData["Error"] = $"Nenhum arquivo encontrado em {userImagePath}";
            }

            model.Files = files;

            List<string> caminhoImagem = new List<string>();

            foreach (var file in files)
            {
                caminhoImagem.Add(file.FullName);
            }

            ViewData["Lista"] = new SelectList(files, "FullName", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Preco,DescricaoCurta,DescricaoLonga,ImagemUrl,ThumbnailUrl,Preferido,Disponivel,CategoriaId,Id")] Lanche lanche)
        {

            if (ModelState.IsValid)
            {
                lanche.Id = Guid.NewGuid();
                await _context.Post(lanche);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList( _context.ReturaLista(), "Id", "Nome", lanche.CategoriaId);
            return View(lanche);
        }

        // GET: Admin/AdminLanches/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Get(id);

            if (lanche == null)
            {
                return NotFound();
            }
            var lista = new SelectList( _context.ReturaLista(), "Id", "Nome", lanche.CategoriaId);
            ViewBag.CategoriaId = lista;

            return View(lanche);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Preco,DescricaoCurta,DescricaoLonga,ImagemUrl,ThumbnailUrl,Preferido,Disponivel,CategoriaId,Id")] Lanche lanche)
        {
            if (id != lanche.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Put(lanche);
  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheExists(lanche.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.ReturaLista(), "Id", "Descricao", lanche.CategoriaId);
            return View(lanche);
        }

        // GET: Admin/AdminLanches/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Get(id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lanche = await _context.Get(id);
            if (lanche != null)
            {
                _context.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(Guid id)
        {
            return _context.IfExists(l => l.Id == id);
        }
    }
}
