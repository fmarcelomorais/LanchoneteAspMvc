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

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriasController : Controller
    {
        private readonly ICategoriaRepository _context;

        public AdminCategoriasController(ICategoriaRepository context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategorias
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.GetAll();
            return View(categorias);
        }

        // GET: Admin/AdminCategorias/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.Id = Guid.NewGuid();
                _context.Post(categoria);
               
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Put(categoria);
     
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Admin/AdminCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoria = await _context.Get(id);
            if (categoria != null)
            {
                _context.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(Guid id)
        {
            return _context.IfExists(c => c.Id == id);
           
        }
    }
}
