using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Models;
using DevCommerce.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using DevCommerce.WebUI.Extensions;

namespace DevCommerce.WebUI.Controllers
{
    public class ProductController : ClientAuthController
    {
        Basket<Product> _basket = null;
        public ProductController(Basket<Product> basket)
        {
            _basket = basket;
        }

        public IActionResult Index(int categoryId, int brandId)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.CategoryId = categoryId;
            productViewModel.BrandId = brandId;

            return View(productViewModel);
        }

        public IActionResult Detail(int ProductId)
        {
            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel();
            string stringData = ServiceGetData($"/api/Product/{ProductId}", RequestType.GET, null, true);
            productDetailViewModel.Product = JsonConvert.DeserializeObject<Product>(stringData);
            return View(productDetailViewModel);
        }

        public IActionResult Card()
        {
            CardViewModel cardViewModel = new CardViewModel();
            if (Request.Cookies["Basket"] != null)
            {
                _basket = JsonConvert.DeserializeObject<Basket<Product>>(Request.Cookies["Basket"]);
            }

            return View(cardViewModel);
        }

        //TODO => Key ifadeleri constant olarak tanımlanacak
        [HttpPost]
        public void AddBasket(int productId, int quantity)
        {
            string stringData = ServiceGetData($"/api/Product/{productId}", RequestType.GET, null, true);
            var product = JsonConvert.DeserializeObject<Product>(stringData);
            product.Quantity += quantity;

            _basket.AddBasket(product);

            Response.Cookies.AddCookie("Basket", JsonConvert.SerializeObject(_basket), DateTime.Now.AddMinutes(1));
        }

        public JsonResult GetBasketProductCount()
        {
            if (Request.Cookies["Basket"] != null)
            {
                _basket = JsonConvert.DeserializeObject<Basket<Product>>(Request.Cookies["Basket"]);
                return Json(_basket.Quantity);
            }

            return Json(0);
        }
    }
}