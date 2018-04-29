using System;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Product :BasketProduct
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Images = new HashSet<ProductImage>();
        }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public Nullable<short> Discount { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        
        public  Category Category { get; set; }
        public  ICollection<ProductImage> Images { get; set; }
        public  ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
