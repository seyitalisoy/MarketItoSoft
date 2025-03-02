using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;
using UI.Helpers;
using UI.Models;
using UI.Models.ViewModel;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, ICustomerService customerService, IOrderService orderService)
        {
            _productService = productService;
            _customerService = customerService;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            var cart = SessionHelper.GetCart(HttpContext);

            // Sepet toplam fiyatını hesapla
            decimal totalPrice = cart.Items.Sum(item => item.Price * item.Quantity);

            // ViewModel oluştur ve verileri geç
            var model = new CartViewModel
            {
                CartItems = cart.Items,
                TotalPrice = totalPrice
            };

            return View(model);
        }

        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.GetById(productId);
            if (product != null)
            {
                var cart = SessionHelper.GetCart(HttpContext);
                cart.AddItem(product, quantity);
                SessionHelper.SetCart(HttpContext, cart);
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            var cart = SessionHelper.GetCart(HttpContext);
            cart.RemoveItem(productId);
            SessionHelper.SetCart(HttpContext, cart);
            return RedirectToAction("Index");
        }



        // Checkout formunu göster
        public ActionResult Checkout()
        {
            return View(new CheckoutViewModel());
        }

        // Sipariş tamamlanacak
        [HttpPost]
        public ActionResult CompleteOrder(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Kullanıcıyı Customers tablosuna kaydet
                var customer = new Customer
                {
                    TC = model.TC,
                    Name = model.Name,
                    Phone = model.Phone
                };

                _customerService.Add(customer);

                // 2. Sepeti al
                var cart = SessionHelper.GetCart(HttpContext);
                if (cart.Items.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                int orderNumber = Math.Abs(Guid.NewGuid().GetHashCode());

                foreach (var item in cart.Items)
                {
                    var product = _productService.GetById(item.ProductId);
                    product.Stock -= item.Quantity;
                    _productService.Update(product);

                    var order = new Order
                    {
                        OrderId = orderNumber,
                        ProductId = item.ProductId,
                        ProductAmount = item.Quantity,
                        Price = item.Price,
                        CustomerId = customer.TC,
                        Adress = model.Address,
                        OrderDate = DateTime.Now
                    };

                    _orderService.Add(order);
                }

                // 4. Sepeti sıfırla
                SessionHelper.SetCart(HttpContext, new Cart());

                return RedirectToAction("OrderCompleted");
            }

            return View("Checkout", model);
        }

        public ActionResult OrderCompleted()
        {
            return View();
        }
    }


}