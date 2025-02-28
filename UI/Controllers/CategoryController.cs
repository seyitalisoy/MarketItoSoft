using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace UI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var result = _categoryService.GetAll();
            return View(result);
        }

        [HttpPost]
        public ActionResult Add(Category category)
        {
            var model = new Category { Name = category.Name, Description = category.Description,Picture=category.Picture };
            _categoryService.Add(model);
            ViewBag.SuccessMessage = "Kategori Eklenmiştir";
            return View("Index", _categoryService.GetAll());
        }
    }
}