using DevCommerce.Entities;
using System.Collections.Generic;

namespace DevCommerce.WebUI.Models
{
    public class CategoryPartialModel
    {
        public List<Category> Categories { get; set; }
        public List<Category> AllCategories { get; set; }
    }
}
