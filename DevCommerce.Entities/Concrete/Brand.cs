using DevCommerce.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Brand : IEntity
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public Nullable<int> ImageId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
