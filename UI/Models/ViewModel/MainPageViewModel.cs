using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Concrete;

namespace UI.Models.ViewModel
{
    public class MainPageViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}