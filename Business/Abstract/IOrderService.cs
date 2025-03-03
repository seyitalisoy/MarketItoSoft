using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        void Add(Order entity);

        List<OrderListDto> GetOrderDetails();
    }
}
