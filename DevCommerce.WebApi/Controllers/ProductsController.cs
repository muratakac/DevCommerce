using DevCommerce.Business.Abstract;
using DevCommerce.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevCommerce.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductsController : Controller
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize()]
        [HttpGet]
        public List<Product> GetProducts()
        {
            return _productService.GetAll();
        }

        [Authorize()]
        [HttpGet("{productId}")]
        public Product GetProductByProductId(int productId)
        {
            return _productService.GetById(productId);
        }
    }
}