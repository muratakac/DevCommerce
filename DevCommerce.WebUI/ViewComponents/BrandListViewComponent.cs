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
    public class BrandListViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            BrandPartialModel brandPartialModel = new BrandPartialModel();
            string stringData = ClientBaseController.ServiceGetData("/api/Brands", RequestType.GET, null, true);
            brandPartialModel.Brands = JsonConvert.DeserializeObject<List<Brand>>(stringData);
            return View(brandPartialModel);
        }
    }
}
