using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly Carrinho _carrinho;

        public CarrinhoController(ILancheRepository lancheRepository, Carrinho carrinho)
        {
            _lancheRepository = lancheRepository;
            _carrinho = carrinho;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Carrinho";

            var itens = _carrinho.RetornaItemCarrinhoCompra();
            _carrinho.Itens = itens;
            var carrinhoViewModel = new CarrinhoViewModel
            {
                Carrinho = _carrinho,
                CarrinhoTotal = _carrinho.RetornaTotalCarrinho()
            };
            return View(carrinhoViewModel);
        }

        public async Task<IActionResult> AdicionarItem(Guid lancheId)
        {
            var lanche = await _lancheRepository.Get(lancheId);
            if (lanche != null) _carrinho.AdicionaItem(lanche);
            return  RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoverItem(Guid lancheId)
        {
            var lanche = await _lancheRepository.Get(lancheId);
            if (lanche != null) _carrinho.RemoveItem(lanche);
            return RedirectToAction("Index");
        }
    }
}
