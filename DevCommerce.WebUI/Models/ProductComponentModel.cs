using DevCommerce.Entities;
using DevCommerce.WebUI.Utilities;

namespace DevCommerce.WebUI.Models
{
    public class ProductComponentModel
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public PagingModel<Product> Products { get; set; }
    }
}
