using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;

namespace UI.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            
            return View(_orderService.GetOrderDetails().ToList());
        }
    }
}