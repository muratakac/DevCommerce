using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Controllers;
using DevCommerce.WebUI.Models;
using DevCommerce.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.WebUI.ViewComponents
{
    [ViewComponent(Name = "ProductList")]
    public class ProductListViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke(int categoryId, int brandId, int pageNumber = 0)
        {
            PagingModel<Product> pagingModel = new PagingModel<Product>();
            pagingModel.CurrentPage = pageNumber;
            pagingModel.PageSize = 6;

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

            pagingModel.PagedData = products.ToList();
            productComponentModel.Products = pagingModel;
            productComponentModel.CategoryId = categoryId;
            productComponentModel.BrandId = brandId;

            return View(productComponentModel);
        }
    }
}
