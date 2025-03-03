using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Add(Order entity)
        {
            _orderDal.Add(entity);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetAll();
        }

        public List<OrderListDto> GetOrderDetails()
        {
            return _orderDal.GetOrderDetails();
        }
    }
}
