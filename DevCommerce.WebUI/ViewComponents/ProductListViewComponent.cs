using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Controllers;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.WebUI.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        //Paging
        public ViewViewComponentResult Invoke(int categoryId, int brandId)
        {
            ProductComponentModel productComponentModel = new ProductComponentModel();
            string stringData = ClientBaseController.ServiceGetData("/api/Product", RequestType.GET, null, true);
            IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(stringData);

            if (categoryId > 0)
            {
                products = products.Where(x => x.CategoryId == categoryId);
            }

            if (brandId > 0)
            {
                products = products.Where(x => x.BrandId == brandId);
            }

            productComponentModel.Products = products.ToList();

            return View(productComponentModel);
        }
    }
}
