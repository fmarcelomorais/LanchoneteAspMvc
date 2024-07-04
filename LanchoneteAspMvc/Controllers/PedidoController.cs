using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly Carrinho _carrinho;

        public PedidoController(IPedidoRepository pedidoRepository, Carrinho carrinho)
        {
            _pedidoRepository = pedidoRepository;
            _carrinho = carrinho;
        }
        [HttpGet]
        public IActionResult Checkout() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Pedido pedido) 
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            List<Item> itens = _carrinho.RetornaItemCarrinhoCompra();
            _carrinho.Itens = itens;

            if(_carrinho.Itens.Count < 1)
            {
                ModelState.AddModelError("", "Seu Carrinho está vazio!");
            }

            foreach(var item in itens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if(ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                ViewBag.CheckoutCompletoMessagem = "Pedido realizado com Sucesso ;)";
                ViewBag.TotalPedido = _carrinho.RetornaTotalCarrinho();

                _carrinho.LimpaCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);

        }
    }
}
