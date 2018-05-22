using DevCommerce.Entities;
using System.Collections.Generic;

namespace DevCommerce.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        //Order Insert(Order order);
        //int Update(Order order);
    }
}
