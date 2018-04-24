using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Controllers;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DevCommerce.WebUI.ViewComponents
{
    public class ProductListViewComponent: ViewComponent
    {
        //Paging
        //SearchBrand
        //SearchCategories
        public ViewViewComponentResult Invoke()
        {
            ProductComponentModel productComponentModel = new ProductComponentModel();
            string stringData = ClientBaseController.ServiceGetData("/api/Product", RequestType.GET, null, true);
            productComponentModel.Products = JsonConvert.DeserializeObject<List<Product>>(stringData);
            return View(productComponentModel);
        }
    }
}
