using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderListDto
    {

        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Adress { get; set; }
    }
}
