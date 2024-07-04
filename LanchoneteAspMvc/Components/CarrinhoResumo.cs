using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Components
{
    public class CarrinhoResumo : ViewComponent
    {
        private readonly Carrinho _carrinho;

        public CarrinhoResumo(Carrinho carrinho)
        {
            _carrinho = carrinho;
        }

        public IViewComponentResult Invoke()
        {
            
            var itens = _carrinho.RetornaItemCarrinhoCompra();
            _carrinho.Itens = itens;
            var carrinhoViewModel = new CarrinhoViewModel
            {
                Carrinho = _carrinho,
                CarrinhoTotal = _carrinho.RetornaTotalCarrinho()
            };
            return View(carrinhoViewModel);
        }
    }
}
