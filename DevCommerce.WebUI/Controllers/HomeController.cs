using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCommerce.Business.Abstract;
using DevCommerce.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        public HomeController(ICategoryService categoryService,IBrandService brandService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}