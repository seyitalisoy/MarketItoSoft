using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models;

namespace UI.Helpers
{

    public static class SessionHelper
    {
        public static void SetCart(HttpContextBase context, Cart cart)
        {
            context.Session["Cart"] = cart;
        }

        public static Cart GetCart(HttpContextBase context)
        {
            return context.Session["Cart"] as Cart ?? new Cart();
        }
    }

}