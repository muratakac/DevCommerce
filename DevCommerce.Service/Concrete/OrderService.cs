using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAll()
        {
            return _orderRepository.All().ToList();
        }

        public Order GetById(int id)
        {
            return _orderRepository.Find(id);
        }

        public Order Insert(Order order)
        {
            return _orderRepository.Create(order);
        }

        public int Update(Order order)
        {
            return _orderRepository.Update(order);
        }
    }
}
