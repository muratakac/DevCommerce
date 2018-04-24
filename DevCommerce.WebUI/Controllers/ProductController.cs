using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevCommerce.WebUI.Controllers
{
    public class ProductController : ClientBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ProductId)
        {
            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel();
            string stringData = ClientBaseController.ServiceGetData($"/api/Product/{ProductId}", RequestType.GET, null, true);
            productDetailViewModel.Product = JsonConvert.DeserializeObject<Product>(stringData);
            return View(productDetailViewModel);
        }
    }
}