using DevCommerce.Entities;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DevCommerce.WebUI.Controllers
{
    public class HomeController : ClientBaseController
    {
        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            //string stringData = ClientBaseController.ServiceGetData("/api/Product", RequestType.GET, null, true);
            //viewModel.Products = JsonConvert.DeserializeObject<List<Product>>(stringData);

            return View(viewModel);
        }
    }
}