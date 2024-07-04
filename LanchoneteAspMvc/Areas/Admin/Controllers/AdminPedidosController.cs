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
using LanchoneteAspMvc.Areas.Admin.Repositories;
using ReflectionIT.Mvc.Paging;

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdminPedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var pedidos = await _pedidoRepository.GetAll();
        //    return View(pedidos);
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {

            var resultado = _pedidoRepository.RetornaContext().Pedidos.AsNoTracking().AsQueryable();
            if(!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _pedidoRepository.Get(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {

                _pedidoRepository.CriarPedido(pedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _pedidoRepository.Get(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Sobrenome,Endereco,Estado,Cidade,Telefone,Email,PedidoTotal,TotalItensPedido,PedidoEnviado,PedidoEntregueEm,Id")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pedidoRepository.Put(pedido);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await PedidoExists(pedido.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _pedidoRepository.Get(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            if (pedido != null)
            {
                await _pedidoRepository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PedidoExists(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);
            return pedido == null ? true : false;
        }
    }
}
