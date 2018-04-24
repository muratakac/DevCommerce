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
    public class CategoryListViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            CategoryPartialModel categoryPartialModel = new CategoryPartialModel();
            string stringData = ClientBaseController.ServiceGetData("/api/Categories", RequestType.GET, null, true);
            categoryPartialModel.Categories = JsonConvert.DeserializeObject<List<Category>>(stringData);
            categoryPartialModel.AllCategories = categoryPartialModel.Categories;
            return View(categoryPartialModel);
        }
    }
}
