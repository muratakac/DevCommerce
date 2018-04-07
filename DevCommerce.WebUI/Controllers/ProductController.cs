using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCommerce.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        public ProductController(ICategoryService categoryService, IBrandService brandService)
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