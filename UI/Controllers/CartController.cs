using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<Product> ?? new List<Product>();
            return View(cart);
        }
        public ActionResult AddToCart(int id)
        {
            var product = _productService.GetById(id);

            if (product != null)
            {
                var cart = Session["Cart"] as List<Product> ?? new List<Product>();
                cart.Add(product);
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }
    }
}