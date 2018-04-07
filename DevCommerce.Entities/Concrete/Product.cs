using DevCommerce.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Product : IEntity
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> Discount { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
