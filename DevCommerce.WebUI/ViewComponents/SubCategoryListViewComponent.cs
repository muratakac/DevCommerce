using DevCommerce.Entities;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.WebUI.ViewComponents
{
    public class SubCategoryListViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke(int categoryId, List<Category> categoryList)
        {
            CategoryPartialModel categoryPartialModel = new CategoryPartialModel();
            categoryPartialModel.Categories = categoryList.Where(x => x.ParentId == categoryId).ToList();
            categoryPartialModel.AllCategories = categoryList;
            return View(categoryPartialModel);
        }
    }
}
