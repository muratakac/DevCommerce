using DevCommerce.Business.Abstract;
using DevCommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevCommerce.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            return _orderService.GetAll();
        }

        [HttpGet("{orderId}")]
        public Order GetOrderByOrderId(int orderId)
        {
            return _orderService.GetById(orderId);
        }
    }
}