using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        // DI : Dependency Injection
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var result = _productService.GetAll();
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(result);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetAll();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View("Index", _productService.GetAll());
            }

            _productService.Add(product);
            TempData["AddMessage"] = "Ürün Eklenmiştir";

            return RedirectToAction("Index");
        }

        public ActionResult RemoveProduct(int id)
        {
            var product = _productService.GetById(id);

            _productService.Delete(product);
            TempData["RemoveMessage"] = "Ürün Silinmiştir";

            return RedirectToAction("Index");
        }
    }
}