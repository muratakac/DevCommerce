using DevCommerce.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DevCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IStringLocalizer _localizer;

        public HomeController(ICategoryService categoryService, IBrandService brandService, IStringLocalizer localizer)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var value = _localizer.GetString("Hello");
            return View();
        }
    }
}