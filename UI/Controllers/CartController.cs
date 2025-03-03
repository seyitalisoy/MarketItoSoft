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
        [HttpPost]
        public JsonResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.GetById(productId);
            if (product != null)
            {
                var cart = SessionHelper.GetCart(HttpContext);
                cart.AddItem(product, quantity);
                SessionHelper.SetCart(HttpContext, cart);

                // Sepet miktarını al
                int cartItemCount = cart.Items.Sum(i => i.Quantity);

                // Success response ile birlikte sepetin toplam ürün miktarını döndür
                return Json(new { success = true, message = "Ürün sepete eklendi!", cartItemCount = cartItemCount });
            }

            return Json(new { success = false, message = "Ürün bulunamadı!" });
        }

        public JsonResult GetCartItemCount()
        {
            var cart = SessionHelper.GetCart(HttpContext);
            int cartItemCount = cart.Items.Sum(i => i.Quantity);

            // Sepetin toplam miktarını döndür
            return Json(new { cartItemCount = cartItemCount }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult AddToCart(int productId, int quantity = 1)
        //{
        //    var product = _productService.GetById(productId);
        //    if (product != null)
        //    {
        //        var cart = SessionHelper.GetCart(HttpContext);
        //        cart.AddItem(product, quantity);
        //        SessionHelper.SetCart(HttpContext, cart);
        //    }
        //    return RedirectToAction("Index");
        //}

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
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.Phone
                };

                _customerService.Add(customer);
                var customerId = _customerService.GetAll()
                 .OrderByDescending(c => c.Id)
                 .Select(c => c.Id)
                 .FirstOrDefault(); 


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
                        CustomerId = customerId,
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

        public ActionResult IncreaseQuantity(int productId)
        {
            var cart = SessionHelper.GetCart(HttpContext); // Get the cart from the session
            var cartItem = cart.Items.FirstOrDefault(c => c.ProductId == productId); // Find the product in the cart

            if (cartItem != null)
            {
                cartItem.Quantity++; // Increase the quantity by 1
            }

            SessionHelper.SetCart(HttpContext, cart); // Save the updated cart back to the session
            return RedirectToAction("Index"); // Redirect to the cart page
        }

        public ActionResult DecreaseQuantity(int productId)
        {
            var cart = SessionHelper.GetCart(HttpContext); // Get the cart from the session
            var cartItem = cart.Items.FirstOrDefault(c => c.ProductId == productId); // Find the product in the cart

            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--; // Decrease the quantity by 1
            }

            SessionHelper.SetCart(HttpContext, cart); // Save the updated cart back to the session
            return RedirectToAction("Index"); // Redirect to the cart page
        }
    }


}